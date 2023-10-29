using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;
    [SerializeField] GameObject go_DialogueImage;

    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;

    Dialogue[] dialogues;

    public bool isDialogue = false;
    bool isNext = false;

    [Header ("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0;
    int contextCount = 0;

    InteractionController theIC;
    SplashManager splashManager;
    SpriteManager spriteManager;
    DestroyObject destroyObject;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        splashManager = FindObjectOfType<SplashManager>();
        spriteManager = FindObjectOfType<SpriteManager>();
    }

    private void Update()
    {
        if(isDialogue)
        {
            if(isNext)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount < dialogues.Length)
                        {
                            StartCoroutine(CameraTargettingType());
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        theIC.SettingUI(false);
        dialogues = p_dialogues;

        StartCoroutine(CameraTargettingType());
    }

    IEnumerator CameraTargettingType()
    {
        switch(dialogues[lineCount].cameraType)
        {
            case CameraType.FadeIn: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.FadeIn(false, true));yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FadeOut: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.FadeOut(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashIn: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.Splash()); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashOut: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.Splash()); yield return new WaitUntil(() => SplashManager.isfinished); break;
        }

        StartCoroutine(TypeWriter());
    }

    void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theIC.SettingUI(true);
        SettingUI(false);
        go_DialogueNameBar.SetActive(false);
    }

    void ChangeSprite()
    {
        if (dialogues[lineCount].spriteName[contextCount] != "")
        {
            StartCoroutine(spriteManager.SpriteChangeCoroutine(dialogues[lineCount].targetImage, dialogues[lineCount].spriteName[contextCount]));
        }
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);
        ChangeSprite();

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        //txt_Dialogue.text = dialogues[lineCount].name;
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        go_DialogueImage.SetActive(p_flag);

        //go_DialogueNameBar.SetActive(p_flag);

        if(p_flag)
        {
            if (dialogues[lineCount].name == "")
            {
                go_DialogueNameBar.SetActive(false);
            }
            else
            {
                go_DialogueNameBar.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
    }
}
