using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellFish : MonoBehaviour
{

    private PlayerInventory playerInventory;

    private void Start() 
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void SellAssignedFish(int x)
    {
        playerInventory.SellFishFromInventory(x);
    }

}
