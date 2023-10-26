using System;
using System.Collections;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;

    RaycastHit2D hitInfo;

    [SerializeField] GameObject go_InteractiveCrosshair;
    [SerializeField] GameObject go_NomalCrosshair;
    [SerializeField] GameObject go_Event;
    [SerializeField] GameObject go_Cusror;

    bool isContact = false;
    public static bool isInteract = false;

    [SerializeField] ParticleSystem ps_QuesttionEffect;

    DialogueManager theDM;

    public void SettingUI(bool p_flag)
    {
        go_Event.SetActive(p_flag);
        go_Cusror.SetActive(p_flag);

        isInteract = !p_flag;
    }    

    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();

        go_InteractiveCrosshair.SetActive(false);
    }

    private void Update()
    {
        if (!isInteract)
        {
            CheckObject();
            ClickLeftButton();
        }
    }

    void CheckObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hitInfo = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitInfo.collider != null)
        {
            Contact();
        }
        else
        {
            NoContact();
        }
    }
    void Contact()
    {
        if(hitInfo.transform.CompareTag("Interaction"))
        {
            if (!isContact)
            {
                isContact = true;
                go_InteractiveCrosshair.SetActive(true);
                go_NomalCrosshair.SetActive(false);
            }
        }
        else
        {
            NoContact();
        }
    }

    void NoContact()
    {
        if (isContact)
        {
            isContact = false;
            go_InteractiveCrosshair.SetActive(false);
            go_NomalCrosshair.SetActive(true);
        }
    }

    private void ClickLeftButton()
    {
        if (!isInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isContact)
                {
                    Interact();
                }
            }
        }
    }

    private void Interact()
    {
        isInteract = true;

        ps_QuesttionEffect.gameObject.SetActive(true);
        ps_QuesttionEffect.transform.position = hitInfo.transform.position;
        Vector3 t_targetPos = hitInfo.transform.position;
        ps_QuesttionEffect.GetComponent<QuestionEffect>().SetTarget(t_targetPos);

        StartCoroutine(WaitCollision());
    }

    IEnumerator WaitCollision()
    {
        yield return new WaitUntil(()=>QuestionEffect.isCollide);
        QuestionEffect.isCollide = false;

        theDM.ShowDialogue(hitInfo.transform.GetComponent<InteractionEvent>().GetDialogues());
    }
}
