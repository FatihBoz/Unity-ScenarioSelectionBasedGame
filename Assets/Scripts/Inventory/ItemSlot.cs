using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public static bool tooltipIsActive;

    [SerializeField] private TextMeshProUGUI count;

    [SerializeField] private Image itemImage;

    [SerializeField] private GameObject lootItemTooltipPrefab;

    [SerializeField] private GameObject staffTooltipPrefab;

    [SerializeField] private Vector2 offSet;

    [SerializeField] private Button closeButtonPanel;


    private Canvas canvas;
    private Button itemSlot;
    private ItemSO currentItem;
    private RectTransform rectTransform;



    private void Awake()
    {
       itemSlot = GetComponent<Button>();
       rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        itemSlot.onClick.AddListener(MakeTooltipActive);
        tooltipIsActive = false;
    }

    private void MakeTooltipActive()
    {
        if (currentItem == null || canvas == null || tooltipIsActive) 
            return;


        if (currentItem.ItemType == ItemType.Staff)
        {
            OpenTooltip(staffTooltipPrefab);
        }
        else if(currentItem.ItemType == ItemType.LootItem)
        {
            OpenTooltip(lootItemTooltipPrefab);
        }
    }



    void OpenTooltip(GameObject tooltipPrefab)
    {
        Button b = Instantiate(closeButtonPanel, canvas.transform);

        // Instantiate under canvas
        GameObject tooltipInstance = Instantiate(tooltipPrefab, canvas.transform);
        //Get RectTransform component
        RectTransform rt = tooltipInstance.GetComponent<RectTransform>();

        rt.SetParent(rectTransform.parent);
        // set its values with offSet
        rt.anchoredPosition = GetComponent<RectTransform>().anchoredPosition + offSet;

        if (tooltipInstance.TryGetComponent<ItemTooltip>(out var itemTooltip))
        {
            //Set the general ItemSO information
            itemTooltip.SetItem(currentItem);

            itemTooltip.SetCloseTooltipButtonPanel(b); //assign panel
            rt.SetParent(canvas.transform);

        }

        tooltipIsActive = true;
        SetCloseButtonPanel(tooltipInstance,b); //Add listener
    }


    void SetCloseButtonPanel(GameObject tooltip,Button b)
    {

        b.onClick.AddListener(() =>
        {
            Destroy(tooltip);
            tooltipIsActive = false;
            Destroy(b.gameObject);

        });
    }


    public void SetItem(ItemSO item,int itemCount = 1)
    {
        currentItem = item;
        itemImage.sprite = item.ItemSprite;
        count.text = itemCount.ToString();

        if(itemCount > 1)
        {
            count.gameObject.SetActive(true);
        }
    }

    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    public ItemSO CurrentItem => currentItem;
}
