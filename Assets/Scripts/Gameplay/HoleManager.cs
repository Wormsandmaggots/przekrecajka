using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoleManager : MonoBehaviour
{
    public static UnityEvent<int> OnHealthChange = new UnityEvent<int>();
    private List<Hole> holes;

    private void OnEnable()
    {
        OnHealthChange.AddListener(HealthChange);
    }

    private void OnDisable()
    {
        OnHealthChange.RemoveAllListeners();
    }

    void Awake()
    {
        holes = new List<Hole>(FindObjectsByType<Hole>(FindObjectsInactive.Include, FindObjectsSortMode.None));
    }

    private void HealthChange(int currentHealth)
    {
        foreach (Hole hole in holes)
        {
            hole.SetColliderEnable(currentHealth > hole.HealthThreshold);
        }
    }
}
