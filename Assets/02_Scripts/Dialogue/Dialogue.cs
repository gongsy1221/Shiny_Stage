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
    [Tooltip("대화하고 있는 캐릭터 이미지")]
    public CameraType cameraType;
    public Transform targetImage;

    [HideInInspector]
    public string name;

    [HideInInspector]
    public string[] contexts;

    [Tooltip("이벤트 번호")]
    public string[] number;

    [Tooltip("스킵라인")]
    public string[] skipnum;

    [HideInInspector]
    public string[] spriteName;
}

[System.Serializable]
public class EventTiming
{
    public int eventNum;
    public int[] eventConditions;
    public bool conditionFlag;
    public int eventEndNum;
}

[System.Serializable]
public class DialogueEvent
{
    public string name;
    public EventTiming eventTiming;

    public Vector2 line;
    public Dialogue[] dialogues;

    [Space]
    public AppearType appearType;
    public GameObject[] go_Targets;

    [Space]
    public GameObject go_NextEvent;
}
