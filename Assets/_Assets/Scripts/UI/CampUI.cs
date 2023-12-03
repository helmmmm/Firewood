using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CampUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _woodResourcesText;
    [SerializeField] private TMP_Text _coinResourcesText;
    [SerializeField] private TMP_Text _foodResourcesText;
    [SerializeField] private GameObject _chatPanel;
    [SerializeField] private GameObject _messagePanel;
    [SerializeField] private TMP_InputField _chatInputField;
    [SerializeField] private TMP_Text _messageText;

    private void Start() 
    {
        UpdateResourcesUI();

        ResourceManager.Instance.OnResourceChanged += UpdateResourceText;

        _chatPanel.SetActive(false);
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

    public void OnChatButton()
    {
        _chatPanel.SetActive(true);
    }

    public void OnChatCancelButton()
    {
        _chatPanel.SetActive(false);
    }

    public void OnChatSendButton()
    {
        _messageText.text = _chatInputField.text;
        _chatInputField.text = "";
        _chatPanel.SetActive(false);
        _messagePanel.SetActive(true);
    }

    public void OnCampsButtonClicked()
    {
        SceneManager.LoadScene("Home");
    }

    public void OnMapButtonClicked()
    {
        SceneManager.LoadScene("test_maps");
    }

    private void OnDisable() 
    {
        if (ResourceManager.Instance != null)
            ResourceManager.Instance.OnResourceChanged -= UpdateResourceText;
    }
    
}
