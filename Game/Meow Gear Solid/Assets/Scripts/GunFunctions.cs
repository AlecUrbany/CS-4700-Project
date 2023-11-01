using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunctions : MonoBehaviour
{
    //Need to implement bullet destruction when colliding with walls
    public LayerMask obstacleMask;
    public GameObject bulletPrefab;
    public Transform barrel;
    public float bulletSpeed = 10.0f;
    private GameObject currentBullet;
    private Rigidbody bulletRigidbody;

    public ItemData itemData;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            itemData.currentAmmo --;
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        Destroy(newBullet.GetComponent<GunFunctions>());
        Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.velocity = barrel.forward * bulletSpeed;
        StartCoroutine(BulletLife(2, newBullet));


    }
    IEnumerator BulletLife(float timer, GameObject newBullet)
    {
        yield return new WaitForSeconds(timer);
        Destroy(newBullet);
    }

}
