using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour
{
    private float _displayDuration = 10.0f;
    private float _displayElapsed = 0.0f;

    void Update()
    {
        if (gameObject.activeSelf && _displayElapsed < _displayDuration)
        {
            _displayElapsed += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            _displayElapsed = 0.0f;
        }
    }
}
