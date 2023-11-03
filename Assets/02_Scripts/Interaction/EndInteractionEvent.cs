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
        ChangeScene
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] string sceneName;

    public InteractionEvent interactionEvent;

    private void Start()
    {
        EndEvent();
    }

    public void EndEvent()
    {
        switch(interactionEvent)
        {
            case InteractionEvent.HideImage: spriteRenderer.enabled = false; break;
            case InteractionEvent.ShowImahe: spriteRenderer.enabled = true; break;
            case InteractionEvent.ChangeScene: SceneManager.LoadScene(sceneName);break;
        }

    }
}
