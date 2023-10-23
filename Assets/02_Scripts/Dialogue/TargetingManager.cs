using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class TargetingManager : MonoBehaviour
{
    Dialogue dialogue;

    float noColor = 0.5f;
    float yesColor = 1;

    public void NoTalkColor()
    {
        if(dialogue.image != null)
        {
            //dialogue.image.color = new Color(noColor, noColor, noColor, 1);
        }

    }

    public void TalkColor()
    {
        if (dialogue.image != null)
        {
            //dialogue.image.color = new Color(yesColor, yesColor, yesColor, 1);
        }
    }
}
