using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float limitTime;
    public TextMeshProUGUI time;

    [SerializeField] GameObject badEndingEvent;

    private void Update()
    {
        limitTime -= Time.deltaTime;
        time.text = Mathf.Round(limitTime) + "√ ";

        if(limitTime == 0)
        {
            badEndingEvent.SetActive(true);
        }
    }
}
