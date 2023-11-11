using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] GameObject explainBoard;

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
}
