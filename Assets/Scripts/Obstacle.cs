using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDoDamage
{
    [SerializeField] private float damage;
    
    public float getDamage()
    {
        return damage;
    }
}
