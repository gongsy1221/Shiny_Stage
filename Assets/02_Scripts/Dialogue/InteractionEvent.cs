using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public int lineY;
    public int s_lineY;
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogues()
    {
        return dialogue.dialogues;
    }
}
