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
    private Player player;
    private Vector2 initialAcceleration;
    //private Accelerometer accelerometer;

    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>();
        onAccelerometrActivate.AddListener(player.PushPlayer);
        //InputSystem.EnableDevice(Accelerometer.current);
    }

    private void OnDisable()
    {
        onAccelerometrActivate.RemoveAllListeners();
        //InputSystem.DisableDevice(Accelerometer.current);
    }

    private void Start()
    {
        //accelerometer = Accelerometer.current;
        initialAcceleration = Input.acceleration;
    }

    private void Update()
    {
        //Vector2 acc = new Vector2(Input.acceleration.x, -Input.acceleration.y);
        //Vector2 acc = new Vector2(Input.gyro.userAcceleration.x - initialAcceleration.x, Input.gyro.userAcceleration.y - initialAcceleration.y);

        Vector2 acc = Vector2.Lerp(prevAcceleration, Input.acceleration - (Vector3)initialAcceleration, Time.deltaTime/smooth);
        //acc.y += earthGravityValue * 0.1f;
        
        if (!Active && 
            Vector2.Distance(prevAcceleration, acc) > activateAccelerometrThreshold)
        {
            Debug.Log("current = " + acc + "\n prev = " + prevAcceleration); 
            StartCoroutine(ActivateAccelerometr((-acc).normalized));
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
