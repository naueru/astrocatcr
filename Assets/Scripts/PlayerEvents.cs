using System;

public static class PlayerEvents 
{
    public static Action<string, int> ON_MOVE;
    public static Action<string, int> ON_SET_POS;
    public static Action<string> ON_FIRE;
    public static Action<string, int> ON_NEW_PLAYER;
}