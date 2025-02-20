using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    #region EnemyInOverworld
    public float movementSpeed;
    public float stoppingDistance;
    public Vector2 startingLocation;
    [SerializeField] public Vector2 targetLocation;
    public float visualRange;
    GameObject player;
    #endregion
    
    #region EnemyInBattle
    public int health;
    public int damage;
    public float battleSpeed;
    public Vector2 movementDirection;
    public float directionChangeTime;
    public float latestDirectionChangeTime;
    public float attackSpeed;
    public EnemyState enemyState;
    public enum EnemyState
    {
        Moving,
        Idle,
        Attack,
        Other
    }
    public Sprite enemySprite;
    public GameObject enemyWeapon;

    #endregion


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
            transform.position = Vector2.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
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
            transform.position = Vector2.MoveTowards(transform.position, startingLocation, movementSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<PlayerController>().BattleAlert(this);
        }
    }
}
