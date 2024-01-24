using Camera;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls _playerControls;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PhotoCamera _camera;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationFactorPerFrame = 1f;

    private Vector3 _direction;
    private Quaternion _rotation;

    public void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.SetCallbacks(this);
        }

        _playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }

    private void Update()
    {
        if (_direction.magnitude != 0f)
        {
            _characterController.SimpleMove(_direction * _movementSpeed);
        }

        if (_rotation.y != 0)
        {
            transform.rotation *= _rotation;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        var transform2 = UnityEngine.Camera.main.transform;
        Vector3 cameraZ = transform2.forward;
        Vector3 cameraX = transform2.right;
        var xValue = value.x * cameraX;
        var yValue = value.y * cameraZ;
        _direction = xValue + yValue;
        //Debug.Log($"Velocity is : {_playerVelocity}");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 rotationValueRead = context.ReadValue<Vector2>();
        //Debug.Log($"Value: {rotationValueRead}");

        //rotationValueRead.Normalize();
        rotationValueRead *= _rotationFactorPerFrame;
        //Temporarily removed z rotation because it has a funny result
        _rotation = Quaternion.Euler(0, rotationValueRead.x, 0);
    }

    public void OnTakePicture(InputAction.CallbackContext context)
    {
        if (context.started)
            _camera.TakePicture();
    }

    public void OnBirdFly(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Fly");
    }
}