using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform what;

    private void LateUpdate()
    {
        Vector3 pos = what.position;
        pos.z = transform.position.z;

        transform.position = pos;
    }
}
