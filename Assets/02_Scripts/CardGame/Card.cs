using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardRenderer;
    [SerializeField] private Sprite animalSprite;
    [SerializeField] private Sprite backSprite;

    private bool isFlipped;
    private bool isFlipping = false;
    private bool isMatched = false;

    public int cardID;

    public void SetCardID(int id)
    {
        cardID = id;
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public void SetAnaimalSprite(Sprite sprite)
    {
        this.animalSprite = sprite;
    }

    public void FlipCard()
    {
        isFlipping = true;

        Vector3 originalSclae = transform.localScale;
        Vector3 targetScale = new Vector3(0f, originalSclae.y, originalSclae.z);

        transform.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            isFlipped = !isFlipped;

            if (isFlipped)
            {
                cardRenderer.sprite = animalSprite;
            }
            else
            {
                cardRenderer.sprite = backSprite;
            }

            transform.DOScale(originalSclae, 0.2f).OnComplete(()=>{
                isFlipping = false;
            });
        });
    }
    private void OnMouseDown()
    {
        if (!isFlipping && !isMatched && !isFlipped)
        {
            CardGameManager.instance.CardClicked(this);
        }
    }
}
