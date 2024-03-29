using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : SingleTone<GameManager>
{
    public Transform PlayerTr;

    public bool BossPause = false;

    public bool IsPause = false;

    private bool GoTitle = false;

    private void Awake()
    {
        if(GameManager.Instance != null && GameManager.Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            GoTitle = false;
        }
    }

    private void Update()
    {
        if (CommonCollection.ESCBtn && !IsPause)
        {
            UIManager.Instance.SetPause(true);
            Time.timeScale = 0f;
        }
        if(CommonCollection.ESCBtn && IsPause)
        {
            UIManager.Instance.SetPause(false);
            Time.timeScale = 1f;
        }
    }

    public void GoTitleSet()
    {
        if (!GoTitle)
        {
            GoTitle = true;
            StartCoroutine(LoadTitle());
        }
    }

    private IEnumerator LoadTitle()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(0);
        async.allowSceneActivation = true;
        yield return async;

        if (async.isDone)
        {
            DestroyImmediate(SoundManager.Instance.gameObject);
            DestroyImmediate(PoolManager.Instance.gameObject);
            DestroyImmediate(PortalManager.Instance.gameObject);
            DestroyImmediate(PlayerTr.gameObject.gameObject);
            DestroyImmediate(CameraManager.Instance.MainCam.gameObject);
            DestroyImmediate(CameraManager.Instance.gameObject);
            DestroyImmediate(UIManager.Instance.gameObject);
            DestroyImmediate(this.gameObject);
        }
    }
}
