using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsChain : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform settingsGUI;
    [SerializeField] private float maxPullDistance;
    [SerializeField] private float returnDurationToDefault;

    [Header("Animator")]
    [SerializeField] private Animator buttonChainAnim;
    [SerializeField] private Animator longChainAnim;

    #region Position
    private Vector2 initialGUIPosition;
    private Vector2 initialButtonPosition;
    private Vector2 dragStartPosition;
    private RectTransform rectTransform;
    #endregion Position

    #region Animation
    private readonly string animationName = "Swing";
    private bool canBeAnimated = true;
    #endregion Animation


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        initialButtonPosition = rectTransform.anchoredPosition;
        initialGUIPosition = settingsGUI.anchoredPosition;
        //canPull = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StopAllCoroutines();
        if (dragStartPosition == Vector2.zero)
        {
            dragStartPosition = eventData.position;
        }

        canBeAnimated = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (!canPull)
        //{
        //    return;
        //}

        Vector2 dragCurrentPosition = eventData.position;
        float dragDistance = dragStartPosition.y - dragCurrentPosition.y;

        float clampedDragDistance = Mathf.Clamp(dragDistance, 0, maxPullDistance);  

        MoveButton(clampedDragDistance);

        MoveGUI(clampedDragDistance);
    }

    void MoveButton(float clampedDragDistance)
    {
        Vector2 newChainPosition = initialButtonPosition;

        newChainPosition.y = initialButtonPosition.y - clampedDragDistance;

        rectTransform.anchoredPosition = newChainPosition;
    }

    void MoveGUI(float clampedDragDistance)
    {
        if (settingsGUI != null)
        {
            Vector2 temp = new(initialGUIPosition.x,initialGUIPosition.y + clampedDragDistance *2);
            settingsGUI.anchoredPosition = temp;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //if (canPull)
        //{
        //    StartCoroutine(SmoothReturn());
        //}

        StartCoroutine(SmoothReturn());
    }

    private IEnumerator SmoothReturn()
    {
        //canPull = false;
        Vector2 buttonPos = rectTransform.anchoredPosition;
        Vector2 guiPos = settingsGUI.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < returnDurationToDefault)
        {
            elapsedTime += Time.deltaTime;  

            float percentage = elapsedTime / returnDurationToDefault;

            rectTransform.anchoredPosition = Vector2.Lerp(buttonPos, initialButtonPosition, percentage);
            settingsGUI.anchoredPosition = Vector2.Lerp(guiPos, initialGUIPosition, percentage);

            yield return null;
        }

        //canPull = true;
        canBeAnimated = true;
    }

    private void FixedUpdate()
    {
        buttonChainAnim.SetBool(animationName, !canBeAnimated);
        longChainAnim.SetBool(animationName,canBeAnimated);
    }
}