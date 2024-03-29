using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitDamage : MonoBehaviour
{
    private int damage;

    private float timer = 2f;

    private Text text;

    private void Awake()
    {
        text = this.GetComponent<Text>();
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnEnable()
    {
        timer = 2f;
        StartCoroutine(TextAnimation());
    }

    private IEnumerator TextAnimation()
    {
        while(timer >= 0f)
        {
            text.color = Color.red;
            text.fontStyle = FontStyle.Bold;
            yield return new WaitForSeconds(0.1f);

            text.color = Color.white;
            text.fontStyle = FontStyle.Normal;

            yield return new WaitForSeconds(0.01f);

            text.color = Color.yellow;
            text.fontStyle = FontStyle.Bold;

            yield return new WaitForSeconds(0.1f);

            text.color = Color.white;
            text.fontStyle = FontStyle.Normal;

            yield return new WaitForSeconds(0.01f);

            timer -= Time.deltaTime;
        }

        if(timer <= 0f)
        {
            PoolManager.Instance.Despawn(this.gameObject);
        }
    }
}
