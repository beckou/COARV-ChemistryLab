using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLaboIntroInt : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("introScene");
    }
}
