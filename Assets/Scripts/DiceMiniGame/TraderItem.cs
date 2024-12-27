using TMPro;
using UnityEngine;

public class TraderItem : MonoBehaviour
{

    [Header("*** TEXT ***")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI coinAmountNeedToPurchase;

    [Header("OTHER")]
    [SerializeField] private ItemSlot itemSlot;
    
}
