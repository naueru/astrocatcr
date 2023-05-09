using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3f;
    float elapsedTime = 0f;

    public float speed = 10.0f;

    void Update()
    {
        if (elapsedTime >= life) Destroy(gameObject);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        elapsedTime += Time.deltaTime;
    }

}
