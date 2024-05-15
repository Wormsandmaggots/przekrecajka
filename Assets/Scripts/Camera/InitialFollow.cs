using System;
using UnityEngine;

public class InitialFollow : MonoBehaviour
{
    [SerializeField] private bool followX = false;
    [SerializeField] private bool followY = false;
    private void Start()
    { 
        CameraFollow.FollowX = followX; 
        CameraFollow.FollowY = followY;
    }
}
