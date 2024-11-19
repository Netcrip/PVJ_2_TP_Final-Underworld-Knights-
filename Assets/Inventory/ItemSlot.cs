using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

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

    //

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //Verificar si ya hay item
    //    if (isFull) 
          //  return quantity;
        
        //UPDATE NOMBRE
        this.itemName = itemName;

        //UPDATE Imagen
        this.itemSprite = itemSprite;
        //itemImage.sprite = itemSprite; esto es viejo?

        //UPDATE Descrip
        this.itemDescription = itemDescription;

        //UPDATE Cantidad
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            //return extraItems;

        }
            
        //UPDATE CANTIDAD DEL TEXTO
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        //itemImage.sprite = itemSprite;
         
        //return 0;
        
    }

    public void UpdateQuantityText()
    {
        if (quantity > 1) // Mostrar solo si hay más de una unidad
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
        //throw new System.NotImplementedException();
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
        //quantity -= 1;
        _playerHealth.Heal(10);
    }
}
