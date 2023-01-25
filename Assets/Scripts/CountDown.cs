using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public GameObject equationContainer;
    protected equation1 equation;
    public static float timeLeft;
    public static float time = 100;
    AudioSource audioClock;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = "\n" + Mathf.Round(timeLeft);
        timeLeft = time;
        audioClock = GameObject.Find("Audio Clock").GetComponent<AudioSource>();
    }

    private void Start()
    {
        equation = equationContainer.GetComponent<equation1>();
    }

    void Update()
    {

        if (timeLeft < 0)
        {
            text.text = "\n Time Over !!!";
            audioClock.Stop();
            SceneManager.LoadScene("defaiteScene");
            //Application.LoadLevel("gameOver");
        }
        else
        {
            timeLeft -= Time.deltaTime;
            text.text = "\n" + Mathf.Round(timeLeft);
            if(equation.resolue)
            {
                SceneManager.LoadScene("victoireScene");
            }
        }
    }
}
