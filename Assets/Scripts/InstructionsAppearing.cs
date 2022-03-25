using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstructionsAppearing : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private float _time = 30f;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = false;
        StartCoroutine(AppearInstruction(_time));
    }

    public IEnumerator AppearInstruction(float t)
    {
        yield return new WaitForSeconds(t);
        _renderer.enabled = true;
    }
}
