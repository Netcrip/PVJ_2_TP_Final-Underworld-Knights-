using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private int healAmount;
    [SerializeField] private string uniqueID;

    private InventoryManager inventoryManager;

    public string ItemDescription => itemDescription;
    public int HealAmount => healAmount;
    public string UniqueID => uniqueID;

    void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, healAmount, uniqueID);
            Destroy(gameObject);
        }
    }
}