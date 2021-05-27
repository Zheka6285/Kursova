using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static float speed;
    public float maxSpeed = 10;
    
    void FixedUpdate()
    {
        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        if (transform.position.z == -1)
            DestroyCoin();
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
