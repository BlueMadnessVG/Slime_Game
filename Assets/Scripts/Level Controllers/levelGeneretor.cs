using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class weabeGenerator : MonoBehaviour
{

    private enum Grid
    {
        ROOM,
        EMPTY
    }

    [SerializeField] private Tiles[] RoomsOptions;
    private List<RoomCreated> Roomslist = new List<RoomCreated>();

    private int direction;
    private int newRoom;

    private RoomCreated[,] RoomsHandler;
    public List<WalkerObject> Walkers;
    public int MapWidth = 10;
    public int MapHeight = 10;

    public int MaximumWalkers = 10;
    public int TileCount = default;
    public float FillPercentage = 0.3f;
    public float WaitTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        InitializeGrid();

        //GenerateTile(newObject);
        //GenerateTile(Roomslist[Roomslist.Count - 1].type);

        //var location = newTile.Exits[1].position.x - (newTile.frontNeigbours[0].GetComponentInChildren<Tilemap>().size.x * 0.5f);
        //newTile = Instantiate(newTile.frontNeigbours[0], new Vector2(location, newTile.Exits[0].position.y), Quaternion.identity);

        //direction = Random.Range(0, newTile.Exits.Count());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeGrid()
    {
        RoomsHandler = new RoomCreated[MapWidth, MapHeight];
        Walkers = new List<WalkerObject>();

        Vector3Int tileCenter = new Vector3Int(RoomsHandler.GetLength(0) / 2, RoomsHandler.GetLength(1) / 2, 0);

        WalkerObject curWalker = new WalkerObject(new Vector2(tileCenter.x, tileCenter.y), transform.position, GetDirection(), 0.5f);
        RoomsHandler[tileCenter.x, tileCenter.y] = new RoomCreated(RoomsOptions[3]);
        Instantiate(RoomsOptions[3], curWalker.MapPosition, Quaternion.identity);
        Walkers.Add(curWalker);

        TileCount++;

        Debug.Log(Walkers[0].Direction);
        StartCoroutine(CreateRooms());

    }

    private IEnumerator CreateRooms()
    {
        bool hasCreatedFloor = false;
       
        while ((float)TileCount / (float)RoomsHandler.Length < FillPercentage)
        {
            hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker.Position.x, (int)curWalker.Position.y, 0);
                if (RoomsHandler[curPos.x,curPos.y] == null)
                {
                    Instantiate(RoomsOptions[3], curPos, Quaternion.identity);
                    TileCount++;
                    RoomsHandler[curPos.x, curPos.y] = new RoomCreated(RoomsOptions[3]);
                    hasCreatedFloor = true;
                }
            }

            ChangeToRemove();
            ChangeToRedirect();
            ChangeToCreate();
            UpdatePosition();

            //if (hasCreatedFloor)
            //{
                yield return new WaitForSeconds(WaitTime);
            //}
            Debug.Log(((float)TileCount / (float)RoomsHandler.Length));
        }

        Debug.Log((float)TileCount / (float)RoomsHandler.Length);
    }

    private void GenerateTile(Tiles LastTile)
    {
        Debug.Log(Roomslist.Count);
        direction = Mathf.FloorToInt(UnityEngine.Random.Range(0, LastTile.Exits.Count()));
        direction = 0;
        Debug.Log(LastTile.Exits[direction].gameObject.name == "frontExit");
        RoomCreated comoEstas = new RoomCreated(RoomsOptions[3]);
        var _spawnPoint = new Vector2(0, 0);

        if (LastTile.Exits[direction].gameObject.name == "frontExit")
        {
            comoEstas.frontExit = true;
            newRoom = Mathf.FloorToInt(UnityEngine.Random.Range(0, LastTile.frontNeigbours.Count()));
            Debug.Log(newRoom);
            _spawnPoint = new Vector2((LastTile.Exits[direction].position.x + (LastTile.frontNeigbours[newRoom].GetComponentInChildren<Tilemap>().size.x * 0.5f)), LastTile.Exits[direction].position.y);
        }
        else if (LastTile.Exits[direction].gameObject.name == "backExit")
        {
            comoEstas.backExit = true;
            _spawnPoint = new Vector2((LastTile.Exits[direction].position.x - (LastTile.frontNeigbours[3].GetComponentInChildren<Tilemap>().size.x * 0.5f)), LastTile.Exits[direction].position.y);
        }
        else if (LastTile.Exits[direction].gameObject.name == "topExit")
        {
            comoEstas.topExit = true;
            _spawnPoint = new Vector2(LastTile.Exits[direction].position.x, (LastTile.Exits[direction].position.y + (LastTile.topNeigbours[1].GetComponentInChildren<Tilemap>().size.y * 0.5f)));
        }
        else
        {
            comoEstas.downExit = true;
            _spawnPoint = new Vector2(LastTile.Exits[direction].position.x, (LastTile.Exits[direction].position.y - (LastTile.topNeigbours[1].GetComponentInChildren<Tilemap>().size.y * 0.5f)));
        }

        comoEstas.numExits++;
        comoEstas.takenExits++;
        Roomslist.Add(comoEstas);
       
        var newTile = Instantiate(LastTile.frontNeigbours[newRoom], _spawnPoint, Quaternion.identity);
        comoEstas.type = newTile;
    }

    private Vector2 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.Range(0, 3));

        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    private void ChangeToRemove()
    {
        int updateCount = Walkers.Count;
        for (int i = 0; i < updateCount; i++)
        {
            if( UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count > 1)
            {
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    private void ChangeToRedirect()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if ( UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    private void ChangeToCreate()
    {
        int updateCount = Walkers.Count;
        for(int i = 0;i < updateCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection();
                Vector2 newPosition = Walkers[i].Position;
                Vector2 newMapPos = Walkers[i].MapPosition;

                WalkerObject newWalker = new WalkerObject(newPosition, newMapPos, newDirection, 0.5f);
                Walkers.Add(newWalker);
            }
        }
    }

    private void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers[i];
            FoundWalker.Position += FoundWalker.Direction;
            FoundWalker.Position.x = Mathf.Clamp(FoundWalker.Position.x, 1, RoomsHandler.GetLength(0) - 2);
            FoundWalker.Position.y = Mathf.Clamp(FoundWalker.Position.y, 1, RoomsHandler.GetLength(1) - 2);
            Walkers[i] = FoundWalker;
        }
    }

    private class RoomCreated
    {
        public Tiles type;
        public int numExits = 0;
        public int takenExits = 0;

        public bool frontExit;
        public bool backExit;
        public bool topExit;
        public bool downExit;

        public RoomCreated(Tiles selectedType)
        {
            type = selectedType;
        }
    }

}


