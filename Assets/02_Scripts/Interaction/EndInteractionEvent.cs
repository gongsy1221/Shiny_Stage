using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndInteractionEvent : MonoBehaviour
{
    public enum InteractionEvent
    {
        HideImage,
        ShowImahe,
        ChangeScene,
        EndingCredit,
        SelectEvent,
        ChangeItem
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] string sceneName;
    [SerializeField] string stageName;
    [SerializeField] string endingName;
    [SerializeField]
    GameObject selecEvent;
    [SerializeField] Item item;

    InteractionController theIC;
    DialogueManager dialogueManager;
    Inventory inventory;

    public InteractionEvent interactionEvent;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        inventory = FindObjectOfType<Inventory>();

        EndEvent();
    }

    public void EndEvent()
    {
        switch(interactionEvent)
        {
            case InteractionEvent.HideImage: spriteRenderer.enabled = false; break;
            case InteractionEvent.ShowImahe: spriteRenderer.enabled = true; break;
            case InteractionEvent.ChangeItem: inventory.items.Remove(inventory.items[0]); inventory.items.Add(item); break;
            case InteractionEvent.ChangeScene: theIC.SettingUI(false);
                MySceneManager.Instance.ChangeScene(sceneName, stageName);
                break;
            case InteractionEvent.EndingCredit: theIC.SettingUI(false);
                MySceneManager.Instance.EndingCredit(endingName);
                break;
            case InteractionEvent.SelectEvent: selecEvent.SetActive(true);
                theIC.SettingUI(false);
                dialogueManager.SettingUI(false);
                break;
        }

    }
}
