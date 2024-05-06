using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccum : MonoBehaviour
{
    [SerializeField] private Vector2 vaccumPower = new Vector2(1, 1);
    [SerializeField] private Vector2 smooth = new Vector2(1, 1);

    public Vector2 GetPower()
    {
        return vaccumPower;
    }

    public Vector2 GetSmooth()
    {
        return smooth;
    }
}
