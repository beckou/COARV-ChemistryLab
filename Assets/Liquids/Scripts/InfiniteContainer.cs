using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteContainer : MonoBehaviour
{
    public int thresholdAngle;

    public GameObject streamObject;

    public GameObject liquidContent;

    protected Liquid liquid;

    public float maxOutput;

    public Color liquidColour;

    public float innerVolume;

    protected bool isPouring;

    protected Transform model;

    protected Stream currentStream;
    // Start is called before the first frame update
    void Start()
    {
        model = transform.parent;
        isPouring = false;
        GenerateLiquid();
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldPour = TiltAngle() > thresholdAngle;

        if(isPouring != shouldPour)
        {
            isPouring = shouldPour;

            if(!isPouring)
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
        if(currentStream != null)
            currentStream.End();
        currentStream = null;
    }

    protected float TiltAngle()
    {
        return model.up.y * Mathf.Rad2Deg;
    }

    protected Stream CreateStream()
    {
        GameObject stream = Instantiate(streamObject, transform.position, Quaternion.identity, model);
        Stream res = stream.GetComponent<Stream>();
        res.Colour = liquidColour;
        res.Output = maxOutput;
        return res;
    }

    protected void GenerateLiquid()
    {
        //liquid.maxVolume = innerVolume;
        //liquid.AddLiquid(innerVolume, liquidColour);
        liquidContent.GetComponent<Renderer>().material.color = liquidColour;
    }

    public virtual void AddLiquid(float volume, Color colour)
    {
        
    }


}
