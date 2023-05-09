using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public float speed = 1f;
    public float animationSpeed = 5f;
    public bool isDebuging = false;
    public bool[,] matrix;
    public string VariantName = "L";
    public GameObject tileObj;
    private bool isComplete = false;

    void Start()
    {
        PieceVariants creator = new PieceVariants();
        matrix = creator.GetVariantByName(VariantName);
        InitMatrix();
    }

    void Update()
    {
        if(isComplete)
        {
            if (transform.position.z < 20)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * animationSpeed * 2);
            }
            transform.Translate(Vector3.down * Time.deltaTime * animationSpeed);
        } else
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }

    public void InitMatrix()
    {
        MatrixToTile();
    }


    public void MatrixToTile()
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                if(matrix[x, y])
                {
                    AttachTile(x, y);
                }
            }
        }
    }

    public bool CheckCompletedPiece()
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            for (int y = 0; y < matrix.GetLength(1); y++)
            {                
                if (matrix[x, y] == false) return false;
            }
        }
        return true;
    }
    
    public void AddTile(int x, int y)
    {
        if (isComplete) return;

        if (y == matrix.GetLength(1) - 1)
        {           
            bool[,] temp = new bool[matrix.GetLength(0), matrix.GetLength(1) + 1];
            int height = temp.GetLength(1);

            for (int newX = 0; newX < temp.GetLength(0); newX++)
            {
                for (int newY = 0; newY < height; newY++)
                {
                    temp[newX, newY] = newY == height -1 ?  false : matrix[newX, newY];
                }
            }
           matrix = temp;          
        }

        matrix[x, y + 1] = true;
     
        AttachTile(x, y, 1);

        isComplete = CheckCompletedPiece();
        if(isComplete) DisableCollidersInChildren(transform);

    }

    void DisableCollidersInChildren(Transform parent)
    {
        BoxCollider2D collider = parent.GetComponent<BoxCollider2D>();
        if (collider != null) collider.enabled = false;

        foreach (Transform child in parent)
        {
            DisableCollidersInChildren(child);
        }
    }

    private void AttachTile(int x, int y, int deltaY = 0)
    {
        Vector3 pos = transform.position;
        GameObject newTile = Instantiate(tileObj, new Vector3(pos.x + x, pos.y - y - deltaY, pos.z), Quaternion.Euler(Vector3.zero));
        Tile ScriptTile = newTile.GetComponent<Tile>();
        ScriptTile.SetPos(x, y + deltaY);
        newTile.transform.parent = gameObject.transform;
    }

}
