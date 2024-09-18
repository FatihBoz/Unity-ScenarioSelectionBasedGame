using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private Image itemImage;


    public Image ItemImage { get => itemImage; set => itemImage = value; }
    public TextMeshProUGUI Count { get => count; set => count = value; }

}
