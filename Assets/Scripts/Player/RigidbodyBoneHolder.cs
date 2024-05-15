using System;
using UnityEngine;

public class RigidbodyBoneHolder : MonoBehaviour
{
    private Rigidbody2D[] bonerb;

    private void Awake()
    {
        bonerb = GetComponentsInChildren<Rigidbody2D>();

        // foreach (var bone in bonerb)
        // {
        //     bone.gameObject.AddComponent<BoneFunctionality>();
        // }
    }

    public void PushBones(Vector2 dir, Vector2 force)
    {
        foreach (var rb in bonerb)
        {
            rb.AddForce(dir * force);
        }
    }

    public void SetGravity(float gravityScale)
    {
        foreach (Rigidbody2D rb in bonerb)
        {
            rb.gravityScale = gravityScale;
        }
    }
}
