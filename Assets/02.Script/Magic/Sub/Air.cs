using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    private Rigidbody2D rb;

    private SpriteRenderer render;

    private Animator anim;

    private Magic_Info m_info;

    private Vector2 MovePos;

    private float moveSpeed = 1.5f;


    private bool rot;
    private void Awake()
    {
        m_info = this.GetComponent<Magic_Info>();

        m_info.SetUpMagic(this.gameObject.name);

        anim = this.GetComponent<Animator>();

        rb = this.GetComponent<Rigidbody2D>();

        render = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        m_info.SetUpMagic(this.gameObject.name);
    }

    public void Direasd(bool isFlip)
    {
        rot = isFlip;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Magic-air-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Magic-air-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
        {

            if (rot)
            {

                rb.velocity = Vector2.left * moveSpeed;
            }

            if (!rot)
            {
                rb.velocity = Vector2.right * moveSpeed;
            }
            //rb.velocity = MovePos * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
