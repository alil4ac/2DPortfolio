using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHP : MonoBehaviour
{
    [SerializeField]
    private Image backImg;

    [SerializeField]
    private Image fillImg;

    private Slider slider;

    private float fadeTime = 0;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();

        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        fadeTime = 0f;

        while(fadeTime <= 1)
        {
            backImg.color = new Color(1, 1, 1, fadeTime);
            fillImg.color = new Color(1, 1, 1, fadeTime);

            fadeTime += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        if(fadeTime >= 1f)
        {
            while(slider.value < slider.maxValue)
            {
                slider.value += 2;

                yield return new WaitForSeconds(0.01f);
            }
        }

        if(slider.value >= slider.maxValue)
        {
            yield break;
        }
    }

    public IEnumerator FadeOut()
    {
        fadeTime = 1f;

        while (fadeTime >= 0)
        {
            backImg.color = new Color(1, 1, 1, fadeTime);
            fillImg.color = new Color(1, 1, 1, fadeTime);

            fadeTime -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        if (fadeTime <= 0)
        {
            this.gameObject.SetActive(false);
            yield break;
        }
    }
}
