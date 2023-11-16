using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunFunctions : MonoBehaviour
{
    //Need to implement bullet destruction when colliding with walls
    public LayerMask obstacleMask;
    public GameObject bulletPrefab;
    public Transform barrel;
    public float reloadSpeed = 2f;
    public float bulletSpeed = 25.0f;
    public float burstSpeed = .01f;

    public bool isReloading;
    public GameObject sightline;
    void Start()
    {
        isReloading = false;
    }


    void Update()
    {
        bool canSeePlayer = sightline.GetComponent<visionCone>().canSeePlayer;
        if(canSeePlayer == true && isReloading == false)
        {
            StartCoroutine(Shoot3());
            isReloading = true;
            
        }
    }

    IEnumerator Shoot3()
    {
        Shoot();
        yield return new WaitForSeconds(burstSpeed);
        Shoot();
        yield return new WaitForSeconds(burstSpeed);
        Shoot();
        yield return new WaitForSeconds(reloadSpeed);
        isReloading = false;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        //Destroy(newBullet.GetComponent<EnemyGunFunctions>());
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
