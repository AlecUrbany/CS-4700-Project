
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 20f;
    void OnCollisionEnter(Collision collision)
    {
            Destroy(bullet);
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("BANG1");
                PlayerHealth playerScript = collision.gameObject.GetComponent<PlayerHealth>();
                if(playerScript != null)
                {
                    Debug.Log("BANG");
                    playerScript.TakeDamage(damage);
                }
            }
        
    }
}
