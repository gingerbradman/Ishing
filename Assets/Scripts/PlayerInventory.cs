using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField]
    private int gold;
    public Fish emptySpace;
    public Item emptyItem;
    public int inventorySpaces = 3;
    public int inventoryCounter = 0;
    public void SetInventorySpaces(int x){inventorySpaces = x;}
    public int GetInventorySpaces(){return inventorySpaces;}

    public List<GameObject> inventory;
    public List<GameObject> inventoryUI;
    
    public List<GameObject> inventoryUpgradeUI;
    public List<GameObject> GetInventory(){return inventory;}

    public List<GameObject> storeItems;
    public GameObject store;
    public StoreIsland storeIsland;

    public GameObject goldUI;
    public ItemManager itemManager;
    public double fishSellMultiplier;

    public AudioSource buySound;
    public AudioSource sellSound;

    private void Start() 
    {
        itemManager = GetComponent<ItemManager>();
        GoldChange();
        fishSellMultiplier = 1;
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
        gold += (int)(inventory[x].GetComponent<Fish>().sellValue * fishSellMultiplier);

        GoldChange();
        sellSound.Play();

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
            buySound.Play();

            itemManager.BuyItem(storeItems[x].GetComponent<Item>().name);

            storeIsland.storeItems[x] = emptyItem.gameObject;
            EmptyStoreSpace(x);
        }
    }

    public void AllowSelling()
    {
        for (int i = 0; i < inventorySpaces; i++)
        {
            inventoryUI[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void RemoveSelling(StoreIsland island)
    {
        if(storeIsland == island)
        {
            for (int i = 0; i < inventorySpaces; i++)
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

            storeUI.GetComponentInChildren<TextMeshProUGUI>().text = item.name + " " + item.buyValue;
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
        for (int i = 0; i < inventorySpaces; i++)
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
    
    public void upgradeStore()
    {

        inventoryUpgradeUI[0].gameObject.transform.parent.gameObject.SetActive(true);

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryUpgradeUI[i].GetComponentInChildren<TextMeshProUGUI>().text = inventory[i].GetComponent<Fish>().name + " " + inventory[i].GetComponent<Fish>().stars;
            inventoryUpgradeUI[i].transform.GetChild(1).GetComponent<Image>().sprite = inventory[i].GetComponent<Fish>().fimage;
        }

        inventorySpaces = 5;

        inventoryUI[0].gameObject.transform.parent.gameObject.SetActive(false);
        inventoryUI = inventoryUpgradeUI;
    }


}
