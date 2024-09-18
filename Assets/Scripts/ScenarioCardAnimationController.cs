using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioCardAnimationController : MonoBehaviour
{
    public static bool canFlip;
    public bool isFlipped;

    [SerializeField] private GameObject backContent;
    [SerializeField] private GameObject frontContent;
    [SerializeField] private Sprite emptyCardBack;

    private Animator animator;
    private Image img;

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
            StartCoroutine(ShowTextAfterFlip(0.2f));
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
    }
}