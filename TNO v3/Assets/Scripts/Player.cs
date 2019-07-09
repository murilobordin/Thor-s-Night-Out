using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private bool grounded, isAlive = true, isDead = false, levelComplete = false, timeIsOver = false;
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
    

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        platform = FindObjectOfType<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!grounded && rb2D.velocity.y > 5f)
            grounded = false;
        else
            grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);


        PlayAnimations();

        FacingRight();

        if (transform.position.y < -8f && isAlive)
        {
            PlayerDie();
        }

        

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            move = CrossPlatformInputManager.GetAxis("Horizontal");
            rb2D.velocity = new Vector2(move * speed, rb2D.velocity.y);
        }
    }

    void PlayAnimations()
    {
        if(grounded && move == 0 && isAlive)
        {
            anim.Play("Idle");
        } else if(grounded && move != 0 && isAlive)
        {
            anim.Play("Run");
        } else if(!grounded && isAlive)
        {
            anim.Play("Jump");
        } else if (isDead)
        {
            anim.Play("Die");
        } else if (levelComplete)
        {
            anim.Play("Win");
        }
    }

    void FacingRight()
    {
        if (facingRight && move < 0 || !facingRight && move > 0)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
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
            isAlive = false;
            levelComplete = true;
            rb2D.velocity = Vector2.zero;
        }
    }

    void PlayerDie()
    {
        isAlive = false;
        isDead = true;
        rb2D.velocity = new Vector2(0f, 0f);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(8, 10);
        rb2D.AddForce(new Vector2(0f, knockBack));
        platform.useColliderMask = false;
    }

    void DieAnimationFinished()
    {
        if (timeIsOver)
        {
            GameManager.instance.SetStatus(GameManager.GameStatus.LOSE);
            GameManager.instance.loseMenu.SetActive(true);
        }
        else
        {
            GameManager.instance.SetStatus(GameManager.GameStatus.DIE);
            GameManager.instance.dieMenu.SetActive(true);
        }     
    }

    public void WinAnimationFinished()
    {
        GameManager.instance.SetStatus(GameManager.GameStatus.WIN);
        GameManager.instance.winMenu.SetActive(true);
    }
}
