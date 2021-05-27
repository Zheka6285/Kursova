using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    public float speedRotate = 50;
   
    void FixedUpdate()
    {
        transform.Rotate(0, speedRotate * Time.deltaTime, 0);
    }
}
