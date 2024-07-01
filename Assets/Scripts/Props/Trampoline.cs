using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float upPower;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D playerRigid = collision.gameObject.GetComponent<Rigidbody2D>();
            animator.SetTrigger("up");
            playerRigid.AddForce(new Vector2(0, upPower));
        }
    }
}
