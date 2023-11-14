using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    Inventory inventory;
    MoveCamera moveCamera;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        moveCamera = GetComponentInChildren<MoveCamera>();
    }

    public void Save()
    {
        DataManager.instance.nowPlayer.savetime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
        DataManager.instance.nowPlayer.sceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < DataManager.instance.nowPlayer.eventFlags.Length; i++)
        {
            DataManager.instance.nowPlayer.eventFlags[i] = DatabaseManager.instance.eventFlags[i];
        }

        DataManager.instance.nowPlayer.camPos = moveCamera.myCam.transform.position;
        DataManager.instance.SaveData();
    }
}
