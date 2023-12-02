using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.MapLayers.Components;
using Niantic.Lightship.Maps;
using TMPro;

public class MapInteractions : MonoBehaviour
{
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private LightshipMapView _lightshipMapView;
    [SerializeField] private FloatText _floatingText;

    void Update()
    {
        var touchPosition = Vector3.zero;
        bool touchDetected = false;

        // if (Input.touchCount == 1)
        // {
        //     if (Input.touches[0].phase == TouchPhase.Ended)
        //     {
        //         touchPosition = Input.touches[0].position;
        //         touchDetected = true;
        //     }
        // }

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            touchDetected = true;
        }

        if (touchDetected) 
        {
            CheckForInteractableTouch(touchPosition);
        }
    }

    private void CheckForInteractableTouch(Vector3 touchPosition)
    {
        var ray = _mapCamera.ScreenPointToRay(touchPosition);

        if (!Physics.Raycast(ray, out var hit))
        {
            return;
        }

        var hitResources = hit.collider.GetComponent<MapResourceFeature>();
        if (hitResources == null)
            return;

        if (!hitResources._gainable)
            return;
            
        int amount = hitResources.GainResource();
        ResourceManager.Instance.AddResource(hitResources._resourceType, amount);
        Destroy(hitResources.gameObject);

        var floatingTextPos = hit.point + Vector3.up * 20.0f;
        var forward = floatingTextPos - _mapCamera.transform.position;
        var rotation = Quaternion.LookRotation(forward, Vector3.up);
        var floatText = Instantiate(_floatingText, floatingTextPos, rotation);
        floatText.SetText($"+{amount} {hitResources._resourceType.ToString()}");
    }
}
