using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static LayerMask platforms;
    public static GameObject heart;
    public static LayerMask bubbleHolder;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask bubbleHolderLayer;
    [SerializeField] private GameObject heartPrefab;

    private void Start()
    {
        heart = heartPrefab;
        platforms = platformLayer;
        bubbleHolder = bubbleHolderLayer;
    }
}
