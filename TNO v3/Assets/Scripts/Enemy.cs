using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float vel;
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
        this.vel = GameManager.instance.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, layerGround);

        if (isCalm)
        {
            rb2D.velocity = new Vector2(vel, rb2D.velocity.y);

            ChangeWalk(platformKind);
        }

        PlayAnimation();
    }

    void Flip()
    {
        if (facingRight && vel <= 0 || !facingRight && vel >= 0)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void PlayAnimation()
    {
        if (isCalm)
        {
            anim.Play("Run");
        }
        else if (isAttacking)
        {
            anim.Play("Attack");
        }
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
                vel = -vel;
                Flip();
            }
        } else if (!platformCheck)
        {
            if (grounded)
            {
                vel = -vel;
                Flip();
            }
        }

    }
}
