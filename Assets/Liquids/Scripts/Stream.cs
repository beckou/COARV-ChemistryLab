using System.Collections;
using UnityEngine;


// Stream of liquid pouring out of a container. Manages the animation, the splash particles and makes the stream follow the 
// container movements
public class Stream : MonoBehaviour
{
    #region Class members
    // Liquid output
    public float Output { get; set; }

    // Colour of the liquid
    public Color Colour { get; set; }

    // Shortcut for the stream renderer, a LineRenderer
    private LineRenderer lineRenderer;

    // Shortcut for the splash particles
    private ParticleSystem splashes;

    // The current animation coroutine
    private Coroutine pourRoutine;

    // Where the stream will stop falling
    private Vector3 targetPosition;

    // How different the splash particles colour can be from the liquid colour, adds contrast to the particles. It’s prettier.
    private float randomColourModifier = 1.2f;

    // Shortcut for Time.deltaTime used to calculate the volume of liquid added to an eventual container in a frame
    private float dT;

    // How fast the liquid falls to the ground when it starts and stops falling, in custom units
    private const float fallSpeed = 1.75f;
    #endregion


    #region Mono behaviour callbacks
    // Instantiating the objects
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashes = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Puts the start and end position of the stream at the entrance of the container
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);

        SetLineRendererProperties();

        SetParticleProperties();
    }

    // Update is called once per frame
    void Update()
    {
        dT = Time.deltaTime;
    }
    #endregion


    #region Stream animations
    // Starts the stream and particle animation coroutines
    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPouring());
    }

    // Animation start coroutine : make the end of the stream fall to the ground and the stream follow the movements of the source
    private IEnumerator BeginPouring()
    {
        while(gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }
    }

    // Stops the stream start animation coroutine and starts the stream stop animation coroutine (the beginning of the stream falls to the ground)
    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(StopPouring());
    }

    // Animation stop coroutine : the beginning of the stream falls to the ground and the stream object (this) is destroyed
    private IEnumerator StopPouring()
    {
        while(!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);
            yield return null;
        }

        Destroy(gameObject);
    }
    #endregion


    #region Stream position calculation

    // Casts a ray downward to calculate the fall point of the stream
    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, float.MaxValue);

        // Notify the collider if it’s a container
        AddLiquidToPotentialContainer(hit.collider);

        return hit.collider ? hit.point : ray.GetPoint(float.MaxValue);
    }

    // Update the positions of the beginning and the end of the LineRenderer modelling the stream
    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    // Progressively moves the beginning or the end of the stream to the desired position (used at the start and the end of the animation)
    // This is different from the flowing effect which is done with a shader moving a texture along the stream
    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * fallSpeed);
        lineRenderer.SetPosition(index, newPosition);
    }

    // Simply checks wether the given extremity of the stream has reached a position or not
    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        return targetPosition == lineRenderer.GetPosition(index);
    }

    #endregion


    #region Particles management

    // Gives a second (random) colour to the splash particles
    private void SetParticleProperties()
    {
        ParticleSystem.MainModule mainModule = splashes.main;
        Color secondColour = new Color(RandomAround(0, 1, Colour.r, randomColourModifier), RandomAround(0, 1, Colour.g, randomColourModifier), RandomAround(0, 1, Colour.b, randomColourModifier), 1);
        mainModule.startColor = new ParticleSystem.MinMaxGradient(Colour, secondColour);
    }

    // Make the splash particles stick to the fall point of the stream and hide them if no liquid is pouring
    private IEnumerator UpdateParticle()
    {
        while(gameObject.activeSelf)
        {
            splashes.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashes.gameObject.SetActive(isHitting);
            yield return null;
        }
    }
    #endregion


    #region Liquid management
    // If a collision occurs with a container we add a small amount of liquid to it
    private void AddLiquidToPotentialContainer(Collider collider)
    {
        if (collider && collider.transform.parent && collider.transform.parent.GetComponentInChildren<InfiniteContainer>())
        {
            collider.transform.parent.GetComponentInChildren<InfiniteContainer>().AddLiquid(Output * dT, Colour);
        }
    }

    #endregion


    #region Internal methods

    // Sets the colour and the width of the stream.
    private void SetLineRendererProperties()
    {
        lineRenderer.startColor = Colour;
        lineRenderer.endColor = Colour;

        lineRenderer.startWidth /= 30;
        lineRenderer.endWidth /= 30;
    }

    // Calculate a random value around the given one and clamps it to an interval
    private float RandomAround(float min, float max, float value, float delta)
    {
        value += Random.Range(-delta, delta);
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }

    #endregion
}
