using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreIsland : MonoBehaviour
{

    private PlayerInventory playerInventory;

    public List<GameObject> storeItems;

    private void Start() 
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            playerInventory.AllowSelling();
            playerInventory.AllowBuying(storeItems, this);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            playerInventory.RemoveSelling(this);
            playerInventory.RemoveBuying(this);
        }
    }
}
