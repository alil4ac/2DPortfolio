using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Air : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    private Magic_Info m_info;

    private float moveSpeed = 1.5f;

    private Vector2 tarpos;

    private bool rot;
    private void Awake()
    {
        //m_info = new Magic_Info();

        m_info = this.GetComponent<Magic_Info>();

        m_info.SetUpMagic(this.gameObject.name);

        anim = this.GetComponent<Animator>();

        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        m_info.SetUpMagic(this.gameObject.name);

        tarpos = GameManager.Instance.PlayerTr.position - this.transform.position;
    }

    public void Direasd(bool isFlip, float speed)
    {
        rot = isFlip;
        moveSpeed = speed;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Magic-Enemy-Air-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Magic-Enemy-Air-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
        {
            if (rot)
            {
                rb.velocity = new(tarpos.normalized.x * moveSpeed, 0);
            }

            if (!rot)
            {
                rb.velocity = new(tarpos.normalized.x * moveSpeed, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CommonCollection.AndLayerToLayerMask(collision.gameObject.layer, CommonCollection.L_Player))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
