using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Image background;
    public Sprite backgroundSprite;
    public Sprite tutorialSprite;

    public GameObject mainmenuUI;
    public GameObject tutorialUI;


    public void StartPressed()
    {
        background.sprite = tutorialSprite;
        mainmenuUI.SetActive(false);
        tutorialUI.SetActive(true);
    }

    public void LoadGivenScene(int x)
    {
        SceneManager.LoadScene(x);
    }


}
