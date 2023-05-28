using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFish : MonoBehaviour
{

    private FishManager fishManager;
    public List<GameObject> fishList;

    private void Start() 
    {
        fishManager = GameObject.Find("Player").GetComponent<FishManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Bobber")
        {
            fishManager.SetFishList(fishList);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Bobber")
        {
            fishManager.ResetFishList();
        }
    }

}
