using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead_zone : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void FixedUpdate()
    {
        if (!(player.position.x < transform.position.x))
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("ostio puta tio que has perdido mamawebo");
        }
    }
}
