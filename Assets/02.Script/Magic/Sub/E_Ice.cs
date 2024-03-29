using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Ice : MonoBehaviour
{
    private Animator anim;

    private Magic_Info m_info;

    private void Awake()
    {
        m_info = this.GetComponent<Magic_Info>();

        anim = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_info.SetUpMagic(this.gameObject.name);
    }
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("magic-ice-Animation") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerInfo info = collision.gameObject.GetComponent<PlayerController>().P_Info;

            info.GetDmg(m_info);
        }
    }
}
