using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInstructions : MonoBehaviour
{
    [SerializeField] private float _time = 33f;
    Canvas obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Canvas>();
        //_renderer = GetComponent<Renderer>();
        //_renderer.enabled = false;
        obj.enabled = false;
        StartCoroutine(AppearTextInstruction(_time));
    }

    public IEnumerator AppearTextInstruction(float t)
    {
        yield return new WaitForSeconds(t);
        obj.enabled = true;
    }
}
