using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] GameObject explainBoard;
    [SerializeField] GameObject saveBoard;

    public void StartButton()
    {
        SceneManager.LoadScene("Prologue");
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
}
