using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldChoose : MonoBehaviour
{
    private static WorldPicker[] worlds;
    
    public static string chosenWorldName;
    
    private void Start()
    {
        worlds = FindObjectsByType<WorldPicker>(FindObjectsSortMode.None);
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
}
