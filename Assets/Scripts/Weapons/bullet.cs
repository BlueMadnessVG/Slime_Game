using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();


    // Update is called once per frame
    void Update() => transform.right = rb.velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
