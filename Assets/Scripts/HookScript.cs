using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{

    public FishingMiniGame fishingMiniGame;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        fishingMiniGame.SetCatching(true);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        fishingMiniGame.SetCatching(false);
    }
}
