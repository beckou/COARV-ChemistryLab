using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteContainer : MonoBehaviour
{
    public int thresholdAngle;

    public GameObject streamObject;

    public float maxOutput;

    public Color liquidColour;

    private bool isPouring;

    private Transform model;

    private Stream currentStream;
    // Start is called before the first frame update
    void Start()
    {
        model = transform.parent;
        isPouring = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldPour = TiltAngle() < thresholdAngle;

        if(isPouring != shouldPour)
        {
            isPouring = shouldPour;

            if(isPouring)
            {
                StartPouring();
            } else
            {
                StopPouring();
            }
        }
    }

    protected void StartPouring()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    protected void StopPouring()
    {
        currentStream.End();
        currentStream = null;
    }

    protected float TiltAngle()
    {
        return model.up.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject stream = Instantiate(streamObject, transform.position, Quaternion.identity, model);
        Stream res = stream.GetComponent<Stream>();
        res.Colour = liquidColour;
        res.Output = maxOutput;
        return res;
    }

    public void AddLiquid(float volume, Color color)
    {

    }

}
