using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField]
    private int gold;
    public Fish emptySpace;
    public Item emptyItem;
    private int inventorySpaces = 3;
    private int inventoryCounter = 0;
    public void SetInventorySpaces(int x){inventorySpaces = x;}
    public int GetInventorySpaces(){return inventorySpaces;}

    public List<GameObject> inventory;
    public List<GameObject> inventoryUI;
    public List<GameObject> GetInventory(){return inventory;}

    public List<GameObject> storeItems;
    public GameObject store;
    public StoreIsland storeIsland;

    public GameObject goldUI;

    private void Start() 
    {
        GoldChange();
    }

    public void AddToInventory(GameObject x)
    {
        if(inventoryCounter < inventorySpaces)
        {
            Fish fish = x.GetComponent<Fish>();

            int index = FindFirstEmptySlot();

            Debug.Log(index);

            if(index >= 0)
            {
                inventoryUI[index].GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " " + fish.stars;
                inventoryUI[index].transform.GetChild(1).GetComponent<Image>().sprite = fish.fimage;
                inventory[index] = x;
                inventoryCounter++;
            }
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void SellFishFromInventory(int x)
    {
        gold += inventory[x].GetComponent<Fish>().sellValue;

        GoldChange();

        inventory[x] = emptySpace.gameObject;
        inventoryUI[x].GetComponentInChildren<TextMeshProUGUI>().text = "";
        inventoryUI[x].transform.GetChild(1).GetComponent<Image>().sprite = emptySpace.fimage;
        inventoryCounter--;
    }

    public void BuyItemForInventory(int x)
    {
        int cost = storeItems[x].GetComponent<Item>().buyValue;

        if(gold >= cost)
        {
            gold -= cost;

            GoldChange();

            storeIsland.storeItems[x] = emptyItem.gameObject;
            EmptyStoreSpace(x);

            //Do Item Logic
        }
    }

    public void AllowSelling()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryUI[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void RemoveSelling(StoreIsland island)
    {
        if(storeIsland == island)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventoryUI[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    public void AllowBuying(List<GameObject> options, StoreIsland storeIslandInstance)
    {

        RemoveBuying(storeIsland);

        storeIsland = storeIslandInstance;

        store.gameObject.SetActive(true);

        for (int i = 0; i < options.Count; i++)
        {
            storeItems[i] = options[i].gameObject;

            GameObject storeUI = store.transform.GetChild(i).gameObject;

            Item item = storeItems[i].GetComponent<Item>();

            storeUI.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            storeUI.transform.GetChild(1).GetComponent<Image>().sprite = item.image;

            if(item.name != "")
            {
                storeUI.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    public void RemoveBuying(StoreIsland island)
    {
        if(storeIsland == island)
        {
            for (int i = 0; i < storeItems.Count; i++)
            {
                EmptyStoreSpace(i);
            }

            store.gameObject.SetActive(false);
        }
    }

    public int FindFirstEmptySlot()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].GetComponent<Fish>().name == "")
            {
                return i;
            }
        }

        return -1;
    }


    public void EmptyStoreSpace(int x)
    {            
        storeItems[x] = emptyItem.gameObject;
        GameObject storeUI = store.transform.GetChild(x).gameObject;
        storeUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
        storeUI.transform.GetChild(1).GetComponent<Image>().sprite = emptySpace.fimage;
        storeUI.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void GoldChange()
    {
        goldUI.GetComponentInChildren<TextMeshProUGUI>().text = "" + gold;
    }
    



}
