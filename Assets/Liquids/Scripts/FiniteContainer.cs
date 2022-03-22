using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteContainer : InfiniteContainer
{
    public bool pouringAllowed;
    // Start is called before the first frame update
    void Start()
    {
        liquid = liquidContent.GetComponent<Liquid>();
        GenerateLiquid();
    }

    // Update is called once per frame
    void Update()
    {
        if(pouringAllowed)
        {
            bool shouldPour = TiltAngle() > thresholdAngle;

            if (isPouring != shouldPour)
            {
                isPouring = shouldPour;

                if (!isPouring)
                {
                    StartPouring();
                }
                else
                {
                    StopPouring();
                }
            }
        }
    }

    protected new void GenerateLiquid()
    {
        liquid.filling = Liquid.minFilling;
        liquid.maxVolume = innerVolume;
    }

    public override void AddLiquid(float volume, Color colour)
    {
        liquid.AddLiquid(volume, colour);
    }
}
