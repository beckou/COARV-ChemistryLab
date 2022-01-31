using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verserLiquide : MonoBehaviour
{
    public GameObject goutte;
    float oldTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        oldTime = Time.fixedTime;
        //goutte = Instantiate(goutte, gameObject.transform); Pour tester
        //goutte.transform.Translate(new Vector3(0.0f, 0.3f, 0.0f));  
    }

    // Update is called once per frame
    void Update()
    {
        //executé toutes les 0.3 secondes
        if (Time.fixedTime - oldTime > 0.1)
        {
            //permet de recuperer les donnees de rotation en degré
            Vector3 rot = gameObject.transform.eulerAngles;
        
            //si la rotation suivant x ou z est plus grande(plus petite) que 90(-90), alors on verse du liquide
            if ( (rot.x >= 0 && rot.x % 360 > 90 && rot.x % 360 < 270) || (rot.x < 0 && rot.x % -360 < -90 && rot.x % -360> -270) || (rot.z >= 0 && rot.z % 360 > 90 && rot.z % 360 < 270) || (rot.z < 0 && rot.z % -360 < -90 && rot.z % -360 > -270))
            {
                var instanceGoutte = Instantiate(goutte, gameObject.transform.position, Quaternion.identity);
                //permet de faire apparaitre la goutte à l'entrée de la verrerie
                instanceGoutte.transform.Translate(0.28f * gameObject.transform.up);
                
                oldTime = Time.fixedTime;
            }
        }
        
        
    }
}
