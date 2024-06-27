using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector2 pushPower = new Vector2(50, 50);
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float immunityTime = 0.5f;
    [SerializeField] private float sizeMultiplier = 1.1f;
    private int health;
    private bool immune = true;
    private RigidbodyBoneHolder rbh;
    private PlayerMainBone playerMainBone;
    private Animator animator;
    private Vector2 dir;
    
    private void Start()
    {
        transform.position = Settings.playerStartPos;
        animator = GetComponent<Animator>();
        playerMainBone = GetComponentInChildren<PlayerMainBone>();
        rbh = GetComponent<RigidbodyBoneHolder>();
        StartCoroutine(Immune());
        Health = maxHealth;
        // else
        // {
        //     transform.localScale = Vector3.one + Vector3.one * health / 10f * sizeMultiplier;
        // }
    }

    public void TriggerEnter(Collider2D c2d, PlayerMainBone pmb)
    {
        if (c2d.TryGetComponent(out IDoDamage doDamage))
        {
            dir = pmb.transform.position - c2d.transform.position;
            dir = dir.normalized;
            //rbh.PushBones(dir, pushPower);

            //Vector2 random = new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized;
            
            AudioManager.instance.Play("damage");
            
            if (!immune)
            {
                StartCoroutine(Immune());
                Health -= doDamage.getDamage();
            }

            PushBones(dir, pushPower);
        }
        else if(c2d.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(this);
        }
        else if(c2d.TryGetComponent(out FollowChange followChange))
        {
            followChange.ChangeFollow();
        }
        else if(c2d.gameObject.TryGetComponent(out Accelerator acc))
        {
            if (acc.GetAccelerationType() == AcceleratorType.ACCELERATOR)
            {
                rbh.PushBones(acc.GetDirection(), acc.GetForce());
            }
        }
        else if(c2d.TryGetComponent(out Laser laser))
        {
            dir = pmb.transform.position - c2d.transform.position;
            dir = dir.normalized;
            //rbh.PushBones(dir, pushPower);
            
            AudioManager.instance.Play("damage");

            //Vector2 random = new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized;
            
            if (!immune)
            {
                StartCoroutine(Immune());
                Health -= 1;
            }

            PushBones(dir, pushPower);
        }
    }

    public void TriggerStay(Collider2D c2d, PlayerMainBone pmb)
    {
        if(c2d.gameObject.TryGetComponent(out Accelerator acc))
        {
            if (acc.GetAccelerationType() == AcceleratorType.WIND)
            {
                rbh.PushBones(acc.GetDirection(), acc.GetForce());
            }
        }
        else if(c2d.TryGetComponent(out Vaccum v))
        {
            Vector2 distance = c2d.transform.position - pmb.transform.position;
            Vector2 dir2 = distance.normalized;
            
            Vector2 vp;
            
            vp.x = Mathf.Lerp(dir.x, v.GetPower().x, distance.magnitude / v.GetSmooth().x);
            vp.y = Mathf.Lerp(dir.y, v.GetPower().y, distance.magnitude / v.GetSmooth().y);
            
            PushBones(dir2, vp);
        }
    }
    
    public void CollisionEnter(Collider2D c2d)
    {
        if (c2d.gameObject.TryGetComponent(out IDoDamage doDamage))
        {
            Health -= doDamage.getDamage();
            Vector2 dir2 = transform.position - c2d.transform.position;
            dir2 = dir2.normalized;
            rbh.PushBones(dir2, pushPower);
        }
    }

    public void PushBones(Vector2 dir, Vector2 power)
    {
        rbh.PushBones(dir, power);
    }

    public void ToggleGravity(bool value)
    {
        rbh.ToggleGravity(value);
    }

    public void SetConstraintsTrue()
    {
        rbh.ToggleConstraints(true);
    }

    public void SetConstraintsFalse()
    {
        rbh.ToggleConstraints(false);
    }

    public void ShowHud()
    {
        HUD.instance.ShowLoseScreen();
    }

    public void DeletePlayer()
    {
        Destroy(gameObject);
    }

    public void ShowWinScreen()
    {
        HUD.instance.ShowWinScreen();
    }

    private IEnumerator Immune()
    {
        immune = true;

        yield return new WaitForSeconds(immunityTime);

        immune = false;
    }

    public bool Immune1
    {
        get => immune;
        set => immune = value;
    }

    public int Health
    {
        get => health;
        set
        {
            health = value;

            if (health <= 0)
            {
                Debug.Log("Player is dead");
                animator.SetTrigger("die");
            }

            if (health > maxHealth)
            {
                health = maxHealth;
            }
            
            HoleManager.OnHealthChange.Invoke(health);
            
            //transform.localScale = Vector3.one + Vector3.one * health/10f * sizeMultiplier;  
        }
    }
}