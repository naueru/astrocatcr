using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public string PlayerId = Globals.DEFAULT_PLAYER_NAME;
    public PlayerStats P_Stats;
    public GameObject bullet;
    private bool isMoving;
    public Vector3 currentPos;

    private Coroutine courtine;

    void Start()
    {
        currentPos = transform.position;
        PlayerEvents.ON_MOVE += Move;
        PlayerEvents.ON_SET_POS += SetPos;
        PlayerEvents.ON_FIRE += Fire;
    }


    public void SetPos(string id, int position) {
        if (PlayerId != id) return;
        currentPos = new Vector3(position, currentPos.y, currentPos.z);
        if (isMoving)
        {
            StopCoroutine(courtine);
            isMoving = false;
        }
        courtine = StartCoroutine(MoveCoroutine());
    }

    void Move (string id, int direction) {

        if (PlayerId != id) return;
        currentPos =  new Vector3(currentPos.x + direction * Globals.STEP_SIZE, currentPos.y, currentPos.z);
        MultiplayerConnection.EmitMove((int)currentPos.x);

        if (isMoving) {
            StopCoroutine(courtine);
            isMoving = false;
        }
        courtine = StartCoroutine(MoveCoroutine());    
    }

    void Fire(string id)
    {
        if (PlayerId != id) return;
        Instantiate(bullet, currentPos, Quaternion.Euler(Vector3.zero));
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;
        
        while (elapsedTime < P_Stats.moveSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, currentPos , elapsedTime / P_Stats.moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = currentPos;
        isMoving = false;
    }
}
