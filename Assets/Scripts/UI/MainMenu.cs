using System;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string levelSceneName = "LevelChoose";
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Core.LoadScene(levelSceneName);
            }
        }
    }
}