using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBackGround : MonoBehaviour
{
    [SerializeField]
    private float distanceX;

    [SerializeField]
    private float distanceY;

    [SerializeField]
    private float moveSpeed = 500f;

    public float dis = -10f;
    void Update()
    {

    }

    private void SetDistance()
    {

    }

    private void FixedUpdate()
    {
        Vector2 CamPos = CameraManager.Instance.MainCam.transform.position;

        Vector3 newPos = new Vector3(CamPos.x + distanceX, CamPos.y + distanceY, dis);

        this.transform.position = Vector3.Slerp(this.transform.position, newPos, moveSpeed);

    }
}
