using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public const float maxFilling = 1;

    public const float minFilling = 0;

    public float filling { get; set; }

    public float maxVolume { get; set; }

    protected float currentVolume;

    protected bool containsLiquid;

    protected IDictionary<Color, float> containedLiquids;

    protected int nbLiquids;

    protected Color currentColour;

    protected readonly Color neutralColour = new Color(0, 0, 0, 0);

    protected Material material;

    protected Bounds bounds;

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

    protected void ClampFilling()
    {
        filling = Mathf.Clamp(currentVolume / maxVolume, minFilling, maxFilling);
    }

    public void RenderLiquid()
    {
        currentColour = neutralColour;
        foreach (KeyValuePair<Color, float> entry in containedLiquids)
        {
            currentColour += entry.Key * entry.Value / currentVolume;
        }
        //transform.localScale = filling * Vector3.one;
        //GetComponent<Renderer>().material.color = currentColour;
        material.SetColor("_Color", currentColour);
        material.SetVector("_Plane", CalculatePlane());
    }

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

    protected Vector4 CalculatePlane()
    {
        float liquidHeight = -bounds.extents.y + filling * bounds.size.y;
        Plane plane = new Plane(transform.up, transform.position + liquidHeight * Vector3.up);

        Vector4 planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
        return planeRepresentation;
    }
}
