using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    //Coyote time
    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; //how much time the player can hang in the air before jumping
    private float coyoteCounter; //how much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;


    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;
    //sound
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;


    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    //private float wallJumpCoolDown;
    private float horizontalInput;

    private void Awake()
    {
        //grab reference for rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Application.targetFrameRate = 120;
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

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        if (OnWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (IsGrounded())
            {
                coyoteCounter = coyoteTime; //reset coyote counter when on the ground
                jumpCounter = extraJumps; //reset extra jump when on ground
            }
            else
            {
                coyoteCounter -= Time.deltaTime; //start decreasing coyote counter when not on the ground
            }
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !OnWall() && jumpCounter <= 0) return;
        //if coyote counter is <= 0 and not on the wall and don't have extra jump don't do anything
        SoundManager.instance.PlaySound(jumpSound);

        if (OnWall())
        {
            WallJump();
        }

        else
        {
            if (IsGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                //if not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
                else
                {
                    if (jumpCounter > 0) //if we have extra jump
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }
            //reset coyote counter to 0 to avoid double jump
            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        //wallJumpCoolDown = 0;
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


