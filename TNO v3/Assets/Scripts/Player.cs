using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, ICharacterBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool grounded, isAlive = true, isDead = false, levelComplete = false;
    [SerializeField]
    private LayerMask layerGround;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float radiusCheck;
    private Rigidbody2D rb2D;
    private Animator anim;
    private Vector2 direction;
    private float move;
    private bool facingRight = true;
    [SerializeField]
    private float knockBack;
    private PlatformEffector2D platform;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        platform = FindObjectOfType<PlatformEffector2D>();
    }
    
    void Update()
    {
        if (!grounded && rb2D.velocity.y > 5f)
            grounded = false;
        else
            grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        PlayAnimations();

        Flip();

        if (transform.position.y < -8f && isAlive)
        {
            PlayerDie();
        }
    }

    private void FixedUpdate()
    {
        move = CrossPlatformInputManager.GetAxis("Horizontal");
        if (isAlive)
        {
            MoveCharacter(move, speed); 
        }
    }

    void PlayAnimations()
    {
        if (grounded && rb2D.velocity.x == 0)
        {
            anim.SetBool("Moving", false);
        }
        else if (grounded && rb2D.velocity.x != 0)
        {
            anim.SetBool("Moving", true);
        }

        if (grounded)
            anim.SetBool("Jumping", false);
        else if (!grounded)
            anim.SetBool("Jumping", true);
    }

    public void PlayerJump()
    {
        if (grounded && isAlive)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            PlayerWin();
        }
    }

    void PlayerDie()
    {
        anim.SetTrigger("PlayerDie");
        isAlive = false;
        isDead = true;
        rb2D.velocity = new Vector2(0f, 0f);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(8, 10);
        rb2D.AddForce(new Vector2(0f, knockBack));
        platform.useColliderMask = false;
    }

    void PlayerWin()
    {
        isAlive = false;
        levelComplete = true;
        rb2D.velocity = Vector2.zero;
        anim.SetTrigger("PlayerWin");
    }

    void DieAnimationFinished()
    {
            GameManager.instance.SetStatus(GameManager.GameStatus.DIE);
            GameManager.instance.dieMenu.SetActive(true);   
    }

    public void WinAnimationFinished()
    {
        GameManager.instance.SetStatus(GameManager.GameStatus.WIN);
        GameManager.instance.winMenu.SetActive(true);
    }

    public void MoveCharacter(float moveDirection, float characterSpeed)
    {
        rb2D.velocity = new Vector2(moveDirection * characterSpeed, rb2D.velocity.y);
    }

    public void Flip()
    {
        if (facingRight && move < 0 || !facingRight && move > 0)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
