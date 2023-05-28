using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public string name;
    public Sprite fimage;
    public string stars;
    public int sellValue;
    private float weight;
    public float swimUpSpeed;
    public float swimDownSpeed;

    public float sliderValue;

    public float GetMovement()
    {
        int determination = (int)DetermineGreatestWeight();

        switch (determination){
            case 0:
                return 0;
            case 1:
                return swimUpSpeed;
            case 2:
                return swimDownSpeed;
            default:
                return 0;
        }

    }

    public float DetermineGreatestWeight()
    {
        weight = Random.Range(-10.0f, 10.0f);
        return weight;
    }

}
