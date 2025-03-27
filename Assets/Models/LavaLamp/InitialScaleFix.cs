using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScaleFix : MonoBehaviour
{
    public Vector3 initialScale = Vector3.one;

    void Start()
    {
        transform.localScale = initialScale;
    }
}

