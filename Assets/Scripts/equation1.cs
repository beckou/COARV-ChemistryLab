﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equation1 : MonoBehaviour
{
    //permet de savoir si l'équation a été resolue ou non
    public bool resolue = false;

    public GameObject flask1;
    public GameObject flask2;
    public GameObject flask3;
    public GameObject contenant;

    // Contenu liquide dans le récipient
    protected Liquid liquidContent;
    // Volume de liquide minimal requis pour valider l’énigme
    protected const float minGoodLiquidVolume = 0.1f;
    protected const float maxBadLiquidVolume = 0.01f;

    // Start is called before the first frame update
    void Start()
    {


    }

    //Quand le script est enable (permet de choisir dans le futur quelle enigme faire apparaitre)
    private void OnEnable()
    {
        //changer le text du tableau pour afficher la consigne
        //UnityEngine.UI.Text menu = GameObject.FindGameObjectWithTag("menuToChange").GetComponent<UnityEngine.UI.Text>();
        //menu.text = "Un peu de bleu et de rose";

        //on instancie chaque fiole et le contenant
        GameObject table = gameObject;
        flask1 = Instantiate(flask1, table.transform);
        flask1.transform.Translate(new Vector3(0.0f, 1.297f, 0.0f));

        flask2 = Instantiate(flask2, table.transform);
        flask2.transform.Translate(new Vector3(-0.15f, 1.297f, 0.4f));

        flask3 = Instantiate(flask3, table.transform);
        flask3.transform.Translate(new Vector3(0.25f, 1.297f, 0.25f));

        contenant = Instantiate(contenant, table.transform);
        contenant.transform.Translate(new Vector3(0.25f, 1.297f, -0.5f));
        contenant.name = "contenant";

        liquidContent = contenant.GetComponentInChildren<Liquid>();
    }

    // Update is called once per frame
    void Update()
    {
        /*//On cherche le contenant pour vérifier le nombre de gouttes qu'il a reçu de chaque fiole
        GameObject contenant = GameObject.Find("contenant");
        compterGouttes compteGouttes = contenant.GetComponent<compterGouttes>();

        //condition arbitraire laissé à l'imagination pour déterminer quand l'énigme est resolue
        if (compteGouttes.compteurGoutte1 > 50 && compteGouttes.compteurGoutte2 > 50 && compteGouttes.compteurGoutte3 < 5)
        {
            resolue = true;
        }
        else
        {
            resolue = false;
        }*/


        // On vérifie si les liquides constituant le contenu liquide du récipient sont cohérents avec la condition de validation
        // Un liquide est caractérisé par sa couleur.
        bool liquideBleuOk = false;
        bool liquideRoseOk = false;
        bool liquideVertOk = true;

        foreach (KeyValuePair<Color, float> lv in liquidContent.ContainedLiquids)
        {
            if (lv.Key == flask1.GetComponentInChildren<InfiniteContainer>().liquidColour && lv.Value > minGoodLiquidVolume)
            {
                liquideBleuOk = true;
            }

            if (lv.Key == flask2.GetComponentInChildren<InfiniteContainer>().liquidColour && lv.Value > minGoodLiquidVolume)
            {
                liquideRoseOk = true;
            }

            if (lv.Key == flask3.GetComponentInChildren<InfiniteContainer>().liquidColour && lv.Value > maxBadLiquidVolume)
            {
                liquideVertOk = false;
            }
        }

        resolue = liquideBleuOk && liquideRoseOk && liquideVertOk;

    }
}
