
 using System.Collections;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 
 public class LoadLabo : MonoBehaviour
{
    public float delay = 2;
    public string NewLevel = "introScene";
    public void loadIntro()
    {
        StartCoroutine(LoadLevelAfterDelay(delay));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        print("delay: "+delay);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(NewLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}