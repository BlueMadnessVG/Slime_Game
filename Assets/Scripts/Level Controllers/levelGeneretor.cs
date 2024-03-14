using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class weabeGenerator : MonoBehaviour
{

    [SerializeField] private Tiles[] RoomsOptions;
    private List<RoomCreated> Roomslist = new List<RoomCreated>();

    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        var newObject = Instantiate(RoomsOptions[1], transform.position, Quaternion.identity);

        Tiles hola = (Tiles)newObject.GetComponent(typeof(Tiles));

        Debug.Log(hola.Exits.Count());

        direction = Random.Range(0, hola.Exits.Count());
        Debug.Log( hola.Exits[direction].gameObject.name == "frontExit");



        RoomCreated comoEstas = new RoomCreated();
        comoEstas.frontExit = true;
        Roomslist.Add(comoEstas);

        Debug.Log(hola.frontNeigbours[3].GetComponentInChildren<Tilemap>().size.x * 0.5f);

        var location = hola.Exits[0].position.x + (hola.frontNeigbours[3].GetComponentInChildren<Tilemap>().size.x * 0.5f);

        var newTile = Instantiate(hola.frontNeigbours[3], new Vector2(location, hola.Exits[0].position.y), Quaternion.identity);

        location = newTile.Exits[0].position.x + (newTile.frontNeigbours[0].GetComponentInChildren<Tilemap>().size.x * 0.5f);
        newTile = Instantiate(newTile.frontNeigbours[0], new Vector2(location, newTile.Exits[0].position.y), Quaternion.identity);

        direction = Random.Range(0, newTile.Exits.Count());
        Debug.Log(direction + " " + newTile.Exits.Count());
        Debug.Log(newTile.Exits[direction]);
    }

    private void Move()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class RoomCreated
    {
        public bool frontExit;
        public bool backExit;
        public bool topExit;
        public bool downExit;
    }
}


