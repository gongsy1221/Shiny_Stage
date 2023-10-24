using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] GameObject endPos;
    [SerializeField] GameObject openButton;
    [SerializeField] GameObject closeButton;
    [SerializeField] GameObject mapUI;

    InteractionController theIC;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();

        startPos = transform.position;
        mapUI.SetActive(false);
    }

    public void ClickMoveButton()
    {
        mapUI.SetActive(true);
        theIC.SettingUI(false);
    }

    public void ExitMove()
    {
        mapUI.SetActive(false);
        theIC.SettingUI(true);
    }

    public void ClickCloseButton()
    {
        MoveDown();
    }

    public void ClickOpenButton()
    {
        MoveUp();
    }

    void MoveDown()
    {
        transform.DOMove(endPos.transform.position, 0.5f);
        closeButton.SetActive(false);
        openButton.SetActive(true);
    }

    void MoveUp()
    {
        openButton.SetActive(false);
        closeButton.SetActive(true);
        transform.DOMove(startPos, 0.5f);
    }
}
