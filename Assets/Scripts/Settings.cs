using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static LayerMask platforms;
    public static Heart heart;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Heart heartPrefab;

    private void Start()
    {
        heart = heartPrefab;
        platforms = platformLayer;
    }
}
