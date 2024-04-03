using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static bool FollowX = true;
    public static bool FollowY = true;
    [SerializeField] private Transform what;

    private void LateUpdate()
    {
        Vector3 pos = what.position;
        Vector3 cameraPos = transform.position;
        
        pos.z = cameraPos.z;

        if (!FollowX)
        {
            pos.x = cameraPos.x;
        }

        if (!FollowY)
        {
            pos.y = cameraPos.y;
        }
        
        transform.position = pos;
    }
}
