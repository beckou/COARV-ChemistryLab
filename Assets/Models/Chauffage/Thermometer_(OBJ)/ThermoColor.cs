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
    public string nomCible = "Heating Plate"; //l'objet ou le liquide que l'on souhaite colorer

    // Start is called before the first frame update
    void Start()
    {
        aColorer = GameObject.Find(nomCible);
        myRenderer = aColorer.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // lerp the color between min and max position
        coloration_rouge = (gameObject.transform.position.y - position_min) / (position_max - position_min);// float entre 0 et 1
        myColor = new Color(0.5f+ coloration_rouge /2, 0.6f, 0.6f, 0.0f);
        gameObject.GetComponent<Renderer>().material.color = myColor; //on colorie le curseur
        myRenderer.material.color = myColor; //on colorie la cible
    }
}
