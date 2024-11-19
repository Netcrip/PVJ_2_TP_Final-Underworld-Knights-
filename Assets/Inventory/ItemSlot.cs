using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public int healAmount;
    public string uniqueID;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems = 10;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;
    public Image itemDescriptionImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int healAmount, string uniqueID)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;
        this.healAmount = healAmount;
        this.uniqueID = uniqueID;
        UpdateQuantity(quantity);
        isFull = true;
    }

    public void AddQuantity(int quantity)
    {
        UpdateQuantity(this.quantity + quantity);
    }

    public void RemoveOneItem()
    {
        if (--quantity <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateQuantityText();
        }
    }

    public bool IsMatch(string itemName, string itemDescription, int healAmount, string uniqueID)
    {
        return this.itemName == itemName && this.itemDescription == itemDescription &&
               this.healAmount == healAmount && this.uniqueID == uniqueID;
    }

    public void Deselect()
    {
        selectedShader.SetActive(false);
        thisItemSelected = false;
    }

    private void UpdateQuantity(int quantity)
    {
        this.quantity = Mathf.Clamp(quantity, 0, maxNumberOfItems);
        UpdateQuantityText();
    }

    private void UpdateQuantityText()
    {
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
        quantityText.enabled = quantity > 0;
    }

    private void ClearSlot()
    {
        isFull = false;
        itemName = "";
        itemDescription = "";
        uniqueID = "";
        itemImage.sprite = emptySprite;
        quantity = 0;
        quantityText.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            SetItemDescription();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryManager.UseSelectedItem(this);
        }
    }

    private void SetItemDescription()
    {
        if (ItemDescriptionNameText != null && ItemDescriptionText != null && itemDescriptionImage != null)
        {
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite ?? emptySprite;
        }
    }
}
