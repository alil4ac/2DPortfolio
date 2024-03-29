using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Thunder : MonoBehaviour
{
    private Animator anim;

    private Magic_Info m_info;

    private BoxCollider2D col;

    private void Awake()
    {
        m_info = this.GetComponent<Magic_Info>();

        anim = this.GetComponent<Animator>();

        m_info.SetUpMagic(this.gameObject.name);

        col = this.GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        m_info.SetUpMagic(this.gameObject.name);
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("magic-thunder-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
