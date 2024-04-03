using System;
using UnityEngine;

public class FollowChange : MonoBehaviour
{
    [SerializeField] private bool followX;
    [SerializeField] private bool followY;

    public void ChangeFollow()
    {
        CameraFollow.FollowX = followX;
        CameraFollow.FollowY = followY;
    }
}