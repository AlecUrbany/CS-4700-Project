
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 20f;
    private void OnTriggerEnter(Collider other)
    {
            Destroy(bullet);
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerHealth playerScript = other.gameObject.GetComponent<PlayerHealth>();
                if(playerScript != null)
                {
                    playerScript.TakeDamage(damage);
                }
            }
        
    }
}
