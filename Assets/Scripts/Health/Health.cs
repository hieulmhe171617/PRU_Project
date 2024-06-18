using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;


    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private bool invulnerable;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            //iframes
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            { 
                //deactive all attached components class
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                if (gameObject.tag == "Player")
                {
                    anim.SetBool("grounded", true);
                }
                anim.SetTrigger("die");
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }

        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        //cần invulnerble ở đây bởi vì hiệu ứng chém của enemy đang vẽ bằng boxcast, ko phải collider của
        //enemy -> ko phát hiện được collion của 8 player và 9 enemy
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //khong bi thuong

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); // tạm dừng quá trình thực hiện hàm coroutine trong 1s
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
