using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Accelerometr : MonoBehaviour
{
    public static bool CanDash = true;
    private static bool Active = false;
    [SerializeField] private Vector2 accelerometrPower = new Vector2(1000,1000);
    [SerializeField] private float activateAccelerometrThreshold = 1f;
    [SerializeField] private Vector2 activateDirectionThreshold = new Vector2(1, 1);
    [SerializeField] private Vector2 unintentionalDirectionActivationGapMultiplier = new Vector2(1.5f, 1.5f);
    [SerializeField] private Vector2 minimalAccelerationThreshold = new Vector2(1, 1);
    [SerializeField] private float activeateDelay = 0.5f;
    [SerializeField] private Vector2 gyroDelta = new Vector2(1f, 1.5f);
    [SerializeField] private bool accelerometer = true;
    private float earthGravityValue = 9.807f;
    private Vector2 prevAcceleration;
    private Vector2 prevGyro;
    public static UnityEvent<Vector2, Vector2> onAccelerometrActivate = new UnityEvent<Vector2, Vector2>();
    private Vector2 initialAcceleration;

    private void OnEnable()
    {
        onAccelerometrActivate.AddListener(FindAnyObjectByType<RigidbodyBoneHolder>().PushBones);
        Input.compensateSensors = true;
    }

    private void OnDisable()
    {
        onAccelerometrActivate.RemoveAllListeners();
    }

    private void Update()
    {
        if(!CanDash) return;
        
        float g = earthGravityValue * 0.1f;
        Vector3 gyro = Input.gyro.gravity;
        Vector3 gyroRate = Input.gyro.rotationRate;
        Vector2 acc = new Vector2(Input.acceleration.x, Input.acceleration.y);
        Vector2 unchangedAcc = Vector2.zero;

        if (accelerometer)
        {
            if (gyro.y <= 0)
            {
                acc.y += g;
                acc.y = -acc.y;
            }
            else
            {
                acc.y -= g;
                acc.y = -acc.y;
            }
            // else
            // {
            //     acc.y -= g;
            //     Debug.Log("dupa");
            // }

            acc.x = -acc.x;

            unchangedAcc = acc;


            if (Mathf.Abs(acc.x) <= minimalAccelerationThreshold.x ||
                Mathf.Abs(acc.x) <= activateDirectionThreshold.x)
            {
                acc.x = 0;
            }

            if (Mathf.Abs(acc.y) <= minimalAccelerationThreshold.y ||
                Mathf.Abs(acc.y) <= activateDirectionThreshold.y)
            {
                acc.y = 0;
            }

            if (Mathf.Abs(acc.x) * unintentionalDirectionActivationGapMultiplier.x < Mathf.Abs(acc.y))
            {
                acc.x = 0;
            }

            if (Mathf.Abs(acc.y) * unintentionalDirectionActivationGapMultiplier.y < Mathf.Abs(acc.x))
            {
                acc.y = 0;
            }

            if (Mathf.Abs(gyroRate.x) > gyroDelta.y)
            {
                acc.y = 0;
            }

            if (Mathf.Abs(gyroRate.y) > gyroDelta.x)
            {
                acc.x = 0;
            }
        }
        else
        {
            acc.y = -gyroRate.x;
            acc.x = gyroRate.y;

            if (Mathf.Abs(acc.x) <= activateDirectionThreshold.x)
            {
                acc.x = 0;
            }

            if (Mathf.Abs(acc.y) <= activateDirectionThreshold.y)
            {
                acc.y = 0;
            }
        }
        
        if (!Active && acc.magnitude >= activateAccelerometrThreshold)
        {
            StartCoroutine(ActivateAccelerometr(acc.normalized));
        }
        else
        {
            prevAcceleration = unchangedAcc;   
        }

        prevGyro = gyro;
    }

    private IEnumerator ActivateAccelerometr(Vector2 dir)
    {
        Active = true;
        onAccelerometrActivate.Invoke(dir, accelerometrPower);
        
        yield return new WaitForSeconds(activeateDelay);

        Active = false;
    }
}
