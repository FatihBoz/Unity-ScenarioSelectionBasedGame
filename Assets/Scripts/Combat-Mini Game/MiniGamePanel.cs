using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class MiniGamePanel : MonoBehaviour
{
    [Header("GENERAL")]
    [SerializeField] protected GameObject canvas;
    [SerializeField] protected GameObject panel; //starting or ending

    [SerializeField] private Image backgroundImage;

    protected void SetBackgroundImage()
    {
        backgroundImage.sprite = BackgroundManager.Instance.GetBackgroundImage().sprite;
    } 
    


}
