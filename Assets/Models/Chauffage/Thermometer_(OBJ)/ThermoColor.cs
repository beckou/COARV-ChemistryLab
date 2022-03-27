using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermoColor : MonoBehaviour
{
    public GameObject aColorer;
    public Renderer myRenderer;
    public Color myColor;
    public float coloration_rouge;
    public float position_max;
    public float position_min;
    // Start is called before the first frame update
    void Start()
    {
        aColorer = GameObject.Find("Heating Plate");
        myRenderer = aColorer.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position.z);
        coloration_rouge = (gameObject.transform.position.z - position_min) / (position_max - position_min);// float entre 0 et 1
        myColor = new Color(coloration_rouge, 0.0f, 0.0f, 0.0f);
        gameObject.GetComponent<Renderer>().material.color = myColor;
        myRenderer.material.color = myColor;
    }
}
