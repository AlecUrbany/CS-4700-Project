using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunctions : MonoBehaviour
{
    //Need to implement bullet destruction when colliding with walls
    public LayerMask obstacleMask;
    public GameObject bulletPrefab;
    public Transform barrel;
    public float reloadSpeed = 1.5f;
    public float bulletSpeed = 25.0f;
    private GameObject currentBullet;
    private Rigidbody bulletRigidbody;

    [SerializeField] private ItemData gunData;
    public PlayerInventoryControls gunMagazine;
    public bool isReloading;

    void Start()
    {
        gunData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        gunMagazine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>();
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")&& isReloading == false)
        {
            if (gunData.currentAmmo > 0)
            {
                if (gunData.magazine > 0)
                {
                    Shoot();
                    gunMagazine.DecreaseMagazine();
                    gunData.magazine --;
                    gunData.currentAmmo --;
                }
                else
                {
                    isReloading = true;
                    Reload(reloadSpeed);

                }
            }
            else
            {
                //add a blank cartridge sfx here
            }
        }
        if(Input.GetButtonDown("Reload"))
        {
                    isReloading = true;
                    Reload(reloadSpeed);
                    
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
    
    void Reload(float reloadSpeed)
    {
        StartCoroutine(ReloadTime(reloadSpeed));
    }
    IEnumerator BulletLife(float timer, GameObject newBullet)
    {
        yield return new WaitForSeconds(timer);
        Destroy(newBullet);
    }

    IEnumerator ReloadTime(float timer)
    {
        yield return new WaitForSeconds(timer);

        if(gunData.currentAmmo >= gunData.magazineSize)
        {
            gunData.magazine = gunData.magazineSize;
        }
        else
        {
            gunData.magazine = gunData.currentAmmo;
        }
        gunMagazine.ReloadMagazine();
        isReloading = false;
    }

}
