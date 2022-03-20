using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    public float Output { get; set; }

    public Color Colour { get; set; }

    private LineRenderer lineRenderer;
    private ParticleSystem splashes;

    private Coroutine pourRoutine;

    private Vector3 targetPosition;


    private float dT;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashes = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);

        lineRenderer.startColor = Colour;
        lineRenderer.endColor = Colour;
        splashes.startColor = Colour;

        lineRenderer.startWidth /= 30;
        lineRenderer.endWidth /= 30;
    }

    // Update is called once per frame
    void Update()
    {
        dT = Time.deltaTime;
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPouring());
    }

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


    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(StopPouring());
    }

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

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, float.MaxValue);

        AddLiquidToPotentialContainer(hit.collider);

        return hit.collider ? hit.point : ray.GetPoint(float.MaxValue);
    }

    private void AddLiquidToPotentialContainer(Collider collider)
    {
        if (collider && collider.gameObject.GetComponent<InfiniteContainer>())
        {
            collider.gameObject.GetComponent<InfiniteContainer>().AddLiquid(Output * dT, Colour);
        }
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
        return targetPosition == lineRenderer.GetPosition(index);
    }

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
}
