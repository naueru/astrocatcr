using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    Transform mesh = null;
    public const float REMESH_COORD = 180;
    // Start is called before the first frame update
    void Start()
    {
        PickRandomMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickRandomMesh()
    {
        if (mesh) mesh.gameObject.SetActive(false);

        Debug.Log(transform.childCount);
        int rnd = Random.Range(0, transform.childCount - 1);
        mesh = transform.GetChild(rnd);
        mesh.gameObject.SetActive(true);

        //if (mesh) mesh.gameObject.SetActive(false);
        //Transform meshList = transform.Find("Decorations");
        //Debug.Log(meshList.childCount);
        //int rnd = Random.Range(0, meshList.childCount - 1);
        //mesh = meshList.GetChild(rnd);
        //mesh.gameObject.SetActive(true);

        // foreach (Transform child in transform)
        // {
        // if (child.CompareTag("Tile")) child.gameObject.SetActive(false);
        // }
    }

    void ClearMeshes()
    {

        Transform meshList = transform.Find("Decorations");
        int rnd = Random.Range(0, meshList.childCount - 1);
        Transform mesh = meshList.GetChild(rnd);
        mesh.gameObject.SetActive(true);

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Tile")) child.gameObject.SetActive(false);
        }
    }
}
