using Camera;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls _playerControls;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PhotoCamera _camera;
    [SerializeField] private float _movementSpeed = 5f;

    private Vector3 _playerVelocity;

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
        
        _characterController.SimpleMove(_playerVelocity * _movementSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        var transform2 = UnityEngine.Camera.main.transform;
        Vector3 cameraZ = transform2.forward;
        Vector3 cameraX = transform2.right;
        var xValue = value.x * cameraX;
        var yValue = value.y * cameraZ;
        _playerVelocity = xValue + yValue;
        //Debug.Log($"Velocity is : {_playerVelocity}");
    }


    public void OnTakePicture(InputAction.CallbackContext context)
    {
        if(context.started)
            _camera.TakePicture();
    }

    public void OnBirdFly(InputAction.CallbackContext context)
    {
        if(context.started)
            Debug.Log("Fly");
    }
}
