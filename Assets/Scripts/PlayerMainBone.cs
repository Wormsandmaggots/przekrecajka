using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMainBone : MonoBehaviour
{
    private UnityEvent<Collider2D, PlayerMainBone> onTriggerEnter = new UnityEvent<Collider2D, PlayerMainBone>();

    private void OnEnable()
    {
        onTriggerEnter.AddListener(GetComponentInParent<Player>().TriggerEnter);
    }

    private void OnDisable()
    {
        onTriggerEnter.RemoveAllListeners();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter.Invoke(other, this);
    }
}
