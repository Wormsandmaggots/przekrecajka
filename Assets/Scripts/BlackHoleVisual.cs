using System;
using UnityEngine;

namespace DefaultNamespace
{
    [ExecuteAlways]
    public class BlackHoleVisual : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        private void Update()
        {
            transform.eulerAngles += Vector3.forward * Time.deltaTime * speed;
        }
    }
}