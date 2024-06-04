using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class Movement : MonoBehaviour, IDamagable
{
    [Header("General")]
    [SerializeField] private float speed;
    [SerializeField] private float health = 10f;
    private bool _isDead = false;

    [Header("DodgeRoll")]
    [SerializeField] private AnimationCurve dodgeCurve;
    [SerializeField ]private float dodgeTime = 0.2f;
    [SerializeField] private float dodgeDistance = 3f;

    private Vector3 _lastMovedDirection;
    private bool _isDodging = false;
    private bool _canDodge = true;

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
        if (!_canDodge) return;
        if (context.performed)
        {
            StartCoroutine(Dodge());
        }
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }


    private void Move()
    {
        if (!(_moveVector.magnitude >= 0.1f) && _isDodging) return;
        
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

        //Save last moved direction for the Dodge mechanic
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
        if (IsGrounded() && !_isDodging)
        {
            _gravity.y = 0;
            return;
        }
        _gravity += Physics.gravity * Time.deltaTime;
        _charCon.Move(_gravity);
    }
    
    //Simple is grounded function as a easy to access helper function
    private bool IsGrounded() => _charCon.isGrounded;

    //Dodging mechanic via coroutine because of the Time benefits
    private IEnumerator Dodge()
    {
        _isDodging = true;
        _canDodge = false;

        //get positions for the Lerp
        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position + (_lastMovedDirection * dodgeDistance);

        float currentDodgeTime = dodgeTime;
        while (currentDodgeTime > 0)
        { 
            float dodgeTimeNormalized = 1 - (currentDodgeTime / dodgeTime);

            //Move from position A to B with the animationCurve
            Vector3 newPos = Vector3.Lerp(startPos, targetPos, dodgeCurve.Evaluate(dodgeTimeNormalized));
            transform.position = newPos;

            currentDodgeTime -= Time.deltaTime;
            yield return null;
        }

        //make sure the player ends up in the right position
        transform.position = targetPos;

        _isDodging = false;
        _canDodge = true; //# should be set to true after a cooldown
        yield return null;
    }

    #region IDamageable Logic
    public void Damage(float amount)
    {
        if (!_isDodging) 
        { 
            health -= amount;
        }

        if (health <= 0 && !_isDead)
        {
            _isDead = true;
            OnDeath.Invoke();
            this.enabled = false;
        }
    }
    #endregion
}
