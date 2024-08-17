using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private string levelSceneName = "WorldPicker";
        private static bool first = true;
        private float timer = 1.5f;
        [SerializeField] private Image transition;
        private bool isTransitionWorking = false;

        private void Start()
        {
            if (!first) timer = 0;
        }

        private void Update()
        {
            if(first)
                timer -= Time.deltaTime;
            
            if (Input.touchCount > 0 && timer <= 0 && !isTransitionWorking)
            {
                isTransitionWorking = true;
                transition.DOFade(1, 2).onComplete = () =>
                {
                    Core.LoadScene(levelSceneName);
                };
            }
        }
    }
}