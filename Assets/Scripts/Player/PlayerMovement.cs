using Unity.VisualScripting;
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

    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        #region Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(PlayerInputManager.instance.sprintKey);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion
        #region Jump
        if (Input.GetKey(PlayerInputManager.instance.jumpKey) && canMove && _characterController.isGrounded)
        {
            _moveDirection.y = jumpHeight;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion
        #region Rotation
        _characterController.Move(_moveDirection * Time.deltaTime);
        if (canMove)
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
        else if (_characterController.velocity.magnitude != 0 && isRunning)
        {
            _playerCamera.GetComponent<Animator>().SetBool("IsRunning", true);
            _playerCamera.GetComponent<Animator>().SetBool("IsWalking", false);
            _playerCamera.GetComponent<Animator>().SetBool("IsIdle", false);
        }
        else if (!isRunning && _characterController.velocity.magnitude != 0)
        {
            _playerCamera.GetComponent<Animator>().SetBool("IsRunning", false);
            _playerCamera.GetComponent<Animator>().SetBool("IsWalking", true);
            _playerCamera.GetComponent<Animator>().SetBool("IsIdle", false);
        }
        #endregion
    }
}
