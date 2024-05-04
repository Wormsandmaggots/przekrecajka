using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccum : MonoBehaviour
{
    [SerializeField] private Vector2 vaccumPower = new Vector2(1, 1);
    [SerializeField] private Vector2 smooth = new Vector2(1, 1);

    private void OnTriggerStay2D(Collider2D other)
    {
        // if (other.TryGetComponent(out PlayerMainBone p))
        // {
        //     Vector2 distance = other.transform.position - transform.position;
        //     Vector2 dir = -distance.normalized;
        //     
        //     Vector2 vp;
        //     
        //     vp.x = Mathf.Lerp(dir.x, vaccumPower.x, distance.magnitude / smooth.x);
        //     vp.y = Mathf.Lerp(dir.y, vaccumPower.y, distance.magnitude / smooth.y);
        //     
        //     p.PushBones(dir, vp);
        // }
    }

    public Vector2 GetPower()
    {
        return vaccumPower;
    }

    public Vector2 GetSmooth()
    {
        return smooth;
    }
}
