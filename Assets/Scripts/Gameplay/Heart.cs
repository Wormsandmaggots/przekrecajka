using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

public class Heart : MonoBehaviour, ICollectable
{
    [SerializeField] private int healValue = 1;
    private Transform particlesTransform;
    private VisualEffect particles;
    private Collider2D collider;
    private float collectDelay = 2f;
    private float disappearTime = 5f;
    private bool canCollect = false;
    private float dissapear = 0;
    private Animator animator;
    
    private void Start()
    {
        canCollect = false;
        animator = GetComponentInParent<Animator>();
        LayerMask l = LayerMask.NameToLayer("heartStart");
        gameObject.layer = l;
        transform.GetChild(0).gameObject.layer = l;
        particlesTransform = transform.parent.GetChild(1);
        particles = particlesTransform.GetComponent<VisualEffect>();
        StartCoroutine(Delay());

        dissapear = disappearTime;

        particles.enabled = false;

        //transform.DOScale(transform.localScale / 2, disappearTime).onComplete = () => StartCoroutine(DestroyObject());
    }

    private void Update()
    {
        particlesTransform.position = transform.position;

        disappearTime -= Time.deltaTime;

        if (dissapear - disappearTime > 1 && disappearTime > 0)
        {
            dissapear = disappearTime;

            transform.DOShakeScale(0.3f).onComplete = () => transform.DOScale(transform.localScale / 1.7f, 0.5f);
            //animator.SetTrigger("fade");

            particles.enabled = true;
            particles.Play();
        }

        if (disappearTime < 1 && disappearTime > -1)
        {
            StartCoroutine(DestroyObject());

            disappearTime = -5;
        }
    }

    public void Collect(Player player)
    {
        if (!canCollect) return;
        
        player.Health += healValue;
        Destroy(gameObject);
        StartCoroutine(DestroyObject());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(collectDelay);

        canCollect = true;
        LayerMask l  = LayerMask.NameToLayer("Default");
        
        gameObject.layer = l;
        transform.GetChild(0).gameObject.layer = l;
    }

    private IEnumerator DestroyObject()
    {
        transform.DOShakeScale(0.3f).onComplete = () => transform.DOScale(Vector3.zero, 0.5f);
        animator.SetTrigger("fade");
        
        particles.Play();
        
        yield return new WaitForSeconds(0.1f);

        while (particles.aliveParticleCount > 0)
        {
            yield return null;
        }
        
        Destroy(transform.parent.gameObject);
    }   
}
