using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndInteractionEvent : MonoBehaviour
{
    public enum InteractionEvent
    {
        HideImage,
        ShowImahe,
        ChangeScene,
        EndingCredit,
        SelectEvent
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] string sceneName;
    [SerializeField] string endingName;
    [SerializeField]
    GameObject selecEvent;

    InteractionController theIC;
    DialogueManager dialogueManager;

    public InteractionEvent interactionEvent;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        EndEvent();
    }

    public void EndEvent()
    {
        switch(interactionEvent)
        {
            case InteractionEvent.HideImage: spriteRenderer.enabled = false; break;
            case InteractionEvent.ShowImahe: spriteRenderer.enabled = true; break;
            case InteractionEvent.ChangeScene: theIC.SettingUI(false); MySceneManager.Instance.ChangeScene(sceneName); break;
            case InteractionEvent.EndingCredit: theIC.SettingUI(false); MySceneManager.Instance.EndingCredit(endingName); break;
            case InteractionEvent.SelectEvent: selecEvent.SetActive(true); theIC.SettingUI(false); dialogueManager.SettingUI(false); break;
        }

    }
}
