using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public GameObject PlayerInstance;
    void Start()
    {
        PlayerEvents.ON_NEW_PLAYER += CreatePlayer;
    }

    // Update is called once per frame
    void CreatePlayer(string id, int position)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            GameObject nPlayer = Instantiate(PlayerInstance, transform);
            PlayerMovment dataPlayer = nPlayer.GetComponent<PlayerMovment>();
            dataPlayer.PlayerId = id;
            dataPlayer.SetPos(id, position);
        });
    }
}
