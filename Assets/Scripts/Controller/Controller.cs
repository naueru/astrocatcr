using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerStats P1;
    private Throttle _moveThrottle = new Throttle();
    private Throttle _fireThrottle = new Throttle();

    private float horizontal;
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0) {
            _moveThrottle.Run(RunMove, P1.moveSpeed);
        } else {
            _moveThrottle.ResetTimer();
        }

        if (Input.GetButton("Fire1"))
        {
            _fireThrottle.Run(RunFire, P1.fireSpeed);
        }
    }
    
    void RunMove () {
        if(PlayerEvents.ON_MOVE != null) {
            int floored = horizontal <= 0f ? -1 : 1;
            PlayerEvents.ON_MOVE(Globals.DEFAULT_PLAYER_NAME, floored);
        }
    }

    void RunFire()
    {
        if (PlayerEvents.ON_FIRE != null)
        {
            PlayerEvents.ON_FIRE(Globals.DEFAULT_PLAYER_NAME);
        }
    }
}
