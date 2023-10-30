using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject go_DialogueNameBar;
    [SerializeField] GameObject[] go_DialogueImage;

    [SerializeField] TextMeshProUGUI txt_Dialogue;
    [SerializeField] TextMeshProUGUI txt_Name;

    Dialogue[] dialogues;

    public bool isDialogue = false;
    bool isNext = false;

    [Header ("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;

    int lineCount = 0; // ��ȭ ī��Ʈ
    int contextCount = 0; // ��� ī��Ʈ

    // �̺�Ʈ ������ ����, ���� ������Ʈ��
    GameObject[] go_Objects;
    byte appearTypeNumber;
    const byte none = 0, appear = 1, disappear = 2;

    public void SetAppearObjects(GameObject[] p_targets)
    {
        go_Objects = p_targets;
        appearTypeNumber = appear;
    }

    public void SetDisappearObjects(GameObject[] p_targets)
    {
        go_Objects = p_targets;
        appearTypeNumber = disappear;
    }

    InteractionController theIC;
    SplashManager splashManager;
    SpriteManager spriteManager;
    CutSceneManager cutSceneManager;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        splashManager = FindObjectOfType<SplashManager>();
        spriteManager = FindObjectOfType<SpriteManager>();
        cutSceneManager = FindObjectOfType<CutSceneManager>();
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
                            StartCoroutine(EndDialogue());
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
            case CameraType.FlashIn: go_DialogueNameBar.SetActive(false); SettingUI(false); SplashManager.isfinished = false; StartCoroutine(splashManager.FadeOut(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashOut: go_DialogueNameBar.SetActive(false); SettingUI(false);SplashManager.isfinished = false; StartCoroutine(splashManager.FadeOut(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.ShowCutScene: SettingUI(false); CutSceneManager.isFinished = false;StartCoroutine(cutSceneManager.CutSceneRoutine(dialogues[lineCount].spriteName[contextCount], true));yield return new WaitUntil(() => CutSceneManager.isFinished);break;
            case CameraType.HideCutScene: SettingUI(false); CutSceneManager.isFinished = false;StartCoroutine(cutSceneManager.CutSceneRoutine(null, false));yield return new WaitUntil(() => CutSceneManager.isFinished);break;
        }

        StartCoroutine(TypeWriter());
    }

    IEnumerator EndDialogue()
    {
        if(cutSceneManager.CheckCutScene())
        {
            CutSceneManager.isFinished = false;
            StartCoroutine(cutSceneManager.CutSceneRoutine(null, false));
            yield return new WaitUntil(() => CutSceneManager.isFinished);
        }

        AppearOrDisappearObjects();

        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        theIC.SettingUI(true);
        SettingUI(false);
        go_DialogueNameBar.SetActive(false);
    }

    void AppearOrDisappearObjects()
    {
        if(go_Objects!=null)
        {
            for (int i = 0; i < go_Objects.Length; i++)
            {
                if(appearTypeNumber == appear)
                {
                    go_Objects[i].SetActive(true);
                }
                else if(appearTypeNumber == disappear)
                {
                    go_Objects[i].SetActive(false);
                }
            }
        }
        go_Objects = null;
        appearTypeNumber = none;
    }

    void ChangeSprite()
    {
        if (dialogues[lineCount].targetImage != null)
        {
            if (dialogues[lineCount].spriteName[contextCount] != "")
            {
                StartCoroutine(spriteManager.SpriteChangeCoroutine(dialogues[lineCount].targetImage, dialogues[lineCount].spriteName[contextCount]));
            }
        }
    }

    IEnumerator TypeWriter()
    {
        SettingUI(true);
        ChangeSprite();

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");
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

        for (int i = 0; i < go_DialogueImage.Length; i++)
        {
            go_DialogueImage[i].SetActive(p_flag);
        }
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
