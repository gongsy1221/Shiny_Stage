using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class Select : MonoBehaviour
{
    public TextMeshProUGUI[] slotText;
    string[] saveTime = new string[5];

    bool[] savefile = new bool[5];

    private void Start()
    {
        for(int i = 0; i< savefile.Length; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;

                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = saveTime[i];
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }

        DataManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            saveTime[number] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            saveTime[number] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
            GoGame();
        }
    }

    private void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.SaveData();
        }
        SceneManager.LoadScene("Prologue");
    }
}
