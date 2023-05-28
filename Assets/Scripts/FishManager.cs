using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> fishList;
    private List<GameObject> oceanFishList;
    public List<GameObject> GetFishList(){return fishList;}
    public void SetFishList(List<GameObject> x){fishList = x;}
    public void ResetFishList(){fishList = oceanFishList;}

    private void Start() 
    {
        oceanFishList = GameObject.Find("OceanFish").GetComponent<OceanFish>().fishList;
        ResetFishList();
    }



}
