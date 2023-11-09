using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;

    [SerializeField] DialogueEvent dialogueEvent;

    private void Start()
    {
        bool t_flag = CheckEvent();

        gameObject.SetActive(t_flag);
    }

    bool CheckEvent()
    {
        bool t_flag = true;

        // ���� ���ǰ� ��ġ���� ���� ���, �����Ű�� ����
        for (int i = 0; i < dialogueEvent.eventTiming.eventConditions.Length; i ++)
        {
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventConditions[i]] != dialogueEvent.eventTiming.conditionFlag)
            {
                t_flag = false;
                break;
            }
        }

        // ���� ���ǰ� �������, ���� ���ǰ� ��ġ�� ���, ������ �����Ű�� ����
        if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventEndNum])
        {
            t_flag = false;
        }

        return t_flag;
    }

    public Dialogue[] GetDialogues()
    {
        DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum] = true;
        DialogueEvent t_DialogueEvent = new DialogueEvent();
        t_DialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            t_DialogueEvent.dialogues[i].targetImage = dialogueEvent.dialogues[i].targetImage;
            t_DialogueEvent.dialogues[i].cameraType = dialogueEvent.dialogues[i].cameraType;
        }

        dialogueEvent.dialogues = t_DialogueEvent.dialogues;

        return dialogueEvent.dialogues;
    }

    public AppearType GetAppearType()
    {
        return dialogueEvent.appearType;
    }

    public GameObject[] GetTargets()
    {
        return dialogueEvent.go_Targets;
    }

    public GameObject GetNextEvent()
    {
        return dialogueEvent.go_NextEvent;
    }

    private void Update()
    {
        if (isAutoEvent && DatabaseManager.isFinish)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            DialogueManager.isWaiting = true;

            if (GetAppearType() == AppearType.Appear)
            {
                dialogueManager.SetAppearObjects(GetTargets());
            }
            else if (GetAppearType() == AppearType.Disappear)
            {
                dialogueManager.SetDisappearObjects(GetTargets());
            }
            dialogueManager.SetNextEvent(GetNextEvent());
            dialogueManager.ShowDialogue(GetDialogues());

            gameObject.SetActive(false);
        }
    }
}
