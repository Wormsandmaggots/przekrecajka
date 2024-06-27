using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlackHoleVisual : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private void Update()
        {
            transform.eulerAngles += Vector3.forward * Time.deltaTime * speed;
        }
    }
}