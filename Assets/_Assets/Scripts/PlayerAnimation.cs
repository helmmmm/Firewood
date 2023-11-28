using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    Vector3 _lastPosition;
    Vector3 _currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        _lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = transform.position;
        float speed = Vector3.Distance(_lastPosition, transform.position) / Time.deltaTime;

        if (speed > 2.0f)
        {
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
        Debug.Log(speed);


        _lastPosition = _currentPosition;
    }
}
