using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static LayerMask platforms;
    [SerializeField] private LayerMask platformLayer;

    private void Start()
    {
        platforms = platformLayer;
    }
}
