using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceTypes
    {
        Wood,
        Coin,
        Food
    }
    private Dictionary<ResourceTypes, int> _resources = new Dictionary<ResourceTypes, int>();

    public delegate void ResourceChangedHandler(ResourceTypes type, int amount);
    public event ResourceChangedHandler OnResourceChanged;

    // Singleton
    public static ResourceManager Instance { get; private set; }

    public float _remainingFireTime;

    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        _resources[ResourceTypes.Wood] = 100;
        _resources[ResourceTypes.Coin] = 2200;
        _resources[ResourceTypes.Food] = 100;
        _remainingFireTime = 1000f;
    }

    void Update()
    {
        if (_remainingFireTime > 0.0f)
        {
            _remainingFireTime -= Time.deltaTime / 60f;
        }
        
        else
        {
            Debug.Log("Fire is out!");
        }    
    }

    public int GetResourceAmount(ResourceTypes type)
    {
        return _resources.TryGetValue(type, out int amount) ? amount : 0;
    }

    public void AddResource(ResourceTypes type, int amount)
    {
        if (!_resources.ContainsKey(type))
            _resources[type] = 0;
        
        _resources[type] += amount;
        Debug.Log($"{_resources[type]}\n");
        OnResourceChanged?.Invoke(type, _resources[type]);
    }

    public void SpendResource(ResourceTypes type, int amount)
    {
        if (!_resources.ContainsKey(type))
            _resources[type] = 0;
        
        if (GetResourceAmount(type) >= amount)
        {
            _resources[type] -= amount;
            OnResourceChanged?.Invoke(type, _resources[type]);
        }
    }
}
