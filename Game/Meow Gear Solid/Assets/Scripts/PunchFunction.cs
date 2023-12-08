using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFunction : MonoBehaviour
{
    public Animator playerAnimator;
    public GameObject HitBoxPrefab;
    public Transform rightArm;
    public Transform leftArm;
    public Transform rightLeg;

    public int punchNumber;
    public bool IsAttacking;
    public bool inAnimation;
    public bool hasHitBoxOut;

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
        hasHitBoxOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && IsAttacking == false && inAnimation == false)
        {
            if((punchNumber == 1) && inAnimation == false && hasHitBoxOut == false)
            {
                playerAnimator.SetInteger("MeleeAttack", punchNumber);
                Punch(rightArm);
                punchNumber = 2;
                StartCoroutine(Timeout());
            }
            if((punchNumber == 2) && inAnimation == false)
            {
                playerAnimator.SetInteger("MeleeAttack", punchNumber);
                Punch(leftArm);
                StartCoroutine(Timeout());
                punchNumber = 3;
            }
            if((punchNumber == 3) && inAnimation == false)
            {
                playerAnimator.SetInteger("MeleeAttack", punchNumber);
                Punch(rightLeg);
                punchNumber = 1;
            }
        }

    }
    void Punch(Transform limb)
    {
        StopCoroutine("Timeout");
        inAnimation = true;
        IsAttacking = true;
        playerAnimator.SetBool("IsAttacking", IsAttacking);
        if(hasHitBoxOut == false)
        {
            hasHitBoxOut = true;
            GameObject hitBox = Instantiate(HitBoxPrefab, limb.position, limb.rotation);
            hitBox.transform.parent = limb;
            StartCoroutine(HitBoxLife(1f, hitBox));
        }

    }
    IEnumerator HitBoxLife(float timer, GameObject hitBox)
    {
        yield return new WaitForSeconds(timer);
        Destroy(hitBox);
        hasHitBoxOut = false;
        inAnimation = false;
        IsAttacking = false;
        playerAnimator.SetBool("IsAttacking", IsAttacking);
    }
    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(2.5f);
        inAnimation = false;
        IsAttacking = false;
        playerAnimator.SetBool("IsAttacking", IsAttacking);
        punchNumber = 1;
    }
}
