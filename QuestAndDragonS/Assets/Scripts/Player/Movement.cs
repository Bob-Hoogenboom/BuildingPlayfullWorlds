using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Movement : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private float health = 10f;

    public UnityEvent OnDeath;

    private Camera _cam;
    private Vector3 _gravity;
    private float _turnSmoothVelocity;
    private CharacterController _charCon;
    private Vector2 _moveVector;


    private void Awake()
    {
        _charCon = GetComponent<CharacterController>();
        _cam = Camera.main;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    public void LookMouseInput(InputAction.CallbackContext context)
    {
        if (_cam == null) return;
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector2 objectPosition = (Vector2) _cam.WorldToScreenPoint(transform.position); //Debug on application quit error 
        Vector2 direction = (mousePosition - objectPosition).normalized;

        RotateAim(direction);
    }
    
    public void LookStickInput(InputAction.CallbackContext context)
    {
        Vector2 stickDirection = context.ReadValue<Vector2>().normalized;

        RotateAim(stickDirection);
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }

    private void Move()
    {
        if (!(_moveVector.magnitude >= 0.1f)) return;
        
        Vector3 camForward = _cam.transform.forward;
        Vector3 camRight = _cam.transform.right;
        
        //Debug.Log("Forward: " + camForward + " Right: " + camRight);

        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        Vector3 forwardRelative = _moveVector.y * camForward;
        Vector3 rightRelative = _moveVector.x * camRight;

        Vector3 moveDir = forwardRelative + rightRelative;

        _charCon.Move(moveDir * speed * Time.deltaTime);
    }
    
    private void RotateAim(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
         
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    private void ApplyGravity()
    {
        _gravity += Physics.gravity * Time.deltaTime;
        _charCon.Move(_gravity);
        if (IsGrounded())
        {
            _gravity.y = 0;
        }
    }
    private bool IsGrounded() => _charCon.isGrounded;

    public void Damage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            OnDeath.Invoke();
            Time.timeScale = 0;
        }
    }
}
