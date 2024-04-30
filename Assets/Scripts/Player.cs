using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    private Rigidbody2D rigidbody2d;
    private float health;
    
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDoDamage doDamage))
        {
            Health -= doDamage.getDamage();
        }
        else if(other.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(this);
        }
        else if(other.gameObject.TryGetComponent(out FollowChange followChange))
        {
            followChange.ChangeFollow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDoDamage doDamage))
        {
            Health -= doDamage.getDamage();
        }
        else if(other.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(this);
        }
        else if(other.gameObject.TryGetComponent(out FollowChange followChange))
        {
            followChange.ChangeFollow();
        }
    }

    public void PushPlayer(Vector2 dir, Vector2 power)
    {
        rigidbody2d.AddForce(dir * power);
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;

            if (health <= 0)
            {
                Debug.Log("Player is dead");
            }

            if (health > 0)
            {
                health = maxHealth;
            }
        }
    }
}
