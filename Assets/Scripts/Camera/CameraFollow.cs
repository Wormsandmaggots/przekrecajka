using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector]public Transform what;
    public static Vector3 CameraStartPos;
    public static bool FollowX = true;
    public static bool FollowY = true;
    [SerializeField] private float e = 0.01f;
    [SerializeField] private float cameraSpeed = 2;
    private Vector3 currentVelocity;

    private void Start()
    {
        CameraStartPos = transform.position;
        what = FindAnyObjectByType<PlayerMainBone>().transform;
    }

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

        if (Vector3.Distance(cameraPos, pos) >= e)
        {
            transform.position = Vector3.SmoothDamp(cameraPos, pos, ref currentVelocity, Time.deltaTime);
            //transform.position = Vector3.Lerp(cameraPos, pos, Time.deltaTime * cameraSpeed);
            //transform.position = pos;
        }
    }
}
