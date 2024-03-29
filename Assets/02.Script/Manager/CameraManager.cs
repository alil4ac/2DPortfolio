using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : SingleTone<CameraManager>
{
    public Camera MainCam;

    [SerializeField]
    private float moveSpeed = 5f;

    private Vector3 targetPos;

    private Vector3 camPos;

    private Vector3 ScreenPoint;

    private Vector3 ScreenPoint2;

    float half;

    public float time = 0.2f;

    public float pow = 0.1f;

    [SerializeField]
    private Transform LeftPoint;

    [SerializeField]
    private Transform RightPoint;
    private void Init()
    {
        SetMainCam();
        DontDestroyOnLoad(this);
    }

    public void SetMainCam()
    {
        MainCam = Camera.main;
        DontDestroyOnLoad(MainCam);
    }

    private void Awake()
    {
        if (CameraManager.Instance != null && CameraManager.Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Init();
        }
    }

    private void Start()
    {
        camPos = new Vector3(0, 0, -10);
    }
    private void FixedUpdate()
    {
        MainCamMove();
        ScreenPoint = MainCam.ViewportToWorldPoint(Vector3.zero);
        ScreenPoint2 = MainCam.ViewportToWorldPoint(Vector3.one);

        half = (ScreenPoint.x - ScreenPoint2.x)/2;
    }
    public void CameraShake(Ease ease, float duration = 0.05f, float magnitudePos = 0.03f, int vibrato = 10)
    {
        Vector3 OriginPos = MainCam.transform.position;

        Vector3 shakePos = Random.insideUnitSphere;

        MainCam.DOShakePosition(duration, shakePos, vibrato, magnitudePos, false).SetEase(ease);

        MainCam.transform.position = OriginPos;
    }

    public void SetCameraShake()
    {
        StartCoroutine(CamShake(time, pow));
    }

    private IEnumerator CamShake(float time = 0.2f, float pow = 0.1f)
    {
        while(time >= 0)
        {
            MainCam.orthographicSize -= pow;

            yield return new WaitForEndOfFrame();

            MainCam.orthographicSize += pow;

            yield return new WaitForEndOfFrame();

            time -= Time.deltaTime;
        }
        if(time < 0)
        {
            MainCam.orthographicSize = 1.5f;

            yield break;
        }
    }


    private void MainCamMove()
    {
        targetPos = GameManager.Instance.PlayerTr.position;

        MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, targetPos + camPos, Time.deltaTime * moveSpeed);

        float lx = Mathf.Clamp(MainCam.transform.position.x, LeftPoint.position.x - half, RightPoint.position.x + half);

        float ly = Mathf.Clamp(MainCam.transform.position.y, -10, 10);

        Vector3 dir =  new Vector3(lx, ly, MainCam.transform.position.z);

        MainCam.transform.position = dir;
    }
}
