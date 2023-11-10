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

    [SerializeField] private ItemData pistolData;
    public List<Transform> playerMagazine = new List<Transform>();
    private Transform playerBulletIcon;

    void Start()
    {
        pistolData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        playerMagazine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>().magazineCount;
        playerBulletIcon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>().newBullet;

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (pistolData.currentAmmo > 0) {
                Shoot();
                pistolData.currentAmmo --;
                playerMagazine.Remove(playerBulletIcon);
            }
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
