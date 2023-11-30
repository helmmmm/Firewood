using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUI : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("Home");
    }
}
