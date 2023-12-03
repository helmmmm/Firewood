using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapResourceFeature : MonoBehaviour
{
    public ResourceManager.ResourceTypes _resourceType;
    private int _amount;
    public bool _gainable = false;

    // public delegate void PlayerRangeHandler();
    // public event PlayerRangeHandler OnPlayerInRange;
    // public event PlayerRangeHandler OnPlayerOutOfRange;


    // Start is called before the first frame update
    void Start()
    {
        switch (_resourceType)
        {
            case ResourceManager.ResourceTypes.Wood:
                _amount = Random.Range(1, 3);
                break;
            case ResourceManager.ResourceTypes.Coin:
                _amount = Random.Range(1, 2);
                break;
            case ResourceManager.ResourceTypes.Food:
                _amount = Random.Range(1, 2);
                break;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            SetGainableTrue();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            SetGainableFalse();
        }
    }

    public int GainResource()
    {
        return _amount;
    }

    private void SetGainableTrue()
    {
        _gainable = true;
        // GetComponentInChildren<MeshRenderer>().enabled = true;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void SetGainableFalse()
    {
        _gainable = false;
        // GetComponentInChildren<MeshRenderer>().enabled = false;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
