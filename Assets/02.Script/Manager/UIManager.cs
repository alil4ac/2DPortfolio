using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : SingleTone<UIManager>
{

    [SerializeField]
    private Slider HP_Bar;

    [SerializeField]
    private Slider Angel_HP_Bar;

    [SerializeField]
    private Slider Phantom_HP_Bar;

    [SerializeField]
    private Slider GhostWolf_HP_Bar;

    [SerializeField]
    private Text PlayerHPText;

    [SerializeField]
    private Text PlayerLevel;

    [SerializeField]
    private Slider ExpBar;

    [SerializeField]
    private Canvas LevelUpCanvas;

    [SerializeField]
    private Image FadeBlackImage;

    [SerializeField]
    private Image PausePanel;


    [SerializeField]
    private Image GameOverImage;
    private void Awake()
    {
        if(UIManager.Instance != null && UIManager.Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            SetFadeOut();
        }
    }

    public void SetGameOver()
    {
        StartCoroutine(SetGameOverImage());
    }

    private IEnumerator SetGameOverImage()
    {
        float time = 0f;
        while(time <= 1f)
        {
            GameOverImage.color = Color.Lerp(new(1,1,1,0), new(1,1,1,1), time);

            time += 0.1f;

            yield return new WaitForSeconds(0.2f);
        }

        if(time > 1f)
        {
            GameOverImage.color = new(1, 1, 1, 1);
            FadeBlackImage.gameObject.SetActive(true);
            FadeBlackImage.color = Color.black;
            GameManager.Instance.GoTitleSet();
            yield break;
        }
    }

    public void SetPause(bool isPause)
    {
        if (isPause)
        {
            _SetPauseIn();
        }
        else if (!isPause)
        {
            _SetPauseClose();
        }
    }

    private void _SetPauseIn()
    {
        SoundManager.Instance.PasueVolum();
        StartCoroutine(PanelOpen());
    }

    private IEnumerator PanelOpen()
    {
        float fadeTime = 0f;
        while (fadeTime <= 0.3f)
        {
            PausePanel.color = new(PausePanel.color.r, PausePanel.color.g, PausePanel.color.b, fadeTime);

            fadeTime += 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (fadeTime > 0.3f)
        {
            GameManager.Instance.IsPause = true;
            yield break;
        }
    }

    private void _SetPauseClose()
    {
        SoundManager.Instance.NormalVolum();
        StartCoroutine(PanelClose());
    }

    private IEnumerator PanelClose()
    {
        float fadeTime = 0.3f;
        while (fadeTime >= 0f)
        {
            PausePanel.color = new(PausePanel.color.r, PausePanel.color.g, PausePanel.color.b, fadeTime);

            fadeTime -= 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (fadeTime < 0f)
        {
            GameManager.Instance.IsPause = false;
            yield break;
        }
    }

    public void SetFadeOut()
    {
        StartCoroutine(BlackFadeOut());
    }


    private IEnumerator BlackFadeOut()
    {
        float fadeTime = 1f;
        while (fadeTime >= 0)
        {
            FadeBlackImage.color = new(0, 0, 0, fadeTime);

            fadeTime -= 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (fadeTime < 0f)
        {
            FadeBlackImage.gameObject.SetActive(false);
            yield break;
        }
    }

    public void SetFadeIn()
    {
        StartCoroutine(BlackFadeIn());
    }

    private IEnumerator BlackFadeIn()
    {
        float fadeTime = 0f;
        while (fadeTime <= 1f)
        {
            FadeBlackImage.color = new(0, 0, 0, fadeTime);

            fadeTime += 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (fadeTime > 1f)
        {
            yield break;
        }
    }

    public void SetLevelUp(Information info)
    {
        PlayerLevel.text = "LV " + info.P_Level.ToString();
        LevelUpCanvas.gameObject.GetComponent<LevelUp>().GetAnimation();
    }

    private void Update()
    {
        this.gameObject.transform.localScale = Vector3.one;
    }

    public void SetExpBar(float exp)
    {
        ExpBar.value = exp;
    }

    public void GetHpBar(Information info, string name)
    {
        switch (name)
        {
            case "Player":
                HP_Bar.value = info.SetSliderBarValue();
                PlayerHPText.text = info.Health.ToString();
                break;

            case "Angel":
                Angel_HP_Bar.value = info.SetSliderBarValue();
                break;

            case "Phantom":
                Phantom_HP_Bar.value = info.SetSliderBarValue();
                break;

            case "GhostWolf":
                GhostWolf_HP_Bar.value = info.SetSliderBarValue();
                break;
        }
    }

    public void SetBossHP(string name)
    {
        switch (name)
        {
            case "Angel":
                Angel_HP_Bar.gameObject.SetActive(true);
                break;
            case "Phantom":
                Phantom_HP_Bar.gameObject.SetActive(true);
                break;
            case "GhostWolf":
                GhostWolf_HP_Bar.gameObject.SetActive(true);
                break;
        }
    }

    public void EndBossHP(string name)
    {
        switch (name)
        {
            case "Angel":
                StartCoroutine(Angel_HP_Bar.gameObject.GetComponent<BossHP>().FadeOut());
                break;
            case "Phantom":
                StartCoroutine(Phantom_HP_Bar.gameObject.GetComponent<BossHP>().FadeOut());
                break;
            case "GhostWolf":
                StartCoroutine(GhostWolf_HP_Bar.gameObject.GetComponent<BossHP>().FadeOut());
                break;
        }
    }
}
