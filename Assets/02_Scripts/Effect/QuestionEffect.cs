using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionEffect : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector3 targetPos = new Vector3();

    [SerializeField] ParticleSystem ps_Effect;

    public static bool isCollide = false;

    public void SetTarget(Vector3 _target)
    {
        targetPos = _target;
    }

    private void Update()
    {
        if (targetPos != Vector3.zero)
        {
            ps_Effect.gameObject.SetActive(true);
            ps_Effect.transform.position = targetPos;
            ps_Effect.Play();
            isCollide = true;
            targetPos = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
