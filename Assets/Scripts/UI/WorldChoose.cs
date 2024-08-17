using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WorldChoose : MonoBehaviour
{
    private static WorldPicker[] worlds;
    
    public static string chosenWorldName;

    public static Action<LevelPicker> OnLevelChoose;
    public static bool isTransitionWorking = true;
    [SerializeField] private Image transition;

    private void OnEnable()
    {
        OnLevelChoose += ChooseLevel;
    }

    private void OnDisable()
    {
        OnLevelChoose -= ChooseLevel;
    }

    private void Start()
    {
        worlds = FindObjectsByType<WorldPicker>(FindObjectsSortMode.None);

        transition.DOFade(0, 2).onComplete = () =>
        {
            isTransitionWorking = false;
        };
    }

    public static void AnimWorlds(WorldPicker invoker)
    {
        chosenWorldName = invoker.worldName;
        int mul = 5;
        foreach (var world in worlds)
        {
            if (world != invoker)
            {
                world.transform.DOMove(world.transform.position +
                                       Vector3.one * mul, 1f);

                mul = -mul;
            }
        }
    }

    private void ChooseLevel(LevelPicker lp)
    {
        if (isTransitionWorking) return;
        
        LevelPicker.CurrentLevel = lp.text.text;
        isTransitionWorking = true;

        transition.DOFade(1, 2).onComplete = () =>
        {
            Core.LoadScene(WorldChoose.chosenWorldName + "_" + lp.text.text);
        };
    }
}
