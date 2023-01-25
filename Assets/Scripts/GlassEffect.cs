using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassEffect : MonoBehaviour
{
    AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            clip.Play();
        }
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 10)
        {
            clip.Play();
        }
    }

}
