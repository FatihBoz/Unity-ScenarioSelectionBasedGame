using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScenarioCard))]
public class ScenarioCardAnimationController : MonoBehaviour
{
    public static bool canFlip;
    public bool isFlipped;

    [SerializeField] private GameObject backContent;
    [SerializeField] private GameObject frontContent;
    [SerializeField] private Sprite emptyCardBack;

    private Animator animator;
    private Image img;
    private readonly float delayAmount = .25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        img = GetComponent<Image>();

        canFlip = true;
        isFlipped = false;
    }

    public void OnScenarioCardClicked()
    {
        if (canFlip)
        {
            animator.SetTrigger("CardFlip");
            StartCoroutine(ShowTextAfterFlip(delayAmount));
            canFlip = false;
        }
    }

    private IEnumerator ShowTextAfterFlip(float delay)
    {
        yield return new WaitForSeconds(delay);

        frontContent.SetActive(false);
        img.sprite = emptyCardBack;
        backContent.SetActive(true);

        yield return new WaitForSeconds(delay);
        isFlipped = true;

        EventManager<ScenarioCard>.TriggerEvent(EventKey.ScenarioCard_Flipped, GetComponent<ScenarioCard>());
    }
}