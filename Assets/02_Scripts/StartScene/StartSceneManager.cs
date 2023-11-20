using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] GameObject explainBoard;
    [SerializeField] GameObject saveBoard;
    [SerializeField] GameObject settingBoard;

    public void StartButton()
    {
        SceneManager.LoadScene("02_Prologue");
    }

    public void ExplainButton()
    {
        explainBoard.SetActive(true);
    }

    public void XExpainButton()
    {
        explainBoard.SetActive(false);
    }
    
    public void SaveButton()
    {
        saveBoard.SetActive(true);
    }

    public void XSaveButton()
    {
        saveBoard.SetActive(false);
    }

    public void SettingButton()
    {
        settingBoard.SetActive(true);
    }

    public void XSettingButton()
    {
        settingBoard.SetActive(false);
    }
}
