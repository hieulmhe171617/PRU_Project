using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    [SerializeField] private float health;
    [SerializeField] private float healthRange;
    [SerializeField] private float damage;


    //private Animator camAnim;
    public Slider healthBar;
    [SerializeField] private Animator anim;
    public bool isDead;
    private bool isRange = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (health <= healthRange)
        {
            anim.SetTrigger("stageTwo");
            if (!isRange)
            {
                damage = damage + 0.5f;
                transform.localScale = new Vector3(0.5f, 0.5f, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                isRange = true;
            }
        }

        if (health <= 0)
        {
            anim.SetTrigger("death");
        }



        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false)
        {

            //camAnim.SetTrigger("shake");
            other.GetComponent<Health>().TakeDamage(damage);

        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }
}
