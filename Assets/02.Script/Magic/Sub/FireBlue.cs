using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlue : MonoBehaviour
{
    private Rigidbody2D rb;

    private CircleCollider2D _collider;

    private Animator anim;

    private Magic_Info m_info;

    private Vector3 movePos;

    private Vector3 dir;

    private float moveSpeed = 10f;

    private float despawntime = 10f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();

        _collider = this.GetComponent<CircleCollider2D>();

        anim = this.GetComponent<Animator>();

        m_info = this.GetComponent<Magic_Info>();

        m_info.SetUpMagic(this.gameObject.name);
    }

    private void Update()
    {
        despawntime -= Time.deltaTime;

        if (despawntime <= 0)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }

    private void OnEnable()
    {
        despawntime = 10f;

        movePos = GameManager.Instance.PlayerTr.position;

        dir = movePos - this.transform.position;

        m_info.SetUpMagic(this.gameObject.name);
    }

    private void FixedUpdate()
    {
        rb.velocity = dir.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CommonCollection.AndLayerToLayerMask(collision.gameObject.layer, CommonCollection.L_Player) ||
           CommonCollection.AndLayerToLayerMask(collision.gameObject.layer, CommonCollection.L_Tile))
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }
}
