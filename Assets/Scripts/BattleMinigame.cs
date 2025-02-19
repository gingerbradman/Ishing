using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMinigame : MonoBehaviour
{
    public PlayerController playerController;
    public Slider enemySlider;
    public float enemySpeed;
    public Slider playerSlider;
    public float playerSpeed;

    private void OnEnable() 
    {
        ResetFishGame();
    }

    private void ResetFishGame()
    {
        SetupEnemySlider(enemySlider);
        SetupPlayerSlider(playerSlider);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            FillSlider(playerSlider, playerSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            FillSlider(playerSlider, playerSpeed * -1);
        }
    }

    private void FixedUpdate() 
    {
        //FishMovement(fish.GetMovement());
    }

    public void FishMovement(float x)
    {
        FillSlider(enemySlider, x);
    }

    void FillSlider(Slider slider , float value)
    {
        float fillTime = Mathf.Abs(value) * Time.deltaTime;
        if(value > 0)
        {
            slider.value += Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);
        }
        else
        {
            slider.value -= Mathf.Lerp(slider.minValue, slider.maxValue, fillTime);
        }
    }

    void SetupEnemySlider(Slider slider)
    {
        slider.value = slider.maxValue / 2;
    }

    void SetupPlayerSlider(Slider slider)
    {
        slider.value = slider.maxValue / 2;
    }
}
