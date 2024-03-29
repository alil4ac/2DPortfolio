using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUp : MonoBehaviour
{
    private float progerss;
    private float survetime;
    private float fadeTime;

    [SerializeField]
    private Text LevelUpText;

    private RectTransform Trans;
    private void Awake()
    {
        Trans = this.GetComponent<RectTransform>();
        LevelUpText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.PlayerTr.localScale.x > 0)
        {
            Trans.localScale = new(0.01f, 0.01f, 0.01f);
        }
        else if(GameManager.Instance.PlayerTr.localScale.x < 0)
        {
            Trans.localScale = new(-0.01f, 0.01f, 0.01f);
        }
    }

    private void SetOnTime()
    {
        progerss = 0f;
        survetime = 2f;
        fadeTime = 1f;
    }

    public void GetAnimation()
    {
        SetOnTime();
        LevelUpText.gameObject.SetActive(true);
        StartCoroutine(SetAnimation());
    }

    private IEnumerator SetAnimation()
    {
        while (survetime > 0f)
        {
            progerss += Time.deltaTime * 1f;

            LevelUpText.color = Color.Lerp(Color.yellow, Color.green, Mathf.PingPong(progerss, 1f));

            yield return new WaitForEndOfFrame();

            LevelUpText.color = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(progerss, 1f));

            yield return new WaitForEndOfFrame();

            survetime -= Time.deltaTime;
        }

        if (survetime <= 0f)
        {
            LevelUpText.color = Color.yellow;

            StartCoroutine(FadeOut());

            yield break;
        }
    }

    private IEnumerator FadeOut()
    {
        fadeTime = 1f;

        while (fadeTime >= 0)
        {
            LevelUpText.color = new Color(1, 0.92f, 0.016f, fadeTime);

            fadeTime -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        if (fadeTime <= 0)
        {
            LevelUpText.gameObject.SetActive(false);

            yield break;
        }
    }
}
