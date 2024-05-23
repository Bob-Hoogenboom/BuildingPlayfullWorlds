using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class Movement : MonoBehaviour, IDamagable
{
    [Header("General")]
    [SerializeField] private float speed;
    [SerializeField] private float health = 10f;

    [Header("DodgeRoll")]
    [SerializeField] private AnimationCurve _dashCurve;
    [SerializeField ]private float _dashTime;
    [SerializeField] private float _dashDistance;

    private Vector3 _lastMovedDirection;
    private bool _isRolling = false;
    private bool _canRoll = true;

    public float HitPoints
    {
        get => health;
        set => health = value;
    }

    [Header("Events")]
    public UnityEvent OnDeath;

    private CharacterController _charCon;
    private Camera _cam;
    private Vector3 _gravity;
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

    public void DodgeRollInput(InputAction.CallbackContext context)
    {
        if (!_canRoll) return;
        if (context.performed)
        {
            StartCoroutine(DodgeRoll());
        }

    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }


    private void Move()
    {
        if (!(_moveVector.magnitude >= 0.1f) && _isRolling) return;
        
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

        _lastMovedDirection = moveDir;

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

    private IEnumerator DodgeRoll()
    {
        
        _isRolling = true;
        _canRoll = false; //# set to true when the cooldown has expired
        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position + (_lastMovedDirection * _dashDistance);
        float currentDashTime = _dashTime;
        Debug.Log(targetPos);
        while (currentDashTime > 0)
        {
            Debug.Log("Dodgerolling");
            Debug.Log(currentDashTime);
            Vector3 targetMotion = targetPos - transform.position;
            float dashTimeNormal = (_dashTime / _dashTime) - (currentDashTime / _dashTime);
            Vector3 newPos = Vector3.Lerp(startPos, targetPos, _dashCurve.Evaluate(dashTimeNormal));
            _charCon.Move(targetMotion);
            currentDashTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Debug.Log("Dodgeroll End");
        _isRolling = false;
        _canRoll = true;
        yield return null;
    }

    #region IDamageable Logic
    public void Damage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            OnDeath.Invoke();
            Time.timeScale = 0;
        }
    }
    #endregion
}
