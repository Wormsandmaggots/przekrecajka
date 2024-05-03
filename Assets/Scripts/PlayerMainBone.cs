using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMainBone : MonoBehaviour
{
    private UnityEvent<Collider2D, PlayerMainBone> onTriggerEnter = new UnityEvent<Collider2D, PlayerMainBone>();
    private UnityEvent<Collider2D, PlayerMainBone> onTriggerStay = new UnityEvent<Collider2D, PlayerMainBone>();

    private void OnEnable()
    {
        Player p = GetComponentInParent<Player>();
        onTriggerEnter.AddListener(p.TriggerEnter);
        onTriggerStay.AddListener(p.TriggerStay);
    }

    private void OnDisable()
    {
        onTriggerEnter.RemoveAllListeners();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter.Invoke(other, this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        onTriggerStay.Invoke(other, this);
    }
}
