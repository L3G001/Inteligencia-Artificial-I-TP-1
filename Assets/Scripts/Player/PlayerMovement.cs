using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Camera _playerCamera;
    public float lookSpeed = default;
    public float lookXLimit = default;

    [Header("Movement")]
    public float walkSpeed = default;
    public float runSpeed = default;
    public float jumpHeight = default;
    public float gravity = default;
    private float rotationX = 0;
    private Vector3 _moveDirection = Vector3.zero;
    public bool canMove = default;
    private float _movementDirectionY = default;
    private bool _isRunning = default;
    private bool _isJumping = default;

    private CharacterController _characterController;

    private Vector2 _inputVector;

    private void OnDisable()
    {
        GameManager.instance.inputReader.MoveEvent -= HandleMove;
        GameManager.instance.inputReader.JumpEvent -= HandleJump;
        GameManager.instance.inputReader.JumpCancelledEvent -= HandleCancelledJump;
        GameManager.instance.inputReader.SprintEvent -= HandleSprint;
        GameManager.instance.inputReader.SprintCancelledEvent -= HandleCancelledSprint;
    }

    void Start()
    {
        var _input = GameManager.instance.inputReader;
        _characterController = GetComponent<CharacterController>();
        _input.MoveEvent += HandleMove;
        _input.JumpEvent += HandleJump;
        _input.JumpCancelledEvent += HandleCancelledJump;
        _input.SprintEvent += HandleSprint;
        _input.SprintCancelledEvent += HandleCancelledSprint;
    }

    void Update()
    {
        Move();
        Jump();
        #region Rotation
        if (canMove && !UIManager.instance.isPaused)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
        #region Camera Animation
        if (_characterController.velocity.magnitude == 0)
        {
            _playerCamera.GetComponent<Animator>().SetBool("IsRunning", false);
            _playerCamera.GetComponent<Animator>().SetBool("IsWalking", false);
            _playerCamera.GetComponent<Animator>().SetBool("IsIdle", true);
        }
        else if (_characterController.velocity.magnitude != 0 && _isRunning)
        {
            _playerCamera.GetComponent<Animator>().SetBool("IsRunning", true);
            _playerCamera.GetComponent<Animator>().SetBool("IsWalking", false);
            _playerCamera.GetComponent<Animator>().SetBool("IsIdle", false);
        }
        else if (!_isRunning && _characterController.velocity.magnitude != 0)
        {
            _playerCamera.GetComponent<Animator>().SetBool("IsRunning", false);
            _playerCamera.GetComponent<Animator>().SetBool("IsWalking", true);
            _playerCamera.GetComponent<Animator>().SetBool("IsIdle", false);
        }
        #endregion
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void HandleMove(Vector2 inputVector) { _inputVector = inputVector; }
    private void HandleJump() { _isJumping = true; }
    private void HandleCancelledJump() { _isJumping = false; }
    private void HandleSprint() { _isRunning = true; }
    private void HandleCancelledSprint() { _isRunning = false; }

    private void Move()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? (_isRunning ? runSpeed : walkSpeed) * (_inputVector.x) : 0;
        float curSpeedY = canMove ? (_isRunning ? runSpeed : walkSpeed) * (_inputVector.y) : 0;
        _movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedY) + (right * curSpeedX);
    }

    private void Jump()
    {
        if (_isJumping && canMove && _characterController.isGrounded)
        {
            _moveDirection.y = jumpHeight;
        }
        else
        {
            _moveDirection.y = _movementDirectionY;
        }
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }
    }
}
