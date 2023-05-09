using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;

public class POS
{
    public int position { get; set; }
}
public class BE_EVENT
{
    public string id { get; set; }
    public POS value { get; set; }
}

public class MultiplayerConnection : MonoBehaviour
{
    private static SocketIO client;

    private string URL = "http://localhost:3000";
    //private string URL = "https://www.astrocatcircuitrescue.com/";
    async void Start()
    {
        client = new SocketIO(URL, new SocketIOOptions
        {
            Query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token", "UNITY"),
            }
        });



        client.On("existingUsers", response =>
        {
            var users = response.GetValue<List<BE_EVENT>>(0);
            Debug.Log($"${users[0].id} ${users[0].value.position}");


            if (PlayerEvents.ON_NEW_PLAYER != null)
            {
                for(int i = 0; i < users.Count; i++)
                {
                    PlayerEvents.ON_NEW_PLAYER(users[i].id, users[i].value.position);
                }
            }
        });


        client.On("newUser", response =>
        {
            string id = response.GetValue<string>(0);
            if (PlayerEvents.ON_NEW_PLAYER != null) PlayerEvents.ON_NEW_PLAYER(id, 0);
        });

        client.On("moveUser", response =>
        {
            string id = response.GetValue<string>(0);
            int position = response.GetValue<int>(1);

            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                if (PlayerEvents.ON_SET_POS != null) PlayerEvents.ON_SET_POS(id, position);
            });

            Debug.Log($"entro ${id} dir: ${position}");
        });

        client.OnConnected += async (sender, e) =>
        {

            Debug.Log(client.Id);
            /*
            await client.EmitAsync("hi", "socket.io");
            var data = new Dictionary<string, string>();
            data["jsonKey"] = "jsonValue";
            await client.EmitAsync("class", data);
            */
        };

        await client.ConnectAsync();
    }

    public static async void EmitMove(int position)
    {
        try
        {
            await client.EmitAsync("moveUser", position);
        } catch (Exception error)
        {
            Debug.Log(error);
        }
    }
}
