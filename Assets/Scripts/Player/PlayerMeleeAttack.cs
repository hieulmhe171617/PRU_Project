using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    private float cooldownTimer = 0; //Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private Animator anim;
    private Health enemyHealth;
    private Health playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q) && cooldownTimer >= attackCooldown)
        {
            //co the attack
            cooldownTimer = 0;
            anim.SetTrigger("attack2");
            DamageEnemy();
        }
    }


    private bool EnemyInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance
            , new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            , 0, Vector2.left, 0, enemyLayer);
        if (hit.collider != null && hit.transform.gameObject.tag == "Enemy")
        {
            enemyHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    private void DamageEnemy() //event trong animation meleeAttack
    {
        if (EnemyInsight())
        {
            //damage player Health
            SoundManager.instance.PlaySound(attackSound);
            enemyHealth.TakeDamage(damage);
            playerHealth.AddHealth(0.5f);
            //Debug.Log("Da tan cong");
        }
    }
}
