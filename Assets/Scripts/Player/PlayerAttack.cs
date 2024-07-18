using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 10;
    //sound

    private float saveStartCooldown;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        saveStartCooldown = attackCooldown;
    }

    private void Update()
    {
        //kiem tra xem chuot trai co duoc click khong
        if (Input.GetKeyDown(KeyCode.W) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        //pool fireballs

        fireballs[FindFireBall()].transform.position = firePoint.position;
        fireballs[FindFireBall()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    public float GetStartAttackCooldown() => saveStartCooldown;

    public void NewAttackCooldown(float newAttackCooldown)
    {
        attackCooldown = newAttackCooldown;
    }


}
