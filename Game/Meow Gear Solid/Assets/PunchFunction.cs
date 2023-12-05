using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFunction : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject bulletPrefab;
    public Transform rightArm;
    public Transform leftArm;
    public Transform rightLeg;

    public int punchNumber;
    public bool IsAttacking;
    public bool inAnimation;
    public float attackTime = 4f;

    [SerializeField] private ItemData gunData;
    public PlayerInventoryControls gunMagazine;
    public bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        gunData = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<InventoryMenu>().equipedItem;
        gunMagazine = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryControls>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        rightArm = GameObject.FindGameObjectWithTag("HandRight").GetComponent<Transform>();
        leftArm = GameObject.FindGameObjectWithTag("HandLeft").GetComponent<Transform>();
        rightLeg = GameObject.FindGameObjectWithTag("LegRight").GetComponent<Transform>();
        punchNumber = 1;
        inAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            IsAttacking = true;
            if((punchNumber == 1) && inAnimation == false)
            {
                playerAnimator.SetBool("IsAttacking", IsAttacking);
                playerAnimator.SetInteger("MeleeAttack", punchNumber);
                Punch(rightArm);
                punchNumber = 2;
            }
            if((punchNumber == 2) && inAnimation == false)
            {
                playerAnimator.SetBool("IsAttacking", IsAttacking);
                playerAnimator.SetInteger("MeleeAttack", punchNumber);
                Punch(leftArm);
                punchNumber = 3;
            }
            if((punchNumber == 3) && inAnimation == false)
            {
                playerAnimator.SetBool("IsAttacking", IsAttacking);
                playerAnimator.SetInteger("MeleeAttack", punchNumber);
                Punch(rightLeg);
                punchNumber = 1;
            }
        }

    }
    void Punch(Transform limb)
    {
        inAnimation = true;
        GameObject hitBox = Instantiate(bulletPrefab, limb.position, limb.rotation);
        hitBox.transform.parent = limb;
        StartCoroutine(BulletLife(1f, hitBox));
    }
    IEnumerator BulletLife(float timer, GameObject hitBox)
    {
        yield return new WaitForSeconds(timer);
        Destroy(hitBox);
        inAnimation = false;
        IsAttacking = false;
    }
    IEnumerator AnimationWindow(float timer)
    {
        yield return new WaitForSeconds(timer);
        IsAttacking = false;
        punchNumber = 1;
    }
}
