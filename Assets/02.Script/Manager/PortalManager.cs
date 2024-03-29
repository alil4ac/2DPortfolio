using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalManager : SingleTone<PortalManager>
{
    private void Awake()
    {
        if(PortalManager.Instance != null && PortalManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void LoadScene(int index, Vector2 spawnPos)
    {
        SceneManager.LoadScene(index);
        GameManager.Instance.PlayerTr.position = spawnPos;
        CameraManager.Instance.MainCam.transform.position = spawnPos;
    }

    public void SceneEvent()
    {

    }
}
