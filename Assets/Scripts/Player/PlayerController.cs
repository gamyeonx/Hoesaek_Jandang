using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    [Header("플레이어 자신을 참조하세요")]
    [Tooltip("이 PlayerController가 붙어있는 GameObject의 Rigidbody2D")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [Tooltip("이 PlayerController가 붙어있는 GameObject의 PlayerState")]
    [SerializeField] private PlayerState _playerState;

    private Vector2 _moveInput;

    private bool _isFlying;
    private float _flyStartY;

    private bool _isDashing;
    private float _dashDirection;
    private float _dashTimer;

    private bool _isBoosting;

    #region 유니티 생명주기 함수
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.Player.Move.performed += OnPlayerMove;
        _playerInputActions.Player.Move.canceled += OnPlayerMoveCancel;
        _playerInputActions.Player.Jump.performed += OnPlayerJump;
        _playerInputActions.Player.Jump.canceled += OnPlayerJumpCancel;
        _playerInputActions.Player.Fly.performed += OnPlayerFly;
        _playerInputActions.Player.Fly.canceled += OnPlayerFlyCancel;
        _playerInputActions.Player.Dash.performed += OnPlayerDash;
        _playerInputActions.Player.Boost.performed += OnPlayerBoost;
        _playerInputActions.Player.Boost.canceled += OnPlayerBoostCancel;
    }

    private void FixedUpdate()
    {
        if (_isDashing) PlayerDash();

        PlayerMove();

        if (_isFlying) PlayerFly();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Move.performed -= OnPlayerMove;
        _playerInputActions.Player.Move.canceled -= OnPlayerMoveCancel;
        _playerInputActions.Player.Jump.performed -= OnPlayerJump;
        _playerInputActions.Player.Jump.canceled -= OnPlayerJumpCancel;
        _playerInputActions.Player.Fly.performed -= OnPlayerFly;
        _playerInputActions.Player.Fly.canceled -= OnPlayerFlyCancel;
        _playerInputActions.Player.Dash.performed -= OnPlayerDash;
        _playerInputActions.Player.Boost.performed -= OnPlayerBoost;
        _playerInputActions.Player.Boost.canceled -= OnPlayerBoostCancel;
        _playerInputActions.Disable();
    }
    #endregion

    #region 초기화
    private void Init()
    {
        _playerInputActions = new PlayerInputActions();
    }
    #endregion

    #region 플레이어 이동
    private void OnPlayerMove(InputAction.CallbackContext ctx)
    {
        _moveInput = new Vector2(ctx.ReadValue<Vector2>().x, 0f);
    }

    private void OnPlayerMoveCancel(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    private void PlayerMove()
    {
        if (_isDashing) return;

        float moveSpeed = _isBoosting ? _playerState.BoostSpeed : _playerState.MoveSpeed;

        _rigidbody.linearVelocity = new Vector2(_moveInput.x * moveSpeed, _rigidbody.linearVelocity.y);
    }
    #endregion

    #region 플레이어 점프
    private void OnPlayerJump(InputAction.CallbackContext ctx)
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _playerState.JumpForce);
    }

    private void OnPlayerJumpCancel(InputAction.CallbackContext ctx)
    {
        if (_rigidbody.linearVelocity.y > 0f)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
        }
    }
    #endregion

    #region 플레이어 활공
    private void OnPlayerFly(InputAction.CallbackContext ctx)
    {
        _isFlying = true;
        _flyStartY = _rigidbody.position.y;
    }

    private void OnPlayerFlyCancel(InputAction.CallbackContext ctx)
    {
        _isFlying = false;
        _rigidbody.gravityScale = 1f;
    }

    private void PlayerFly()
    {
        float maxY = _flyStartY + _playerState.FlyHeight;

        if (_rigidbody.position.y < maxY)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _playerState.FlySpeed);
        }
        else
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        }

        _rigidbody.gravityScale = 0f;
    }
    #endregion

    #region 플레이어 대쉬
    private void OnPlayerDash(InputAction.CallbackContext ctx)
    {
        float direction = _moveInput.x;

        if (direction == 0f || _isDashing)
        {
            return;
        }

        _isDashing = true;
        _dashDirection = direction;
        _dashTimer = _playerState.DashDuration;
    }

    private void PlayerDash()
    {
        _rigidbody.linearVelocity = new Vector2(_dashDirection * _playerState.DashSpeed, _rigidbody.linearVelocity.y);

        _dashTimer -= Time.fixedDeltaTime;

        if (_dashTimer <= 0f)
        {
            _isDashing = false;
        }
    }
    #endregion

    #region 플레이어 부스트
    private void OnPlayerBoost(InputAction.CallbackContext ctx)
    {
        _isBoosting = true;
    }

    private void OnPlayerBoostCancel(InputAction.CallbackContext ctx)
    {
        _isBoosting = false;
    }
    #endregion
}
