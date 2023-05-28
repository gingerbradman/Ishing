using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMiniGame : MonoBehaviour
{
    public PlayerController playerController;
    public Fish fish;
    public Slider catchSlider;
    public float catchRaiseSpeed;
    public float catchFallSpeed;
    public Slider fishSlider;
    public Slider hookSlider;

    public float hookRaiseSpeed;
    public float hookFallSpeed;

    [SerializeField]
    private float fillTime = 0f;

    [SerializeField]
    private bool catching = false;
    public void SetCatching(bool x){catching = x;}
    public bool GetCatching(){return catching;}

    private void OnEnable() 
    {
        ResetFishGame();
    }

    private void ResetFishGame()
    {
        fish = GetFish().GetComponent<Fish>();
        SetupFishSlider(fishSlider);
        SetupCatchSlider(catchSlider);
        SetupHookSlider(hookSlider);
    }

    private GameObject GetFish()
    {
        List<GameObject> list = playerController.fishManager.GetFishList();

        int index = Random.Range(0, list.Count);

        return list[index];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space"))
        {
            FillSlider(hookSlider, hookRaiseSpeed);
        }
        else
        {
            FillSlider(hookSlider, hookFallSpeed);
        }
    }

    private void FixedUpdate() 
    {
        FishMovement(fish.GetMovement());
        Hooking();
    }

    public void Hooking()
    {
        if(catching)
        {
            FillSlider(catchSlider, catchRaiseSpeed);
            if(catchSlider.value == catchSlider.maxValue)
            {
                playerController.CaughtFish(fish.gameObject);
            }
        } 
        else 
        {
            FillSlider(catchSlider, catchFallSpeed);
            if(catchSlider.value == catchSlider.minValue)
            {
                playerController.LostFish();
            }
        }
    }

    public void FishMovement(float x)
    {
        FillSlider(fishSlider, x);
    }

    void FillSlider(Slider slider , float value)
    {
        fillTime = Mathf.Abs(value) * Time.deltaTime;
        if(value > 0)
        {
            slider.value += Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);
        }
        else
        {
            slider.value -= Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);
        }
    }

    void SetupFishSlider(Slider slider)
    {
        slider.value = fish.sliderValue;
    }

    void SetupHookSlider(Slider slider)
    {
        slider.value = 0f;
    }

    void SetupCatchSlider(Slider slider)
    {
        slider.value = 500f;
    }
}
