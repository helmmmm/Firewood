using UnityEngine;

public class SwipeToRotate : MonoBehaviour
{
    public Transform _target; // The target (island) around which the camera rotates
    public float _rotationSpeed = 0.5f; // Speed of rotation
    public float _returnSpeed = 1f; // Speed at which the camera returns to the original position
    public float _angleLimit = 30f; // Maximum rotation angle

    private Vector2 _startTouchPosition;
    private Quaternion _originalRotation;
    private Quaternion _targetRotation;
    private bool _isSwiping = false;

    private void Start()
    {
        _originalRotation = transform.rotation;
        _targetRotation = _originalRotation;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouch();
        }
        #if UNITY_EDITOR
        HandleMouse();
        #endif

        // Smoothly rotate the camera
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 
                                              _isSwiping ? _rotationSpeed * Time.deltaTime : _returnSpeed * Time.deltaTime);
    }

    private void HandleTouch()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _startTouchPosition = touch.position;
                _isSwiping = true;
                break;

            case TouchPhase.Moved:
                ProcessSwipe(touch.position);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                _targetRotation = _originalRotation;
                _isSwiping = false;
                break;
        }
    }

    #if UNITY_EDITOR
    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
            _isSwiping = true;
        }
        else if (Input.GetMouseButton(0))
        {
            ProcessSwipe(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _targetRotation = _originalRotation;
            _isSwiping = false;
        }
    }
    #endif

    private void ProcessSwipe(Vector2 currentTouchPosition)
    {
        float swipeAmount = (currentTouchPosition.x - _startTouchPosition.x) / Screen.width;
        float angle = Mathf.Clamp(swipeAmount * _angleLimit, -_angleLimit, _angleLimit);

        _targetRotation = Quaternion.Euler(0, -angle, 0) * _originalRotation;
    }
}

