using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    private BoxCollider2D col;

    [SerializeField]
    private GameObject boss;

    private void Awake()
    {
        col = this.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
           collision.gameObject.layer == LayerMask.NameToLayer("Ignore"))
        {
            UIManager.Instance.SetBossHP(boss.gameObject.name);
            col.enabled = false;
        }
    }
}
