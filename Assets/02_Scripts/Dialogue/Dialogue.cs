using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CameraType
{
    None,
    FadeIn,
    FadeOut,
    FlashIn,
    FlashOut,
    ShowCutScene,
    HideCutScene
}

public enum AppearType
{
    None,
    Appear,
    Disappear
}

[System.Serializable]
public class Dialogue
{
    [Tooltip("��ȭ�ϰ� �ִ� ĳ���� �̹���")]
    public CameraType cameraType;
    public Transform targetImage;

    [HideInInspector]
    public string name;

    [HideInInspector]
    public string[] contexts;

    [Tooltip("�̺�Ʈ ��ȣ")]
    public string[] number;

    [Tooltip("��ŵ����")]
    public string[] skipnum;

    [HideInInspector]
    public string[] spriteName;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 line;
    public Dialogue[] dialogues;

    [Space]
    public AppearType appearType;
    public GameObject[] go_Targets;
}
