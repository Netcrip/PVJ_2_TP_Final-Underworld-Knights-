using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
            Time.timeScale = 0;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName)
            {
                itemSlot[i].quantity += quantity;
                itemSlot[i].UpdateQuantityText();
                Debug.Log("Cantidad actualizada: " + itemSlot[i].quantity + " de " + itemName);
                return;
            }
            Debug.Log("For" + itemSlot[i].quantity);
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
            }
        }
    }

    public void UseSelectedItem(ItemSlot slot)
    {
        if (slot != null)
        {
            if (slot.thisItemSelected && slot.quantity > 0)
            {
                if (_playerHealth == null)
                {
                    _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                }
                _playerHealth.Heal(10);

                slot.quantity--;
                slot.UpdateQuantityText();

                if (slot.quantity <= 0)
                {
                    slot.isFull = false;
                    slot.itemName = "";
                    slot.itemDescription = "";
                    if (slot.ItemImage != null)
                    {
                        slot.ItemImage.sprite = slot.emptySprite;
                    }
                    if (slot.QuantityText != null)
                    {
                        slot.QuantityText.enabled = false;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("ItemSlot is null");
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
