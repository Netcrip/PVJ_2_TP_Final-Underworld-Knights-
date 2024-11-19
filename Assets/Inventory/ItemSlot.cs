using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;
    private PlayerHealth _playerHealth;

    // ITEM SLOT
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    // ITEM DESCRIPTION SLOT
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public TMP_Text QuantityText
    {
        get { return quantityText; }
    }

    public Image ItemImage
    {
        get { return itemImage; }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;
        this.quantity += quantity;

        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
    }

    public void UpdateQuantityText()
    {
        if (quantity > 1)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
        }
        else
        {
            quantityText.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
            itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        inventoryManager.UseSelectedItem(this);
    }
}
