using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, ICollectable
{
    [SerializeField] private int healValue = 1;
    private Collider2D collider;
    private float collectDelay = 2f;
    private bool canCollect = false;
    
    private void Start()
    {
        collider = GetComponentsInChildren<Collider2D>()[1];
        collider.enabled = false;
        StartCoroutine(Delay());
    }

    public void Collect(Player player)
    {
        if (!canCollect) return;
        
        player.Health += healValue;
        Destroy(gameObject);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(collectDelay);

        collider.enabled = true;
        canCollect = true;
    }
}
