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

    public void ToggleConstraints(bool value)
    {
        foreach (var rb in bonerb)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    public void ToggleGravity(bool value)
    {
        SetGravity(value ? 1 : 0);
    }

    public void SetGravity(float gravityScale)
    {
        foreach (Rigidbody2D rb in bonerb)
        {
            rb.gravityScale = gravityScale;
        }
    }
}
