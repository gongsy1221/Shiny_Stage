using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Game : MonoBehaviour
{
    Inventory inventory;
    MoveCamera moveCamera;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        moveCamera = FindObjectOfType<MoveCamera>();
    }

    public void Save()
    {
        DataManager.instance.nowPlayer.savetime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
        DataManager.instance.nowPlayer.sceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < DataManager.instance.nowPlayer.eventFlags.Length; i++)
        {
            DataManager.instance.nowPlayer.eventFlags[i] = DatabaseManager.instance.eventFlags[i];
        }

        DataManager.instance.nowPlayer.items = inventory.items;

        DataManager.instance.nowPlayer.camPos = moveCamera.myCam.transform.position;
        DataManager.instance.SaveData();
    }
}
