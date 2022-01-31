using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoaderScript : MonoBehaviour
{

    public Animator transitionBis;
    public float transitionTimeBis = 1f;
    protected string sceneName = "Menu";
    //public Leap.Unity.Interaction.InteractionButton interactionButton;



    public void LoadMenu()
    {
        StartCoroutine(LoadSceneMenu(sceneName));
        Debug.Log("Je fais bien ce truc");
    }

    IEnumerator LoadSceneMenu(string nameS)
    {
        //Play animation
        transitionBis.SetTrigger("Start");

        //Wait enough time to see the crossfade
        yield return new WaitForSeconds(transitionTimeBis);

        //Load the scene
        SceneManager.LoadScene(nameS);
    }
}
