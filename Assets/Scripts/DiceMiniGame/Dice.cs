using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Dice : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    [SerializeField] private List<Sprite> diceSprites;
    [SerializeField] private Canvas miniGameCanvas;

    private int currentDiceCount = 0;
    private bool canDrag;
    private Animator animator;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup; 
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);

        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Cursor.visible = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / miniGameCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Cursor.visible = true;
        canvasGroup.blocksRaycasts = true;

        if (!TrySnapToTarget(eventData))
        {
            ResetPosition();
        }
    }

    private bool TrySnapToTarget(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return false;
        }

        if (eventData.pointerEnter != null)
        {
            if (eventData.pointerEnter.TryGetComponent<IDiceTarget>(out var target) && target.isInTargetArea)
            {
                target.PlaceDiceOnItem(currentDiceCount);
                rectTransform.anchoredPosition = target.visibleTarget.rectTransform.anchoredPosition;
                ResetPosition();
                gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    private void ResetPosition()
    {
        rectTransform.DOAnchorPos(originalPosition, 0.5f);
        //rectTransform.anchoredPosition = originalPosition;
    }

    public void Roll()
    {
        canDrag = false;
        animator.enabled = true;
        animator.SetTrigger("Roll");
    }


    public int ChangeSprite()
    {
        canDrag = true;
        int diceCount = Random.Range(0, diceSprites.Count);
        currentDiceCount = diceCount +1;
        if (gameObject.TryGetComponent<Image>(out var img))
        {
            animator.enabled = false;
            img.sprite = diceSprites[diceCount];
            return diceCount + 1;
        }
        return 0;
    }

    public void SetCanvas(Canvas canvas)
    {
        miniGameCanvas = canvas;
    }

}
