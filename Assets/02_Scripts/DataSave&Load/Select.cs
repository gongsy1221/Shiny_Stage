using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using Unity.VisualScripting;

public class Select : MonoBehaviour
{
    public TextMeshProUGUI[] slotText;

    bool[] savefile = new bool[5];

    Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        for(int i = 0; i< savefile.Length; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;

                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.savetime;
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
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            GoGame();
        }
    }

    private void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.savetime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            DataManager.instance.SaveData();
            MySceneManager.Instance.ChangeScene("01_First");
        }
        else
        {
            MySceneManager.Instance.ChangeScene(DataManager.instance.nowPlayer.sceneName);

            if(MySceneManager.Instance.changeScene == true)
            {
                LoadData();
            }
        }
    }

    private void LoadData()
    {
        Camera.main.transform.position = DataManager.instance.nowPlayer.camPos;

        for (int i = 0; i < DataManager.instance.nowPlayer.eventFlags.Length; i++)
        {
            DatabaseManager.instance.eventFlags[i] = DataManager.instance.nowPlayer.eventFlags[i];
        }

        for (int i = 0; i < DataManager.instance.nowPlayer.items.Count; i++)
        {
            inventory.AddItem(DataManager.instance.nowPlayer.items[i]);
        }
    }
}