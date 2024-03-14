using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Tiles[] topNeigbours;
    public Tiles[] downNeigbours;
    public Tiles[] frontNeigbours;
    public Tiles[] backNeigbours;

    public Transform[] Exits;
}
