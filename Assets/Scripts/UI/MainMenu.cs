using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string levelSceneName = "WorldPicker";
        private static bool first = true;
        private float timer = 1.5f;

        private void Start()
        {
            if (!first) timer = 0;
        }

        private void Update()
        {
            if(first)
                timer -= Time.deltaTime;
            
            if (Input.touchCount > 0 && timer <= 0)
            {
                Core.LoadScene(levelSceneName);
            }
        }
    }
}