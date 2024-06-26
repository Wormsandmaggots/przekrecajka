using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerMainBone>(out PlayerMainBone a ))
            {
                a.GetComponentInParent<Animator>().SetTrigger("suck");
                HUD.instance.ShowWinScreen();
            }
        }
    }
}
