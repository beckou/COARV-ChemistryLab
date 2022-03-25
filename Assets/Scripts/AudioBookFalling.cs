using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBookFalling : MonoBehaviour
{
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayDelayed(60.0f);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
