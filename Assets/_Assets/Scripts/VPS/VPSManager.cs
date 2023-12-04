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
        // Enable LocationService.status to be updated
        Debug.Log("VPSManager Start enter>>>>>>\n");
        Input.location.Start();
        RequestCoverageAreas();
    }

    public void RequestCoverageAreas()
    {
        Debug.Log("Requesting coverage areas enter>>>>>>\n");
        coverageClientManager.TryGetCoverage(OnTryGetCoverageAreas);
        Debug.Log("Requesting coverage areas exit<<<<<<\n");
    }

    private async void OnTryGetCoverageAreas(AreaTargetsResult areaTargetsResult)
    {
        Debug.Log("OnTryGetCoverageAreas enter>>>>>>\n");
        Debug.Log("Locations found: " + areaTargetsResult.AreaTargets.Count);
        foreach (var areaTarget in areaTargetsResult.AreaTargets)
        {
            Niantic.Lightship.Maps.Core.Coordinates.LatLng targetCoords =
            new Niantic.Lightship.Maps.Core.Coordinates.LatLng(areaTarget.Target.Center.Latitude, areaTarget.Target.Center.Longitude);

            Vector3 mapPosition = lightshipMapView.LatLngToScene(targetCoords);
            Debug.Log($"Target {areaTarget.Target.Name} at {targetCoords} is at {mapPosition}\n");
            
            GameObject tentInstance = Instantiate(tentPrefab, mapPosition, Quaternion.identity, mapRoot);
            await SetVpsImageOnTent(tentInstance, areaTarget.Target.ImageURL);

            VPSInfoManager tentInfo = tentInstance.GetComponent<VPSInfoManager>();
            tentInfo.SetTargetName(areaTarget.Target.Name);
        }
        Debug.Log("OnTryGetCoverageAreas exit<<<<<<\n");
    }

    private async Task SetVpsImageOnTent(GameObject tentInstance, string imageUrl)
    {
        Debug.Log("SetVPSImageOnTent enter>>>>>>\n");
        Texture2D image = await DownloadImage(imageUrl);
        if (image != null)
        {
            Image VPSImage = tentInstance.GetComponentInChildren<Image>();
            VPSImage.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));

            VPSInfoManager tentInfo = tentInstance.GetComponent<VPSInfoManager>();
            tentInfo.SetImage(VPSImage.sprite);
        }
        Debug.Log("SetVPSImageOnTent exit<<<<<<\n");
    }

    private async Task<Texture2D> DownloadImage(string imageUrl)
    {
        Debug.Log("DownloadImage enter>>>>>>\n");
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
            Debug.Log("DownloadImage exit<<<<<<\n");
            return DownloadHandlerTexture.GetContent(uwr);
        }
    }
}
