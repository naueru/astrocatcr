using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x = 0;
    public int y = 0;

    public void SetPos(int newX, int newY)
    {
        x = newX;
        y = newY;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetComponentInParent<Piece>().AddTile(x, y);
            Destroy(collision.gameObject);
        }
    }
}
