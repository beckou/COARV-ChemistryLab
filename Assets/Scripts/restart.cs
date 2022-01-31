using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{

    public Leap.Unity.Interaction.InteractionButton interactionButton;
    
    // Update is called once per frame
    void Update()
    {
        if (interactionButton.isPressed)
        {
            //Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Done :P");
        }
        
    }
}
