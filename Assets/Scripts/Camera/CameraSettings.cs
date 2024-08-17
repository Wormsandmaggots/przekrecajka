using System;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Vector2 referenceResolution;

    private void Start()
    {
        cam = Camera.main;
        Resolution res = Screen.currentResolution;

        cam.orthographicSize = res.height / res.width * referenceResolution.y / referenceResolution.x;
    }
}
