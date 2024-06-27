using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WorldPicker : MonoBehaviour
{
    [SerializeField] public string worldName;
    [HideInInspector] public LevelPicker[] levels = new LevelPicker[]{};
    private Vector2 startPos;
    private Rigidbody2D rb;
    private float power = 5;
    private float activateThreshold = 0.6f;
    private float returnDelay = 2f;
    private float returnSpeed = 2f;
    private float returnTimer;
    private Collider2D collider;
    private Camera cam;
    private Animator animator;
    private bool locked;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        startPos = transform.position;
        Input.compensateSensors = true;
        rb = GetComponent<Rigidbody2D>();
        returnTimer = returnDelay;
        Input.gyro.enabled = true;
        collider = GetComponent<Collider2D>();

        levels = GetComponentsInChildren<LevelPicker>();

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].text.text = (i + 1).ToString();
        }
    }

    private void Update()
    {
        // Vector2 v = Input.gyro.gravity;
        //
        // //v.y += 0.98f;
        //
        // if (v.magnitude > activateThreshold)
        // {
        //     rb.AddForce(Input.acceleration.normalized * power);
        //     returnTimer = returnDelay;
        // }
        // else
        // {
        //     returnTimer -= Time.deltaTime;
        //
        //     if (returnTimer < 0)
        //     {
        //         if (Vector2.Distance(transform.position, startPos) > 0.01f)
        //         {
        //             Vector2 dst = Vector2.Lerp(transform.position, startPos, Time.deltaTime * returnSpeed);
        //             transform.position = new Vector3(dst.x, dst.y, 0);
        //         }
        //             
        //     }
        // }
    }

    private void OnMouseDown()
    {
        if (locked) return;
        
        cam.transform.DOMove(new Vector3(transform.position.x, transform.position.y, cam.transform.position.z), 2f);
        cam.DOOrthoSize(4f, 2f);
        animator.SetTrigger("Click");
        WorldChoose.AnimWorlds(this);
    }

    public void PlayCrack()
    {
        AudioManager.instance.Play("crack");
    }

    public void StopCrack()
    {
        AudioManager.instance.Stop("crack");
    }

    public void PlayExplosion()
    {
        AudioManager.instance.Play("explosion");
    }

    public bool Locked
    {
        get => locked;
        set
        {
            locked = value;
            
            
        }
    }
}
