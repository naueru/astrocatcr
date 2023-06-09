using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public string PlayerId = Globals.DEFAULT_PLAYER_NAME;
    public PlayerStats P_Stats;
    public GameObject bullet;
    public Vector3 currentPos;

    private bool isMoving;
    private Coroutine courtine;

    public GameObject AimRender;
    private GameObject aimInstance;
    private RaycastHit2D aim;

    void Start()
    {
        currentPos = transform.position;
        PlayerEvents.ON_MOVE += Move;
        PlayerEvents.ON_SET_POS += SetPos;
        PlayerEvents.ON_FIRE += Fire;
        aimInstance = Instantiate(AimRender, transform.position, Quaternion.Euler(Vector3.zero));
    }


    public void SetPos(string id, int position) {
        if (PlayerId != id) return;
        currentPos = new Vector3(position, currentPos.y, currentPos.z);
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

    private void Update()
    {
        int distance = 300;
        aim = Physics2D.Raycast(transform.position, Vector2.up, distance);

        aimInstance.transform.position = new Vector3(0, -100, -400);

        if (aim.collider && aim.collider.tag == "Tile")
        {
            aimInstance.transform.position = new Vector3(currentPos.x, aim.collider.transform.position.y - 1, currentPos.z);
            Debug.DrawRay(transform.position, Vector2.up * aim.collider.transform.position.y, Color.red);
        }
    }
}
