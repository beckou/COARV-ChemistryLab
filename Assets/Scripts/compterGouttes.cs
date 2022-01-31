using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compterGouttes : MonoBehaviour
{
    public int compteurGoutte1 = 0;
    public int compteurGoutte2 = 0;
    public int compteurGoutte3 = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //si l'objet collidé est une goutte
        if (other.gameObject.layer == 9)
        {
            //on incrémente le compteur de goutte dans la verrerie en fonction de la goutte
            switch (other.gameObject.tag)
            {
                case "goutteFlasque1":
                    compteurGoutte1 += 1;
                    break;
                case "goutteFlasque2":
                    compteurGoutte2 += 1;
                    break;
                case "goutteFlasque3":
                    compteurGoutte3 += 1;
                    break;
            }
            //Et on détruit cette goutte pour la faire disparaitre
            Destroy(other.gameObject);
        }
    }
}
