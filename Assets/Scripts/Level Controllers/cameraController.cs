using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraDistance;

    private void Update()
    {

        transform.position = new Vector3(cameraDistance + player.position.x, transform.position.y, transform.position.z);
                
    }
}
