using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, ICollectable
{
    [SerializeField] private float healValue = 1;
    
    public void Collect(Player player)
    {
        player.Health += healValue;
        Debug.Log("Heal");
        Destroy(gameObject);
    }
}
