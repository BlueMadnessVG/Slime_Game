using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject
{
    public Vector2 Position;
    public Vector2 MapPosition;
    public Vector2 Direction;
    public float ChanceToChange;

    public WalkerObject(Vector2 pos, Vector2 mapPos, Vector2 dir, float changeToChange)
    {
        Position = pos;
        MapPosition= mapPos;
        Direction = dir;
        ChanceToChange = changeToChange;
    }
}
