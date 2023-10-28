using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    bool isobjectActive = true;

    DialogueManager theDM;

    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectDestroy();
        }
    }

    public void ObjectDestroy()
    {
        if(isobjectActive && !theDM.isDialogue)
        {
            gameObject.SetActive(true);
            if(theDM.isDialogue && isobjectActive)
            {
                isobjectActive = false;
                if(!isobjectActive && !theDM.isDialogue )
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
