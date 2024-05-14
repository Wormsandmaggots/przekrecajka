using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDoDamage
{
    [SerializeField] private int damage;
    
    public int getDamage()
    {
        return damage;
    }
}
