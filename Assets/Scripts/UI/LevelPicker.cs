using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPicker : MonoBehaviour
{
    public static string CurrentLevel;
    public TextMeshProUGUI text;
    [SerializeField] private Image lockImage;
    private bool locked;
    
    // private void OnEnable()
    // {
    //     text = GetComponentInChildren<TextMeshProUGUI>();
    // }

    private void OnMouseDown()
    {
        if (!locked)
        {
            Core.LoadScene(WorldChoose.chosenWorldName + "_" + text.text);
            CurrentLevel = text.text;
        }
            
    }

    public bool Locked
    {
        get => locked;
        set
        {
            locked = value;
            
            lockImage.gameObject.SetActive(locked);
            text.gameObject.SetActive(!locked);
        }
    }
}