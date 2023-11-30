using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    public void OnMapButtonClicked()
    {
        SceneManager.LoadScene("test_maps");
    }

    public void OnCampsButtonClicked()
    {
        SceneManager.LoadScene("Home");
    }
}
