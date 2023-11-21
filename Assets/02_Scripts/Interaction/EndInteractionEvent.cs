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
        EndingCredit
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] string sceneName;
    [SerializeField] string endingName;

    InteractionController theIC;

    public InteractionEvent interactionEvent;

    private void Start()
    {
        theIC = FindObjectOfType<InteractionController>();

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
        }

    }
}
