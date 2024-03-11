using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float cooldawn;
    [SerializeField] private float spawnDistance;
    private float timer;

    private int targetCreated;
    private int targetMilestone = 10;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = cooldawn;
            targetCreated++;

            if (targetCreated > targetMilestone && cooldawn > 0.5f){
                targetMilestone += 10;
                cooldawn -= 0.3f;
            }
            
            GameObject newTarget = targetPrefab;
            float randomY = Random.Range( cd.bounds.min.y, cd.bounds.max.y );
            Debug.Log(transform.position.x);
            Instantiate(newTarget, new Vector2(transform.position.x, randomY), Quaternion.identity);

        }

        if (!(player.position.x < transform.position.x))
        {
            transform.position = new Vector3(player.position.x - spawnDistance, transform.position.y, transform.position.z);
        }
    }
}
