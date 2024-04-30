using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Accelerometr : MonoBehaviour
{
    private static bool Active = false;
    [SerializeField] private Vector2 accelerometrPower = new Vector2(1,1);
    [SerializeField] private float earthGravityValue = 9.8f;
    [SerializeField] private float activateAccelerometrThreshold = 1f;
    [SerializeField] private float activeateDelay = 0.5f;
    [SerializeField] private float smooth = 0.5f;
    private Vector2 prevAcceleration;
    private UnityEvent<Vector2, Vector2> onAccelerometrActivate = new UnityEvent<Vector2, Vector2>();
    private RigidbodyBoneHolder rbbh;
    private Vector2 initialAcceleration;

    private void OnEnable()
    {
        rbbh = FindAnyObjectByType<RigidbodyBoneHolder>();
        onAccelerometrActivate.AddListener(rbbh.PushBones);
    }

    private void OnDisable()
    {
        onAccelerometrActivate.RemoveAllListeners();
    }

    private void Start()
    {
        initialAcceleration = Input.acceleration;
    }

    private void Update()
    {
        Vector2 acc = new Vector2(Input.acceleration.x - initialAcceleration.x, Input.acceleration.y - initialAcceleration.y);
        //Vector2 acc = new Vector2(Input.gyro.userAcceleration.x - initialAcceleration.x, Input.gyro.userAcceleration.y - initialAcceleration.y);

        //Vector2 acc = Vector2.Lerp(prevAcceleration, Input.acceleration - (Vector3)initialAcceleration, Time.deltaTime/smooth);
        //acc.y += earthGravityValue * 0.1f;

        acc.x = -acc.x;
        acc.y = -acc.y;
        
        if (Mathf.Abs(acc.x) - Mathf.Abs(prevAcceleration.x) < activateAccelerometrThreshold)
        {
            acc.x = 0;
        }
        
        if (Mathf.Abs(acc.y) - Mathf.Abs(prevAcceleration.y) < activateAccelerometrThreshold)
        {
            acc.y = 0;
        }
        
        if (!Active && 
            Vector2.Distance(prevAcceleration, acc) > activateAccelerometrThreshold)
        {
            //acc.x = -acc.x;
            Debug.Log(acc);
            StartCoroutine(ActivateAccelerometr((acc).normalized));
        }

        prevAcceleration = acc;
    }

    private IEnumerator ActivateAccelerometr(Vector2 dir)
    {
        Active = true;
        onAccelerometrActivate.Invoke(dir, accelerometrPower);
        yield return new WaitForSeconds(activeateDelay);

        Active = false;
    }
}
