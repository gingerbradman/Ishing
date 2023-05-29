using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private PlayerController playerController;
    private FishingMiniGame fishingMiniGame;
    private PlayerInventory playerInventory;

    private void Start() 
    {
        playerController = GetComponent<PlayerController>();
        fishingMiniGame = playerController.fishingMiniGamePrefab.GetComponent<FishingMiniGame>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void BuyItem(string x)
    {
        switch(x)
        {
            case "Bait":
                BoughtBait();
                break;
            case "Barbs":
                BoughtBarbs();
                break;
            case "Barter Tutor":
                BoughtBarter();
                break;
            case "Better Reel":
                BoughtReel();
                break;
            case "Heavy Hook":
                BoughtHook();
                break;
            case "Private Island":
                BoughtIsland();
                break;
            case "More Space":
                BoughtSpace();
                break;
            case "New Engine":
                BoughtEngine();
                break;
            case "Weights":
                BoughtWeights();
                break;
        }
    }

    void BoughtBait()
    {
        playerController.biteWaitMax = 5f;
    }

    void BoughtBarbs()
    {
        fishingMiniGame.catchFallSpeed = -.1f;
    }

    void BoughtBarter()
    {
        playerInventory.fishSellMultiplier = 1.5;
    }

    void BoughtReel()
    {
        fishingMiniGame.hookRaiseSpeed = 1;
    }

    void BoughtHook()
    {
        fishingMiniGame.hookFallSpeed = -1;
    }

    void BoughtIsland()
    {
        Debug.Log("Congrats! You won the game!");
    }

    void BoughtSpace()
    {
        playerInventory.upgradeStore();
    }

    void BoughtEngine()
    {
        playerController.speed = 8;
    }

    void BoughtWeights()
    {
        fishingMiniGame.catchRaiseSpeed = .5f;
    }

}
