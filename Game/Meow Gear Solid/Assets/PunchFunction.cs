using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFunction : MonoBehaviour
{
    public LayerMask obstacleMask;
    public GameObject bulletPrefab;
    public Transform rightArm;
    public Transform leftArm;
    public Transform rightLeg;

    public int punchNumber;

    [SerializeField] private ItemData gunData;
    public PlayerInventoryControls gunMagazine;
    public bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        gunData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        gunMagazine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>();
        punchNumber = 1;
        rightArm = GameObject.FindGameObjectWithTag("HandRight").GetComponent<Transform>();
        leftArm = GameObject.FindGameObjectWithTag("HandLeft").GetComponent<Transform>();
        rightLeg = GameObject.FindGameObjectWithTag("LegRight").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(punchNumber == 1)
            {
                Punch(rightArm);
                punchNumber = 2;
            }
            if(punchNumber == 2)
            {
                Punch(leftArm);
                punchNumber = 3;
            }
            if(punchNumber == 3)
            {
                Punch(rightLeg);
                punchNumber = 1;
            }
        }
    }
    void Punch(Transform limb)
    {
        GameObject hitBox = Instantiate(bulletPrefab, limb.position, limb.rotation);
        StartCoroutine(BulletLife(2, hitBox));
    }
    IEnumerator BulletLife(float timer, GameObject hitBox)
    {
        yield return new WaitForSeconds(timer);
        Destroy(hitBox);
    } 
}
