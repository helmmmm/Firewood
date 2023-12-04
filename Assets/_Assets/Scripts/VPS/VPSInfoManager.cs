using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPSInfoManager : MonoBehaviour
{
    private string _targetName;
    private Sprite _targetImage;
    // [SerializeField] private MapUI _mapUI;

    public void TentClicked()
    {
        Debug.Log("TentClicked Enter >>>>>");
        // _mapUI.VPSTentClicked(_targetImage, _targetName);
        // find mapUI in scene
        GameObject mapUI = GameObject.Find("Map UI");
        mapUI.GetComponent<MapUI>().VPSTentClicked(_targetImage, _targetName);
        Debug.Log("TentClicked Exit <<<<<");
    }

    public void SetTargetName(string targetName)
    {
        _targetName = targetName;
    }

    public void SetImage(Sprite targetImage)
    {
        _targetImage = targetImage;
    }
}
