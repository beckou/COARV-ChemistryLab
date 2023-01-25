using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstructionsScript : MonoBehaviour
{
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayDelayed(31.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
