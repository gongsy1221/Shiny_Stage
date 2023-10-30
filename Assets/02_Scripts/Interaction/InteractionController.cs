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
        if(hitInfo.collider.transform.CompareTag("Interaction"))
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
        ps_QuesttionEffect.transform.position = hitInfo.point;
        Vector3 t_targetPos = hitInfo.point;
        ps_QuesttionEffect.GetComponent<QuestionEffect>().SetTarget(t_targetPos);

        StartCoroutine(WaitCollision());
    }

    IEnumerator WaitCollision()
    {
        yield return new WaitUntil(()=>QuestionEffect.isCollide);
        QuestionEffect.isCollide = false;

        yield return new WaitForSeconds(0.5f);

        InteractionEvent t_Event = hitInfo.transform.GetComponent<InteractionEvent>();

        if (t_Event.GetAppearType() == AppearType.Appear)
        {
            theDM.SetAppearObjects(t_Event.GetTargets());
        }
        else if(t_Event.GetAppearType()==AppearType.Disappear)
        { 
            theDM.SetDisappearObjects(t_Event.GetTargets());
        }
        theDM.ShowDialogue(t_Event.GetDialogues());
    }
}
