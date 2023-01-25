using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Manages the rendering of a liquid in a container
public class Liquid : MonoBehaviour
{
    #region Class members
    // Max proportion of the volume allowed to be filled
    public const float maxFilling = 1;

    // Min proportion of the volume allowed to be filled
    public const float minFilling = 0;

    // Current filled proportion of the container
    public float filling { get; set; }

    // Maximum liquid volume
    public float maxVolume { get; set; }

    // Current liquid volume
    protected float currentVolume;

    // If there is liquid or not
    protected bool containsLiquid;

    // List of the contained liquids (sorted by colour)
    protected IDictionary<Color, float> containedLiquids;

    // Number of different liquids (again by colour)
    protected int nbLiquids;

    // Colour of the mix of the liquids
    protected Color currentColour;

    // Base colour of the liquid
    protected readonly Color neutralColour = new Color(0, 0, 0, 0);

    // Material with the custom shader allowing game objects clipping
    protected Material material;

    // Dimensions of the mesh
    protected Bounds bounds;


    public IDictionary<Color, float> ContainedLiquids
    {
        get{ return containedLiquids; }
    }
    #endregion


    # region Mono behaviour callbacks

    // Instantiate the objects
    private void Awake()
    {
        containedLiquids = new Dictionary<Color, float>();
        material = GetComponent<Renderer>().material;
        bounds = GetComponent<MeshRenderer>().bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        nbLiquids = 0;
        filling = minFilling;
        maxVolume = 1;
        currentVolume = maxVolume * filling;
    }

    // Update is called once per frame
    void Update()
    {
        ClampFilling();
        RenderLiquid();
    }
    #endregion


    #region Internal methods
    // Make sure the amount of liquid is below the max
    protected void ClampFilling()
    {
        filling = Mathf.Clamp(currentVolume / maxVolume, minFilling, maxFilling);
    }

    // Determine the colour of the rendered liquid and the portion of the base GameObject to be clipped 
    protected void RenderLiquid()
    {
        currentColour = neutralColour;
        foreach (KeyValuePair<Color, float> entry in containedLiquids)
        {
            currentColour += entry.Key * entry.Value / currentVolume;
        }
        material.SetColor("_Color", currentColour);
        material.SetVector("_Plane", CalculatePlane());
    }

    // Add a certain volume of liquid to the collection, create a new one if it didn’t exist before
    public void AddLiquid(float volume, Color colour)
    {
        if(currentVolume + volume < maxVolume)
        {
           if(containedLiquids.ContainsKey(colour))
            {
                containedLiquids[colour] += volume;
            } else
            {
                nbLiquids++;
                containedLiquids.Add(colour, volume);
            }
            currentVolume += volume;
        }
    }

    // Calculate the plane used for clipping
    protected Vector4 CalculatePlane()
    {
        // Liquid height taken from the bottom of the container
        float liquidHeight = -bounds.extents.y + filling * bounds.size.y;
        // Horizontal (locally) plane representing the surface of the liquid
        // Coordinates are in the local referential as the transformation to world coordinates is done in the shader
        Plane plane = new Plane(transform.up, transform.position + liquidHeight * Vector3.up);

        Vector4 planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
        return planeRepresentation;
    }
    #endregion
}
