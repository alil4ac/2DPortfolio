using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private SpriteRenderer render;

    private Animator anim;

    private Magic_Info m_info;

    private void Awake()
    {
        m_info = this.GetComponent<Magic_Info>();

        render = this.GetComponent<SpriteRenderer>();

        anim = this.GetComponent<Animator>();

        m_info.SetUpMagic(this.gameObject.name);
    }

    private void OnEnable()
    {
        m_info.SetUpMagic(this.gameObject.name);
    }

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("magic-thunder-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }

    public void Direasd(bool isFlip)
    {
        if (isFlip)
        {
            render.flipX = true;
        }
        if (!isFlip)
        {
            render.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
