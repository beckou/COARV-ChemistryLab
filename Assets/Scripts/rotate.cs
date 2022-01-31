using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    // Use this for initialization
    public float smooth = 1f;
    private Quaternion targetRotation;
    float angle = 0.1f;
    float time;
    float timeLeft;
    void Start()
    {
        targetRotation = transform.rotation;
        time = CountDown.time;
    }

    void Update()
    {
        timeLeft = CountDown.timeLeft;

        angle = (30f / timeLeft);
        //targetRotation *= Quaternion.AngleAxis(Mathf.RoundToInt(angle), Vector3.up);
        targetRotation *= Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth); //* Time.deltaTime);
    }
}
