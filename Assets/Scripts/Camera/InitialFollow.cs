using System;
using UnityEngine;

public class InitialFollow : MonoBehaviour
{
    public static bool FollowX, FollowY;
    [SerializeField] private bool followX = false;
    [SerializeField] private bool followY = false;
    private void Start()
    {
        FollowY = followY;
        FollowX = followX;
        
        CameraFollow.FollowX = followX; 
        CameraFollow.FollowY = followY;
    }
}
