using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.enabled = false;
    }


}
