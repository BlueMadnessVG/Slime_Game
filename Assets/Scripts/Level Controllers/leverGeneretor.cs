using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class leverGeneretor : MonoBehaviour
{
    private CompositeCollider2D cd;
    [SerializeField] private Transform player;
    [SerializeField] private float generetorDistance;

    private float groundEnd;
    private float screenhedge;
    private bool didGenerateGround = false;

    private void Start()
    {
        cd = GetComponentInChildren<CompositeCollider2D>();
        groundEnd = transform.position.x + ( cd.bounds.size.x / 2 );
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        screenhedge = (Camera.main.aspect * Camera.main.orthographicSize) + Camera.main.transform.position.x;

        if ( groundEnd < (Camera.main.transform.position.x - (Camera.main.aspect * Camera.main.orthographicSize) * 2))
        {
            Destroy(gameObject);
            return;
        }

        if ( (groundEnd < screenhedge) && !didGenerateGround )
        {
            didGenerateGround = true;
            generateGround();
        }
        
    }

    private void generateGround()
    {
        GameObject go = Instantiate( gameObject );
        Vector2 pos;

        pos.x = groundEnd + 20;
        pos.y = transform.position.y;
        go.transform.position = pos;
    }
}
