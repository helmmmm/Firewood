using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Niantic.Lightship.AR.VpsCoverage;
using Niantic.Lightship.Maps;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // For UnityWebRequestTexture

public class VPSManager : MonoBehaviour
{
    public static VPSManager Instance { get; private set; }

    [SerializeField] private CoverageClientManager coverageClientManager;
    [SerializeField] private GameObject tentPrefab;
    [SerializeField] private LightshipMapView lightshipMapView;
    [SerializeField] private Transform mapRoot; // The root transform of your map

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        RequestCoverageAreas();
    }

    public void RequestCoverageAreas()
    {
        coverageClientManager.TryGetCoverage(OnTryGetCoverageAreas);
    }

    private async void OnTryGetCoverageAreas(AreaTargetsResult areaTargetsResult)
    {
        Debug.Log("Locations found: " + areaTargetsResult.AreaTargets.Count);
        foreach (var areaTarget in areaTargetsResult.AreaTargets)
        {
            Niantic.Lightship.Maps.Core.Coordinates.LatLng targetCoords =
            new Niantic.Lightship.Maps.Core.Coordinates.LatLng(areaTarget.Target.Center.Latitude, areaTarget.Target.Center.Longitude);

            Vector3 mapPosition = lightshipMapView.LatLngToScene(targetCoords);
            GameObject tentInstance = Instantiate(tentPrefab, mapPosition, Quaternion.identity, mapRoot);
            await SetVpsImageOnTent(tentInstance, areaTarget.Target.ImageURL);
        }
    }

    private async Task SetVpsImageOnTent(GameObject tentInstance, string imageUrl)
    {
        Texture2D image = await DownloadImage(imageUrl);
        if (image != null)
        {
            Image VPSImage = tentInstance.GetComponentInChildren<Image>();
            VPSImage.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        }
    }

    private async Task<Texture2D> DownloadImage(string imageUrl)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            var asyncOp = uwr.SendWebRequest();
            while (!asyncOp.isDone)
            {
                await Task.Delay(1000/30); // wait for 1/30 second
            }

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error downloading image: {uwr.error}");
                return null;
            }

            return DownloadHandlerTexture.GetContent(uwr);
        }
    }
}
