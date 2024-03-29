using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [SerializeField]
    private Image EnterImg;

    [SerializeField]
    private Image FadeImg;

    [SerializeField]
    private Image LogoImg;

    private RectTransform LogoRect;

    private RectTransform EnterRect;

    private bool PassStart = false;

    private bool CanInput = false;

    private float timer = 1f;

    private void Awake()
    {
        LogoRect = LogoImg.GetComponent<RectTransform>();

        EnterRect = EnterImg.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && CanInput)
        {
            PassStart = true;
        }

    }

    private void OnEnable()
    {
        SetTitle();

        StartCoroutine(BackFadeOut());

    }

    private void SetTitle()
    {
        CanInput = false;

        timer = 1f;
    }

    private IEnumerator BackFadeOut()
    {
        float fadeTime = 1f;
        while(fadeTime >= 0)
        {
            FadeImg.color = new(0, 0, 0, fadeTime);

            fadeTime -= 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        if(fadeTime < 0f)
        {
            StartCoroutine(LogoFadeIn());
            yield break;
        }
    }
    private IEnumerator LogoFadeIn()
    {
        float fadeTime = 0f;
        while (fadeTime <= 1)
        {
            LogoImg.color = new(1, 1, 1, fadeTime);

            fadeTime += 0.2f;

            LogoRect.anchoredPosition += new Vector2(0f, -5f);

            yield return new WaitForSecondsRealtime(0.2f);
        }
        if (fadeTime > 1f)
        {
            StartCoroutine(PressEnterFadeIn());
            yield break;
        }
    }

    private IEnumerator PressEnterFadeIn()
    {
        float fadeTime = 0f;
        while (fadeTime <= 1)
        {
            EnterImg.color = new(1, 1, 1, fadeTime);

            fadeTime += 0.2f;

            RectTransform rect = EnterImg.GetComponent<RectTransform>();

            EnterRect.anchoredPosition += new Vector2(0f, 2f);

            yield return new WaitForSecondsRealtime(0.2f);
        }
        if (fadeTime > 1f)
        {
            StartCoroutine(EnterImgAnimation());
            yield break;
        }
    }

    private IEnumerator EnterImgAnimation()
    {
        CanInput = true;
        float progerss = 0f;
        while (!PassStart)
        {
            progerss += Time.deltaTime * 1f;

            EnterImg.color = Color.Lerp(Color.white, Color.green, Mathf.PingPong(progerss, 1f));

            yield return new WaitForEndOfFrame();
        }

        if (PassStart)
        {
            while(timer >= 0)
            {
                timer -= 0.2f;

                EnterImg.color = Color.red;

                yield return new WaitForSeconds(0.1f);

                EnterImg.color = Color.yellow;

                yield return new WaitForSeconds(0.1f);

            }
            if (timer < 0)
            {
                StartCoroutine(BackFadeIn());
                CanInput = false;
                yield break;
            }
        }
    }

    private IEnumerator BackFadeIn()
    {
        float fadeTime = 0f;
        while (fadeTime <= 1f)
        {
            FadeImg.color = new(0, 0, 0, fadeTime);

            fadeTime += 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (fadeTime > 1f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
