using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool works = true;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private List<ParticleSystem> particles;
    [SerializeField] private Transform end;
    [SerializeField] private float workingTime = 1.0f;
    [SerializeField] private float disabledTime = 1.0f;
    private BoxCollider2D collider;
    private float workingTimer;
    private float disabledTimer;
        
    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        workingTimer = workingTime;
        disabledTimer = disabledTime;
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPoint.position);
        ToggleLaser(false);
    }

    private void Update()
    {
        if (!works)
        {
            disabledTimer -= Time.deltaTime;

            if (disabledTimer <= 0)
            {
                works = true;
                disabledTimer = disabledTime;
            }
            
            return;
        }

        Vector3 startPos = startPoint.position;
        Vector3 endPos = endPoint.position;

        Vector2 direction = (endPos - startPos).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 100, Settings.platforms);
        
        lineRenderer.SetPosition(0, startPoint.position);
        
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }

        Vector2 offset = (lineRenderer.GetPosition(1) - startPos) / 2;

        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            offset.y = MathF.Abs(offset.x);
        }

        if (offset.y < 0)
        {
            offset.y = MathF.Abs(offset.y);
        }
        
        collider.offset = new Vector2(0, offset.y);
        collider.size = new Vector2(collider.size.x, offset.y * 2);

        end.transform.position = lineRenderer.GetPosition(1);

        workingTimer -= Time.deltaTime;
        
        if(!lineRenderer.enabled)
            ToggleLaser(true);

        if (workingTimer <= 0)
        {
            workingTimer = workingTime;
            
            ToggleLaser(false);
        }
    }

    void ToggleLaser(bool value)
    {
        works = value;
        lineRenderer.enabled = value;
        collider.enabled = value;

        foreach (var ps in particles)
        {
            if(value)
                ps.Play();
            else
            {
                ps.Stop();
            }
        }
    }
}
