using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum AcceleratorType
{
    WIND,
    ACCELERATOR
}

[ExecuteAlways]
public class Accelerator : MonoBehaviour
{
    [SerializeField] private Vector2 direction = new Vector2(0,1);
    [SerializeField] private Vector2 position = Vector2.zero;
    [SerializeField] private Vector2 triggerCenter = Vector2.zero;
    [SerializeField] private Vector2 force = new Vector2(1,1);
    [SerializeField] private AcceleratorType accelerationType = AcceleratorType.WIND;

#if UNITY_EDITOR
    
    private void Start()
    {
        position = transform.position;
        triggerCenter = GetComponent<Collider2D>().offset;
    }
    
    private void Update()
    {
        position = transform.position;
        triggerCenter = GetComponent<Collider2D>().offset;
    }
    
#endif

    public Vector2 GetForce()
    {
        return force;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public AcceleratorType GetAccelerationType()
    {
        return accelerationType;
    }
}
