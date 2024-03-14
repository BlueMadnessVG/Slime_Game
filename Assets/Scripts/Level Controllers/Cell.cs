using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool collapsed;
    public Tiles[] tileOptions;

    public void CreateCell(bool collapseState, Tiles[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
    }

    public void RecreateCell(Tiles[] tiles)
    {
        tileOptions = tiles;
    }
}
