using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _woodResourcesText;
    [SerializeField] private TMP_Text _coinResourcesText;
    [SerializeField] private TMP_Text _foodResourcesText;
    [SerializeField] private GameObject _VPSPanel;
    [SerializeField] private Image _VPSImage;
    [SerializeField] private TMP_Text _VPSNameText;
    [SerializeField] private Button _slot1Button;
    [SerializeField] private Button _slot2Button;
    [SerializeField] private Button _slot3Button;

    

    private void Start() 
    {
        UpdateResourcesUI();  

        ResourceManager.Instance.OnResourceChanged += UpdateResourceText;
    }

    private void UpdateResourceText(ResourceManager.ResourceTypes type, int amount)
    {
        UpdateResourcesUI();
    }

    private void UpdateResourcesUI()
    {
        _woodResourcesText.text = "<sprite name=\"Wood\"> " + ResourceManager.Instance.GetResourceAmount(ResourceManager.ResourceTypes.Wood).ToString();
        _coinResourcesText.text = "<sprite name=\"Coin\"> " + ResourceManager.Instance.GetResourceAmount(ResourceManager.ResourceTypes.Coin).ToString();
        _foodResourcesText.text = "<sprite name=\"Food\"> " + ResourceManager.Instance.GetResourceAmount(ResourceManager.ResourceTypes.Food).ToString();  
    }

    public void VPSTentClicked(Sprite vpsImg, string vpsname)
    {
        Debug.Log("VPSTentClicked Enter >>>>>");
        _VPSImage.sprite = vpsImg;
        _VPSNameText.text = vpsname;
        ResourceManager.Instance._vpsTargetName = vpsname;
        _VPSPanel.SetActive(true);
        Debug.Log("VPSTentClicked Exit <<<<<");
    }

    public void OnVPSPanelCloseButtonClicked()
    {
        _VPSPanel.SetActive(false);
    }

    public void OnSlot1Clicked()
    {
        _slot1Button.interactable = false;
        _slot1Button.GetComponentInChildren<TMP_Text>().text = $"Claimed by: {ResourceManager.Instance._campName}";
        ResourceManager.Instance._deployed = true;
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceTypes.Food, 100);
    }

    public void OnSlot2Clicked()
    {
        _slot2Button.interactable = false;
        _slot2Button.GetComponentInChildren<TMP_Text>().text = $"Claimed by: {ResourceManager.Instance._campName}";
        ResourceManager.Instance._deployed = true;
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceTypes.Food, 100);
    }

    public void OnSlot3Clicked()
    {
        _slot3Button.interactable = false;
        _slot3Button.GetComponentInChildren<TMP_Text>().text = $"Claimed by: {ResourceManager.Instance._campName}";
        ResourceManager.Instance._deployed = true;
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceTypes.Food, 100);
    }

    public void OnFeverTimeButtonClicked()
    {
        ResourceManager.Instance.SpendResource(ResourceManager.ResourceTypes.Food, 50);
        SceneManager.LoadScene("Semantic");
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("Home");
    }

    private void OnDisable() 
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= UpdateResourceText;
    }
}
