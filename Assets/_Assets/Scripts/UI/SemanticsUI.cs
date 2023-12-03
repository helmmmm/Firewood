using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SemanticsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _woodResourcesText;
    [SerializeField] private TMP_Text _coinResourcesText;
    [SerializeField] private TMP_Text _foodResourcesText;
    [SerializeField] private GameObject _promptText;
    [SerializeField] private GameObject _returnButton;


    private void Start() 
    {
        UpdateResourcesUI();  

        ResourceManager.Instance.OnResourceChanged += UpdateResourceText;

        _promptText.SetActive(true);
        _returnButton.SetActive(false);
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

    public void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("test_maps");
    }

    public void EndUI()
    {
        _promptText.SetActive(false);
        _returnButton.SetActive(true);
    }

    private void OnDisable() 
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= UpdateResourceText;
    }
}
