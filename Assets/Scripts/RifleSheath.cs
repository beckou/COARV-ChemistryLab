using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleSheath : MonoBehaviour
{
    [SerializeField]
    GameObject rifle;

    [SerializeField]
    GameObject parent;
    private void OnEnable()
    {
        rifle.transform.parent = parent.transform;
    }
}
