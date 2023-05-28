using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    AudioSource audioData;
    public float speed = 1f;
    public float animationSpeed = 5f;
    public bool isDebuging = false;
    public bool[,] matrix;
    public string VariantName = "L";
    public GameObject tileObj;
    private bool isComplete = false;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        PieceVariants creator = new PieceVariants();
        matrix = creator.GetVariantByName(VariantName);
        InitMatrix();
    }

    void Update()
    {
        if (isComplete)
        {
            if (transform.position.z < 20)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * animationSpeed * 2);
            }
            transform.Translate(Vector3.down * Time.deltaTime * animationSpeed);
        }
        else
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
        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if(matrix[y, x])
                {
                    AttachTile(x, y);
                }
            }
        }
    }

    public bool CheckCompletedPiece()
    {
        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (matrix[y, x] == false) return false;
            }
        }
        return true;
    }

    public void AddTile(int x, int y)
    {
        if (isComplete) return;
        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);

        if (y == height - 1)
        {
            bool[,] temp = new bool[height + 1, width];
            int tempWidth = temp.GetLength(1);
            int tempHeight = temp.GetLength(0);

            for (int newY = 0; newY < tempHeight; newY++)
            {
                for (int newX = 0; newX < tempWidth; newX++)
                {
                    temp[newY, newX] = newY == tempHeight - 1 ? false : matrix[newY, newX];
                }
            }
           matrix = temp;
        }

        matrix[y + 1, x] = true;

        AttachTile(x, y, 1);

        isComplete = CheckCompletedPiece();
        if (isComplete)
        {
            audioData.Play(0);
            DisableCollidersInChildren(transform);
        }

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
