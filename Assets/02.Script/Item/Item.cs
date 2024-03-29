using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo I_Info;

    private SpriteRenderer render;

    private float despawnTime = 5f;
    private void Awake()
    {
        I_Info = new ItemInfo();

        I_Info.SetItem(this.gameObject.name);

        render = this.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        despawnTime = 5f;
    }

    private void Update()
    {
        if (I_Info.IsDespawn) despawnTime -= Time.deltaTime;
        if (despawnTime <= 0f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Information other = collision.GetComponent<PlayerController>().P_Info;

            other.GetItem(I_Info);

            PoolManager.Instance.Despawn(this.gameObject);
        }
    }
}
