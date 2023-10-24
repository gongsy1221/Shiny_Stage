using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Camera myCam;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject mapUI;

    float fadeTime = 1f;

    public void MyRoom()
    {
        CameraMove(new Vector3(20, 0, -10));
    }

    void CameraMove(Vector3 camPos)
    {
        mapUI.SetActive(false);
        fadeImg.enabled = true;
        fadeImg.DOFade(1f, fadeTime);
        myCam.transform.position = camPos;
        fadeImg.DOFade(0f, fadeTime);
        fadeImg.enabled=false;
    }
}
