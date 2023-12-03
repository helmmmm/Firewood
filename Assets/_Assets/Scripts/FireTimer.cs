using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class FireTimer : MonoBehaviour
{
    private float _fireTimer;
    [SerializeField] private TMP_Text _fireTimerText;
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private ParticleSystem _fireBurst;
    private Vector3 _originalScale;
    public FireTimer Instance { get; private set; }

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _originalScale = _fireBurst.transform.localScale; 
    }

    // Update is called once per frame
    void Update()
    {
        _fireTimer = ResourceManager.Instance._remainingFireTime;
        if (_fireTimer > 0.0f)
        {
            TimeSpan timespan = TimeSpan.FromMinutes(_fireTimer);
            _fireTimerText.text = timespan.ToString(@"dd\:hh\:mm\:ss");
        }
        
        else
        {
            Debug.Log("Fire is out!");
        }    
    }

    public void OnFeedFireClick()
    {
        ResourceManager.Instance._remainingFireTime += 5;
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceTypes.Wood, 1);
        
        _fireBurst.Play();
        _fire.gameObject.transform.DOScale(_originalScale * 1.3f, 0.35f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => 
            {
                _fire.transform.DOScale(_originalScale, 0.75f)
                    .SetEase(Ease.InQuad);
            });
    }


}
