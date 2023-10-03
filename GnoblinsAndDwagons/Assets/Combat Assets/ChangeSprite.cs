using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    void changeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }
}
