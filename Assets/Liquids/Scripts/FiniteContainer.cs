using UnityEngine;


// Particular container with a limited amount of liquid stored in it
public class FiniteContainer : InfiniteContainer
{
    #region Class members
    // If the container can pour its content. Useful for storage only containrs
    public bool pouringAllowed;
    #endregion


    #region Mono behaviour callbacks
    // Start is called before the first frame update
    void Start()
    {
        liquid = liquidContent.GetComponent<Liquid>();
        GenerateLiquid();
    }

    // Update is called once per frame
    void Update()
    {
        //_Same as the infinite container with a check added for storage-only containers
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
    #endregion


    #region Liquid management
    // Set the properties of the liquid : max volume allowed and set an empty container
    protected new void GenerateLiquid()
    {
        liquid.filling = Liquid.minFilling;
        liquid.maxVolume = innerVolume;
    }

    // Wraps the call to the liquid method of the same name
    public override void AddLiquid(float volume, Color colour)
    {
        liquid.AddLiquid(volume, colour);
    }
    #endregion
}
