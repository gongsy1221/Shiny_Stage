using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAni : MonoBehaviour
{
    public int rotSpeed = -60;

    private void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }
}
