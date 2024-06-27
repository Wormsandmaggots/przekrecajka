using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShower : MonoBehaviour
{
    private bool used = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMainBone pmb))
        {
            if (used) return;
        
            HUD.instance.ShowTutorial();

            used = true; 
        }
    }
}
