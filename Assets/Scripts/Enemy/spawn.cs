using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class spawn : MonoBehaviour
{

    public GameObject Pipe;
    public float spawnRate = 2;
    private float spawnTime = 0;
    public float heightOffset = 10;

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {

        if ( spawnTime < spawnRate )
        {
            spawnTime += Time.deltaTime;
        }else
        {
            spawnPipe();
            spawnTime = 0;
        }

    }

    void spawnPipe()
    {

        float lowestPoint = transform.position.y - heightOffset;
        float heighestPoint = transform.position.y + heightOffset;

        Instantiate(Pipe, new Vector3( transform.position.x, Random.Range(lowestPoint, heighestPoint), 0), transform.rotation);

    }
}
