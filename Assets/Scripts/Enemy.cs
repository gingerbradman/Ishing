using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameObject player;
    public float speed;
    public float stoppingDistance;
    public Vector2 startingLocation;
    [SerializeField] public Vector2 targetLocation;
    public float visualRange;

    // Start is called before the first frame update
    void Start()
    {
        startingLocation = transform.position;
        player = GameObject.Find("Player");
        targetLocation = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetLocation = player.transform.position;
        CalculateDistance();
    }

    void Chase(Vector2 target)
    {
        if(Vector2.Distance(transform.position, target) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    void CalculateDistance()
    {
        if(Vector2.Distance(transform.position, targetLocation) < visualRange)
        {
            Chase(targetLocation);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startingLocation, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<PlayerController>().BattleAlert();
        }
    }
}
