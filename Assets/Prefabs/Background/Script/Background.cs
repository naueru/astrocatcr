using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{


    public float speed = 5f;
    public const float PANE_HEIGHT = 180;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -PANE_HEIGHT)
        {
            Vector3 temp = new(0, -transform.position.y + PANE_HEIGHT, 0);
            transform.position += temp;
        }
        transform.Translate(speed * Time.deltaTime * Vector3.down);
    }
}
