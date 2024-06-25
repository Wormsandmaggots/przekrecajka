using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMainBone : MonoBehaviour
{
    public static bool AllowCollision = true;
    private UnityEvent<Collider2D, PlayerMainBone> onTriggerEnter = new UnityEvent<Collider2D, PlayerMainBone>();
    private UnityEvent<Collider2D, PlayerMainBone> onTriggerStay = new UnityEvent<Collider2D, PlayerMainBone>();
    private UnityEvent<Vector2, Vector2> pushBones = new UnityEvent<Vector2, Vector2>();

    private void OnEnable()
    {
        Player p = GetComponentInParent<Player>();
        onTriggerEnter.AddListener(p.TriggerEnter);
        onTriggerStay.AddListener(p.TriggerStay);
        pushBones.AddListener(p.PushBones);
    }

    private void OnDisable()
    {
        onTriggerEnter.RemoveAllListeners();
        onTriggerStay.RemoveAllListeners();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!AllowCollision) return;
        
        onTriggerEnter.Invoke(other, this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!AllowCollision) return;
        
        onTriggerStay.Invoke(other, this);
    }
}
