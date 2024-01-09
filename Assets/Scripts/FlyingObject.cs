using UnityEngine;

public class FlyingObject : CheckIfVisible
{
    [SerializeField] private AnimationCurve _flyingHeightModifier;
    [SerializeField] private AnimationCurve _flyingSpeedModifier;
    [SerializeField] private float _flyingSpeed = 5f;
    [SerializeField] private float _flyingTime = 3f;
    [SerializeField] private float _flyingCurrentDuration = 0f;
    [SerializeField] private Vector3 _startingPos;
    [SerializeField] private Vector3 _direction = Vector3.right;

    private bool _isFlying;
    private void Update()
    {
        if (!_isFlying)
        {
            return;
        }

        if (_flyingCurrentDuration >= _flyingTime)
        {
            _isFlying = false;
            _flyingCurrentDuration = 0f;
            return;
        }

        _flyingCurrentDuration += Time.deltaTime;
        float evaluatingValue = _flyingCurrentDuration / _flyingTime;
        Debug.Log($"Evaluating value is {evaluatingValue}");
        Vector3 newPos = transform.position + _direction * (_flyingSpeedModifier.Evaluate(evaluatingValue) * _flyingSpeed);
        transform.position = newPos;
        Debug.Log("New bird position is " + newPos);
    }

    public void Fly()
    {
        if (_isFlying)
        {
            return;
        }

        _isFlying = true;

        transform.position = _startingPos;
        
    }
}
