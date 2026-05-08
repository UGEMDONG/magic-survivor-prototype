using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Player
{
    private InputSystem_Actions _inputMap;
    private InputAction _moveAction;

    private Vector2 _moveInput;

    protected Vector3 _moveDir;
    [SerializeField] protected float _moveSpeed = 6;
    [Space]
    [SerializeField] protected int _level = 1;
    [SerializeField] protected float _exp;
    [SerializeField] protected float _expToLvUp;

    protected override void OnLevelUp()
    {
        
    }

    #region Initialization
    public override void Initialize()
    {
        CreateInput();
    }
    private void CreateInput()
    {
        _inputMap = new InputSystem_Actions();
        _moveAction = _inputMap.Player.Move;

        EnableInput();
    }
    private void EnableInput()
    {
        if (_inputMap == null) return;
        _inputMap.Enable();

        _moveAction.started += Move_Started;
        _moveAction.performed += Move_Performed;
        _moveAction.canceled += Move_Canceled;
    }
    private void DisableInput()
    {
        if (_inputMap == null) return;
        _inputMap.Disable();

        _moveAction.started += Move_Started;
        _moveAction.performed -= Move_Performed;
        _moveAction.canceled -= Move_Canceled;
    }
    #endregion
    #region Interface
    #region IDamageable
    public override void SetDie()
    {

    }

    public override void TakeDamage(float damage)
    {

    }
    #endregion
    #endregion   

    #region Input Callbacks
    void Move_Started(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    void Move_Performed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    void Move_Canceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }
    #endregion
    #region General
    protected override void Move()
    {
        _moveDir = _moveInput.normalized;
        transform.position += _moveDir * Time.deltaTime * _moveSpeed;
    }
    public override void GetEXP(float exp)
    {
        _exp += exp;
        if (exp >= _expToLvUp)
        {
            OnLevelUp();
            _exp = 0f;
        }
    }
    #endregion

    #region Unity LifeCycle
    protected override void OnAwake()
    {
        Initialize();
    }
    protected override void OnEnabled()
    {
        EnableInput();
    }
    protected override void OnDisabled()
    {
        DisableInput();
    }
    protected override void OnStart() { }
    protected override void OnUpdate()
    {
        Move();
    }   
    #endregion
}
