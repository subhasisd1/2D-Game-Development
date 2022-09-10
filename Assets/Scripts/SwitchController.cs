using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private SpriteRenderer  spriteRenderer;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string strCOl = collision.gameObject.name;

        if(strCOl == "Player")
            spriteRenderer.sprite = sprite;
    }
}
