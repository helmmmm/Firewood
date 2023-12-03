using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private SemanticsUI _semanticsUI;
    private float _timer = 5f;
    private bool _started = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_started)
        {
            _started = true;
        }

        if (_started)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _timerText.text = _timer.ToString("F1");
            }
            else
            {
                _timerText.text = "Time's up!";
                _semanticsUI.EndUI();
            }   
        }
    }
}
