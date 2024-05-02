using System;
using UnityEngine;
using UnityEngine.Events;

public class BoneFunctionality : MonoBehaviour
{
    private UnityEvent<BoneFunctionality, Collider2D> onCollisionEnter = new UnityEvent<BoneFunctionality, Collider2D>();
    private UnityEvent<BoneFunctionality, Collider2D> onTriggerEnter = new UnityEvent<BoneFunctionality, Collider2D>();

    private void OnEnable()
    {
        Player p = GetComponentInParent<Player>();
        // onCollisionEnter.AddListener(p.CollisionEnter);
        // onTriggerEnter.AddListener(p.TriggerEnter);
    }

    private void OnDisable()
    {
        onCollisionEnter.RemoveAllListeners();
        onTriggerEnter.RemoveAllListeners();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter.Invoke(this, other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        onCollisionEnter.Invoke(this, other.collider);
    }
}
