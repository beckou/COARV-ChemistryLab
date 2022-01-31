using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLaboIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        //Debug.LogError("Scene not loaded");
        SceneManager.LoadScene("laboScene");
    }
}
