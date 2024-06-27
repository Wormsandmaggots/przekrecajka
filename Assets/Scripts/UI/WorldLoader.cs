using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WorldLoader : MonoBehaviour
{
    private static string path = @"save.txt";
    //true - unlocked
    //false - locked
    public static Dictionary<string, Dictionary<string, bool>> data = new Dictionary<string, Dictionary<string, bool>>
        { };
    private static string saveString;
    private static WorldPicker[] pickers;
    
    private void Start()
    {
        if (data.Count <= 0)
        {
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                }

                bool first = true;

                pickers = FindObjectsByType<WorldPicker>(FindObjectsSortMode.InstanceID);
                foreach (var world in pickers)
                {
                    data.Add(world.worldName, new Dictionary<string, bool>());

                    saveString += world.worldName + " ";

                    if (first)
                    {
                        saveString += "w1";
                        world.Locked = false;
                    }
                    else
                    {
                        saveString += "w0";
                        world.Locked = true;
                        world.GetComponent<Image>().DOFade(0.2f, 1);
                    }

                    foreach (var level in world.levels)
                    {
                        if (first)
                        {
                            saveString += " " + level.text.text + "1";
                            data[world.worldName].Add(level.text.text, true);
                            level.Locked = false;
                            first = false;
                            continue;
                        }

                        saveString += " " + level.text.text + "0";
                        data[world.worldName].Add(level.text.text, false);
                        level.GetComponent<Image>().DOFade(0.5f, 1);
                    }

                    saveString += "\n";
                }

                File.WriteAllText(path, saveString);

            }
            else
            {

                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        saveString += line + "\n";
                    }
                }

                string[] s1 = saveString.Split("\n");
                string[] s = new string[s1.Length - 1];

                for (int j = 0; j < s.Length; j++)
                {
                    s[j] = s1[j];
                }

                string[][] ss = new string [s.Length][];

                int i = 0;
                foreach (var line in s)
                {
                    ss[i] = s[i].Split(" ");
                    i++;
                }

                foreach (var line in ss)
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (j == 0)
                        {
                            data.Add(line[j], new Dictionary<string, bool>());
                            continue;
                        }

                        data[line[0]].Add(line[j][0].ToString(), line[j][1] == '0');
                    }
                }

                pickers = FindObjectsByType<WorldPicker>(FindObjectsSortMode.InstanceID);

                int k = 0;
                foreach (var picker in pickers)
                {
                    picker.Locked = data[picker.worldName]["w"];
                    
                    if (picker.Locked)
                    {
                        picker.GetComponent<Image>().DOFade(0.2f, 1);
                    }


                    for (int j = 0; j < picker.levels.Length; j++)
                    {
                        picker.levels[j].Locked = data[picker.worldName][picker.levels[j].text.text];
                        
                        if (picker.levels[j].Locked)
                        {
                            picker.levels[j].GetComponent<Image>().DOFade(0.5f, 1);
                        }
                    }
                }
            }
        }
        else
        {
            pickers = FindObjectsByType<WorldPicker>(FindObjectsSortMode.InstanceID);

            int k = 0;
            foreach (var picker in pickers)
            {
                picker.Locked = data[picker.worldName]["w"];

                if (picker.Locked)
                {
                    picker.GetComponent<Image>().DOFade(0.2f, 1);
                }

                for (int j = 0; j < picker.levels.Length; j++)
                {
                    picker.levels[j].Locked = data[picker.worldName][picker.levels[j].text.text];

                    if (picker.levels[j].Locked)
                    {
                        picker.levels[j].GetComponent<Image>().DOFade(0.5f, 1);
                    }
                }
            }
        }
    }

    public static void Save()
    {
        saveString = "";
        foreach (var world in pickers)
        {
            saveString += world.worldName;

            saveString += " w" + (world.Locked == true ? "0" : "1");
                
            foreach (var level in world.levels)
            {
                saveString += " " + level.text.text + (data[world.worldName][level.text.text] == false ? "0" : "1");
            }

            saveString += "\n";
        }
            
        File.WriteAllText(path, saveString);
    }

    public static void UpdateSave(string worldName, string level, bool value)
    {
        data[worldName][level] = value;
    }
}
