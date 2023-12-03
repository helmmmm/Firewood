using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextPopup : MonoBehaviour
{
    private float _lifetime = 0.75f;
    private float _currentElapsed = 0.0f;
    private void OnEnable() 
    {
        gameObject.transform.DOLocalMoveY(-0.2f, 0.3f).SetEase(Ease.OutCubic);
        gameObject.transform.DOScale(1.0f, 0.3f).SetEase(Ease.OutCubic);
    }

    private void Update() 
    {
        _currentElapsed += Time.deltaTime;
        if (_currentElapsed >= _lifetime)
        {
            Destroy(gameObject);
        }    
    }
}
