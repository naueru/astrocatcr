using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSocket : MonoBehaviour
{
    public bool isFree = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Gone");
    }
}
