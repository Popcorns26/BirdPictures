using UnityEngine;
using UnityEngine.InputSystem;

public class FlyingObject : CheckIfVisible, PlayerControls.IPlayerActions
{
    [SerializeField] private AnimationCurve _flyingHeightModifier;
    [SerializeField] private AnimationCurve _flyingSpeedModifier;
    [SerializeField] private float _flyingSpeed = 5f;
    public float flightDuration { get; } = 3f;
    [SerializeField] private float _flyingCurrentDuration = 0f;
    [SerializeField] private Vector3 _startingPos;
    [SerializeField] private Vector3 _direction = Vector3.right;

    private bool _isFlying;
    private PlayerControls _playerControls;
    public void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.SetCallbacks(this);
        }

        _playerControls.Player.Enable();
    }
    private void Update()
    {
        if (!_isFlying)
        {
            return;
        }

        if (_flyingCurrentDuration >= flightDuration)
        {
            _isFlying = false;
            _flyingCurrentDuration = 0f;
            return;
        }

        _flyingCurrentDuration += Time.deltaTime;
        float evaluatingValue = _flyingCurrentDuration / flightDuration;
        //Debug.Log($"Evaluating value is {evaluatingValue}");
        Vector3 newPos = transform.position +
                         _direction * (_flyingSpeedModifier.Evaluate(evaluatingValue) * _flyingSpeed);
        transform.position = newPos;
        //Debug.Log("New bird position is " + newPos);
    }

    public void Fly(Vector3 startingPosition, Vector3 endPosition)
    {
        if (_isFlying)
        {
            return;
        }

        _isFlying = true;

        transform.position = startingPosition;
        _direction = endPosition - startingPosition;
        _direction.Normalize();
    }

    #region Controls
    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnTakePicture(InputAction.CallbackContext context)
    {
    }

    public void OnBirdFly(InputAction.CallbackContext context)
    {
        Fly(_startingPos, _direction);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    #endregion
    
}