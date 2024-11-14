using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;

    private bool menuActivated;

    public ItemSlot[] itemSlot;
    
 

    // Update is called once per frame
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

    //public void AddItem(string itemName, int quantity, Sprite itemSprite)
    //{
    //    Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);
    //    for (int i = 0; i < itemSlot.Length; i++)
    //    {
    //        if (itemSlot[i].isFull == false)
    //        {
    //            itemSlot[i].AddItem(itemName, quantity, itemSprite);
    //            return;
    //        }
    //    }
    //}
    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        // Verificar si el item ya está en el inventario
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull && itemSlot[i].itemName == itemName)
            {
                // Si el item ya está, solo aumenta la cantidad
                itemSlot[i].quantity += quantity;
                itemSlot[i].UpdateQuantityText(); // Asegúrate de tener un método para actualizar el texto si es necesario
                Debug.Log("Cantidad actualizada: " + itemSlot[i].quantity + " de " + itemName);
                return;
            }
        }

        // Si no se encuentra el item, agregarlo a un nuevo slot
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                Debug.Log("Item agregado: " + itemName);
                return;
            }
        }

        Debug.Log("No hay espacio en el inventario.");
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
