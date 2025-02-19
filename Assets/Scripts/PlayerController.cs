using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public FishManager fishManager;
    public GameObject bobberPrefab;
    public GameObject bobberInstance;
    float bobberCastPower = 5f;
    public GameObject fishingMiniGamePrefab;
    public GameObject battleMiniGamePrefab;
    public float speed = 5f;
    public Rigidbody2D rb;
    public bool isFishing = false;
    public bool waitingForFish = false;
    public float biteWaitMax;

    private float biteTime = 3f;

    public GameObject exclamationMark;
    private float battleAlertTime = 1f;
    private bool haltPlayerControls = false;

    Vector2 movement;
    Quaternion targetRotation;

    public AudioSource bobberSound;
    public AudioSource caughtSound;
    public AudioSource biteSound;
    public AudioSource lostSound;

    // Start is called before the first frame update
    void Start()
    {
        fishManager = GetComponent<FishManager>();
        biteWaitMax = 10f;
        exclamationMark = GameObject.Find("BattleExclamationMark");
        exclamationMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(haltPlayerControls)
        {
            return;
        }

        if(Input.GetKeyDown("space") && !isFishing)
        {
            isFishing = true;
            bobberInstance = Instantiate(bobberPrefab, gameObject.transform.position, gameObject.transform.rotation);
            bobberInstance.GetComponent<Rigidbody2D>().velocity = transform.up * bobberCastPower;
            StartCoroutine(PlaceBobberAtRest());
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;    
        
        if (movement == Vector2.zero)
        {
        return;
        }    

        if(isFishing)
        {
            StopFishing();
        }

        targetRotation = Quaternion.LookRotation(Vector3.forward, movement);    
        targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);
    }

    IEnumerator PlaceBobberAtRest()
    {
        yield return new WaitForSeconds(.3f);

        if(isFishing)
        {
            bobberSound.Play();
            bobberInstance.GetComponent<FloatingAnimation>().AtRest();
            waitingForFish = true;
            StartCoroutine(WaitingForBite());
        }
    }

    IEnumerator WaitingForBite()
    {
        float randomWaitTime = Random.Range(3f, biteWaitMax);

        yield return new WaitForSeconds(randomWaitTime);

        if(isFishing)
        {
            StartCoroutine(Bite());
        }
    }

    IEnumerator Bite()
    {
        bobberInstance.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        StartCoroutine(TimeForBite());
        biteSound.Play();

        yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));
    }
    
    IEnumerator TimeForBite()
    {
        yield return new WaitForSeconds(biteTime);

        StopFishing();
        lostSound.Play();
        StopAllCoroutines();
        Debug.Log("Fish Missed!");
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;

        if(isFishing)
        {
            waitingForFish = false;
            fishingMiniGamePrefab.SetActive(true);     
            bobberInstance.gameObject.transform.GetChild(0).gameObject.SetActive(false);     
            StopAllCoroutines();
        }
    }

    public void CaughtFish(GameObject x)
    {
        StopFishing();
        caughtSound.Play();
        playerInventory.AddToInventory(x);
    }

    public void LostFish()
    {
        StopFishing();
        lostSound.Play();
        Debug.Log("Fish got away!");
    }

    public void StopFishing()
    {
        isFishing = false;
        Destroy(bobberInstance);
        fishingMiniGamePrefab.SetActive(false);
        StopAllCoroutines();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BattleAlert()
    {
        movement = Vector2.zero;
        haltPlayerControls = true;
        exclamationMark.SetActive(true);
        exclamationMark.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        StartCoroutine(BattleAlertCO());
    }

    IEnumerator BattleAlertCO()
    {
        yield return new WaitForSeconds(battleAlertTime);

        StopFishing(); 
        lostSound.Play();
        StopAllCoroutines();
        StartBattle();
        Debug.Log("Battle Starts!");
    }

    public void StartBattle()
    {
        exclamationMark.SetActive(false);
        battleMiniGamePrefab.SetActive(true); 
    }
}
