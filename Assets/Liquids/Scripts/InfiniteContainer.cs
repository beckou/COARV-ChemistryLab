using UnityEngine;



// Allows a GameObject to contain liquids and adds management for pouring and storing it
public class InfiniteContainer : MonoBehaviour
{
    #region Class members
    // How much the container should be tilted before it starts pouring
    public int thresholdAngle;

    // Prefab giving the pouring effect
    public GameObject streamPrefab;

    // Eventual mesh representing the liquid filling the container
    public GameObject liquidContent;

    // Output of the container
    public float maxOutput;

    // Colour of the contained liquid
    public Color liquidColour;

    // Max volume of liquid
    public float innerVolume;

    // Shortcut for the liquid script
    protected Liquid liquid;

    // If the container is pouring its content or not
    protected bool isPouring;

    // Shortcut for the container 3d model
    protected Transform model;

    // Liquid stream created when the container is pouring
    protected Stream currentStream;
    #endregion


    #region Mono behaviour callbacks
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
    #endregion


    #region Stream management
    // Create the stream and wraps the call to the start of the stream starting animation
    protected void StartPouring()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }
    
    // Remove the stream and wraps the call to the end of the stream ending animation
    protected void StopPouring()
    {
        if(currentStream != null)
        {
            currentStream.End();
        }
        currentStream = null;
    }

    // Create the stream based on the prefab and the parameters (colour, output)
    protected Stream CreateStream()
    {
        GameObject stream = Instantiate(streamPrefab, transform.position, Quaternion.identity, model);
        Stream res = stream.GetComponent<Stream>();
        res.Colour = liquidColour;
        res.Output = maxOutput;
        return res;
    }
    #endregion


    #region Liquid management
    // Set the properties of the liquid (only colours for the infinite containers
    protected void GenerateLiquid()
    {
        if (liquidContent != null)
        {
            liquidContent.GetComponent<Renderer>().material.color = liquidColour;
        }
    }

    // Add liquid to the container. For infinite containers nothing happens
    public virtual void AddLiquid(float volume, Color colour)
    {

    }
    #endregion


    #region Internal methods
    // Return the tilt angle of the container model following the y axis
    protected float TiltAngle()
    {
        return model.up.y * Mathf.Rad2Deg;
    }
    #endregion

}
