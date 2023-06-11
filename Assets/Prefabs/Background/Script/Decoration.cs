using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    Transform mesh = null;
    public const float REMESH_COORD = 180;

    void Start()
    {
        PickRandomMesh();
    }

    public void PickRandomMesh()
    {
        if (mesh) mesh.gameObject.SetActive(false);

        Debug.Log(transform.childCount);
        int rnd = Random.Range(0, transform.childCount - 1);
        mesh = transform.GetChild(rnd);
        mesh.gameObject.SetActive(true);

    }
}
