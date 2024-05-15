using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private int healthThreshold = 1;
    private Collider2D collider2D;
    
    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    public void SetColliderEnable(bool enable)
    {
        collider2D.enabled = enable;
    }

    public int HealthThreshold => healthThreshold;
}
