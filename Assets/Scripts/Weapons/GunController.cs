using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] private Transform gun;
    [SerializeField] private Animator gunAnimation;
    [SerializeField] private float gunDistance;

    private bool fliped = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    //// Update is called once per frame
    //void Update()
    //{
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector3 direccion = mousePos - transform.position;

    //    gun.rotation = Quaternion.Euler(new Vector3( 0, 0, Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg) );

    //    float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
    //    gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0 ,0);

    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //        Shoot( direccion );


    //    if (mousePos.x < gun.position.x && fliped)
    //        FlipGun();
    //    else if (mousePos.x > gun.position.x && !fliped)
    //        FlipGun();
    //}

    private void FlipGun()
    {
        fliped = !fliped;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot( Vector3 direction )
    {
        gunAnimation.SetTrigger("Shoot");

        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

        Destroy(newBullet, 7);
    }
}
