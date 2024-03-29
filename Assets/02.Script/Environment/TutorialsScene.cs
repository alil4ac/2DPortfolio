using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") || 
           collision.gameObject.layer == LayerMask.NameToLayer("Ignore"))
        {
            GameManager.Instance.PlayerTr.position = new(-3.708591f, -0.07063f, 0);
            CameraManager.Instance.MainCam.transform.position = new(-3.708593f, -0.07063197f, -10f);
        }
    }
}
