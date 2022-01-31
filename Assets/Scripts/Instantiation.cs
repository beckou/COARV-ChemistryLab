using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour {
    public GameObject go;
    public ParticleSystem particles;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Dot(go.transform.up, Vector3.up) < 0)
        {
            particles.Emit(1);
        } else
        {
            //particles.Pause();
            //particles.Clear();
            particles.Stop();
        }


	}
}
