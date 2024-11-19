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
            //desactivar mixxer
        }
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
            Time.timeScale = 0;
            //falta activar mixxer
        }
    }
    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
         //Verificar si el item ya está en el inventario
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName)
            {
                

               // Si el item ya está, solo aumenta la cantidad
                itemSlot[i].quantity = itemSlot[i].quantity + 1;
                itemSlot[i].UpdateQuantityText(); // Asegúrate de tener un método para actualizar el texto si es necesario
                Debug.Log("Cantidad actualizada: " + itemSlot[i].quantity + " de " + itemName);
                return;
            } 
            Debug.Log("For" + itemSlot[i].quantity);
        }
        

        // Si no se encuentra el item, agregarlo a un nuevo slot
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].name != name)
            {
                itemSlot[i].AddItem(itemName, 1 , itemSprite, itemDescription);
                return;               
            }
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
