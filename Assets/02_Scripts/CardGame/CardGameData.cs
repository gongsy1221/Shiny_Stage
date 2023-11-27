using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveScore
{
    public int cmwSetScore = 0;
    public int kySetScore = 0;
}

public class CardGameData:MonoBehaviour
{
    public static CardGameData instance;

    public SaveScore nowPlayer = new SaveScore();

    public string path;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + "ScoreData", data);
    }

    public void LoadData()
    {
        string loaddata = File.ReadAllText(path + "ScoreData");
        nowPlayer = JsonUtility.FromJson<SaveScore>(loaddata);
    }
}
