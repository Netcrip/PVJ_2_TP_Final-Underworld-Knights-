using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1;
        }
        CheckAndUsePotion();

    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, int healAmount, string uniqueID)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].IsMatch(itemName, itemDescription, healAmount, uniqueID))
            {
                itemSlot[i].AddQuantity(quantity);
                Debug.Log("Cantidad actualizada: " + itemSlot[i].quantity + " de " + itemName);
                return;
            }
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, healAmount, uniqueID);
                return;
            }
        }
    }

    public void UseSelectedItem(ItemSlot slot)
    {
        if (slot != null && slot.thisItemSelected && slot.quantity > 0)
        {
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>(); // ??
            _playerHealth.Heal(slot.healAmount);
            slot.RemoveOneItem();
        }
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in itemSlot)
        {
            slot.Deselect();
        }
    }
    private void CheckAndUsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsePotionByID("small_pot_10");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UsePotionByID("mid_pot_20");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UsePotionByID("big_pot_30");
        }
    }

    private void UsePotionByID(string potionID)
    {
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.uniqueID == potionID && slot.quantity > 0)
            {
                _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                if (_playerHealth != null)
                {
                    _playerHealth.Heal(slot.healAmount);
                }

                slot.RemoveOneItem();
                return;
            }
        }
    }
}
