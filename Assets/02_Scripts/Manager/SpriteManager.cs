using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(SpriteRenderer p_spriteRenderer, Sprite p_sprite)
    {
        if(p_spriteRenderer.sprite == p_sprite)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator SpriteChangeCoroutine(Transform p_target, string p_SpriteName)
    {
        SpriteRenderer t_spriteRenederer = p_target.GetComponentInChildren<SpriteRenderer>();
        Sprite t_sprite = Resources.Load("Character/" + p_SpriteName, typeof(Sprite)) as Sprite;

        if(!CheckSameSprite(t_spriteRenederer, t_sprite))
        {
            Color t_color = t_spriteRenederer.color;
            t_color.a = 0;
            t_spriteRenederer.color = t_color;

            t_spriteRenederer.sprite = t_sprite;

            while(t_color.a < 1)
            {
                t_color.a += fadeSpeed;
                t_spriteRenederer.color = t_color;
                yield return null;
            }
        }
    }
}
