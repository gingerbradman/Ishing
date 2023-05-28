using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{

    private PlayerInventory playerInventory;

    private void Start() 
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void BuyAssignedItem(int x)
    {
        playerInventory.BuyItemForInventory(x);
    }

}
