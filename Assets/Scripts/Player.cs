using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 pushPower = new Vector2(50, 50);
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private float immunityTime = 0.5f;
    private bool immune = false;
    private RigidbodyBoneHolder rbh;
    private float health;
    
    private void Start()
    {
        rbh = GetComponent<RigidbodyBoneHolder>();
        health = maxHealth;
    }
    
    public void TriggerEnter(Collider2D c2d, PlayerMainBone pmb)
    {
        if (c2d.TryGetComponent(out IDoDamage doDamage))
        {
            if (!immune)
            {
                StartCoroutine(Immune());
                Health -= doDamage.getDamage();
            }
            
            Vector2 dir = pmb.transform.position - c2d.transform.position;
            dir = dir.normalized;
            rbh.PushBones(dir, pushPower);
        }
        else if(c2d.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(this);
        }
        else if(c2d.gameObject.TryGetComponent(out FollowChange followChange))
        {
            followChange.ChangeFollow();
        }
    }
    
    public void CollisionEnter(Collider2D c2d)
    {
        if (c2d.gameObject.TryGetComponent(out IDoDamage doDamage))
        {
            Health -= doDamage.getDamage();
            Vector2 dir = transform.position - c2d.transform.position;
            dir = dir.normalized;
            rbh.PushBones(dir, pushPower);
        }
    }

    private IEnumerator Immune()
    {
        immune = true;

        yield return new WaitForSeconds(immunityTime);

        immune = false;
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

            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }
}

// public void CollisionEnter(BoneFunctionality bf, Collider2D c2d)
// {
//     if (c2d.gameObject.TryGetComponent(out IDoDamage doDamage))
//     {
//         Health -= doDamage.getDamage();
//         Vector2 dir = transform.position - c2d.transform.position;
//         dir = dir.normalized;
//         rbh.PushBones(dir, pushPower);
//     }
// }
//
// public void TriggerEnter(BoneFunctionality bf, Collider2D c2d)
// {
//     if (c2d.TryGetComponent(out IDoDamage doDamage))
//     {
//         Health -= doDamage.getDamage();
//         Vector2 dir = transform.position - c2d.transform.position;
//         dir = dir.normalized;
//         rbh.PushBones(dir, pushPower);
//     }
//     else if(c2d.TryGetComponent(out ICollectable collectable))
//     {
//         collectable.Collect(this);
//     }
//     else if(c2d.gameObject.TryGetComponent(out FollowChange followChange))
//     {
//         followChange.ChangeFollow();
//     }
// }
