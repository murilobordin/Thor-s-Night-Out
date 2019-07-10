using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacterBehaviour
{
    public float speed, move =1f;
    [SerializeField]
    private bool facingRight = true, grounded, isCalm = true, isAttacking = false, platformKind = true;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask layerGround;
    private Rigidbody2D rb2D;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        this.speed = GameManager.instance.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, layerGround);

        if (isCalm)
        {
            MoveCharacter(move, speed);
            ChangeWalk(platformKind);
        }

        PlayAnimation();
    }

    void PlayAnimation()
    {
        if (isCalm)
            anim.SetBool("Attacking",false);
        else if (isAttacking)
            anim.SetBool("Attacking", true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCalm = false;
            isAttacking = true;
            rb2D.velocity = new Vector2(0f, 0f);
        }
    }

    private void ChangeWalk(bool platformCheck)
    {
        if (platformCheck)
        {
            if (!grounded)
            {
                Flip();
            }
        } else if (!platformCheck)
        {
            if (grounded)
            {
                Flip();
            }
        }
    }

    public void MoveCharacter(float moveDirection, float characterSpeed)
    {
        rb2D.velocity = new Vector2(move * speed, rb2D.velocity.y);
    }

    public void Flip()
    {
        move = -move;
        if (facingRight && move <= 0 || !facingRight && move >= 0)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
