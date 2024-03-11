using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    private bool fliped = true;

    // Update is called once per frame
    void Update()
    {
        if (!target) 
            getTarget();
        else
        {
            Vector2 direccion = target.transform.position - transform.position;
            direccion.Normalize();
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);

        if (direccion.x > 0 && !fliped)
            FlipEnemy();
        else if (direccion.x < 0 && fliped)
            FlipEnemy();
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

    private void getTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FlipEnemy()
    {
        fliped = !fliped;
        transform.Rotate(0, 180, 0);
    }
}
