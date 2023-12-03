using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR.ARFoundation;
using Niantic.Lightship.AR.Semantics;
using UnityEngine.UI;
using TMPro;

public class SemanticQuerying : MonoBehaviour
{
    public ARSemanticSegmentationManager _semanticManager;
    private string _currentSemantic = "";
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private Canvas _canvas;
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_semanticManager.subsystem.running)
        {
            return;
        }

        var list = _semanticManager.GetChannelNamesAt(
            Screen.width / 2, 
            Screen.height / 2);

        if (list.Contains("water"))
        {
            _currentSemantic = "water";
            _promptText.text = "Tap to catch fish!";
        }
        else if (list.Contains("ground"))
        {
            _currentSemantic = "ground";
            _promptText.text = "Tap to dig up some coins!";
        }
        else if (list.Contains("foliage"))
        {
            _currentSemantic = "foliage";
            _promptText.text = "Tap to pick up wood!";
        }
        else 
        {
            _currentSemantic = "";
            _promptText.text = "Nothing there. Point somewhere else!";
        }


        // When the user taps the screen, determine the channel name in that moment
        if (Input.GetMouseButtonDown(0) && _currentSemantic != "")
        {
            switch (_currentSemantic)
            {
                case "water":
                    ResourceManager.Instance.AddResource(ResourceManager.ResourceTypes.Food, 1);
                    break;
                case "ground":
                    ResourceManager.Instance.AddResource(ResourceManager.ResourceTypes.Coin, 1);
                    break;
                case "foliage":
                    ResourceManager.Instance.AddResource(ResourceManager.ResourceTypes.Wood, 1);
                    break;
            }
        }

    
    }

}
