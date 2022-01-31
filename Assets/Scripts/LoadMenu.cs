using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    void OnEnable()
    {
        //Debug.LogError("Scene not loaded");
        SceneManager.LoadScene("Menu");
    }
}
