using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscope : MonoBehaviour
{
    [SerializeField] private float gravityPower = 2;
    void Awake()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.gravity = Input.gyro.gravity * gravityPower;
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
