using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private int sceneIdx;

    [SerializeField]
    private Vector2 SpawnPos;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            PortalManager.Instance.LoadScene(sceneIdx, SpawnPos);
        }
    }
}
