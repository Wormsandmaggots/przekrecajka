using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static LayerMask platforms;
    public static GameObject heart;
    public static LayerMask bubbleHolder;
    public static CameraFollow cameraFollow;
    public static GameObject player;
    public static Vector3 playerStartPos;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private LayerMask bubbleHolderLayer;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        heart = heartPrefab;
        platforms = platformLayer;
        bubbleHolder = bubbleHolderLayer;
        cameraFollow = FindAnyObjectByType<CameraFollow>();
        player = playerPrefab;
        playerStartPos = FindAnyObjectByType<Player>().transform.position;
    }
}
