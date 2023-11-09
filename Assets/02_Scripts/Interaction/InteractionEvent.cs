using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;

    [SerializeField] DialogueEvent dialogueEvent;

    PolygonCollider2D polygonCollider2D;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    bool CheckEvent()
    {
        bool t_flag = true;

        for (int i = 0; i < dialogueEvent.eventTiming.eventConditions.Length; i++)
        {
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventConditions[i]] != dialogueEvent.eventTiming.conditionFlag)
            {
                t_flag = false;
                break;
            }
        }

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
        if (polygonCollider2D != null && spriteRenderer != null)
        {
            bool t_flag = CheckEvent();

            polygonCollider2D.enabled = t_flag;
            spriteRenderer.enabled = t_flag;
        }
        else
        {
            return;
        }

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
