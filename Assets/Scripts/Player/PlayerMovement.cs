using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;

    private void Awake()
    {
        //grab reference for rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flip player when moving left - right
        if (horizontalInput > 0.1f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        //name của animator đã set tên là "run" với 2 trạng thái idle (chờ, đứng yên) - false và run (chạy) - true
        //horizontalInput != 0 check true or false
        //true -> chạy -> dùng animation run
        //false -> đứng yên -> dùng animation idle

        animator.SetBool("grounded", IsGrounded());

        //print(OnWall());

        //wall jump logic
        if (wallJumpCoolDown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetTrigger("jump");

        }
        else if (OnWall() && !IsGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Math.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Math.Sign(transform.localScale.x), transform.localScale.y,
                                                transform.localScale.z);
 
            }
            else
            {
                body.velocity = new Vector2(-Math.Sign(transform.localScale.x) * 3, 6);
                //Debug.Log(body.velocity.x + " /-/ " + body.velocity.y);

            }
            wallJumpCoolDown = 0;

        }

    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && IsGrounded() && !OnWall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Dang va cham voi " + collision.gameObject.name);
    }
}


