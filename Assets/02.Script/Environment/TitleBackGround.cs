using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGround : MonoBehaviour
{
    Renderer render;

    Vector2 offset = new Vector2(0, 0f);

    [SerializeField]
    private float Speed = 0.05f;

    private bool scrolling = false;

    private float x = 0f;

    private void Start()
    {
        render = this.GetComponent<Renderer>();

        render.material.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
    }

    private void Update()
    {

        Scrolling();
    }

    private void Scrolling()
    {
        if (!scrolling)
        {
            x += Speed * Time.deltaTime;

            offset = new Vector2(x, 0f);

            render.material.SetTextureOffset("_MainTex", offset);
        }
        else if (scrolling)
        {
            x -= (Speed * Time.deltaTime);

            offset = new Vector2(x, 0f);

            render.material.SetTextureOffset("_MainTex", offset);
        }

        if (offset.x >= 0.3f)
        {
            scrolling = true;
        }
        else if (offset.x <= 0f)
        {
            scrolling = false;
        }
    }
}
