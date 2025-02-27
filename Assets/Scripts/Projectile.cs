using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public BattleMinigame battleMinigame;
    public float speed;
    public float lifeTime = 2f;
    public bool isEnemyProjectile;
    public int damage = 1;
    public enum ProjectileType
    {
        Player,
        Enemy
    }
    public ProjectileType projectileType;

    // Update is called once per frame
    void Update()
    {
        if(projectileType == ProjectileType.Player)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (projectileType == ProjectileType.Enemy)
        {
            if (other.CompareTag("PlayerBattleHandle"))
            {
                battleMinigame.DealDamageToPlayer(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("EnemyBattleHandle"))
            {
                battleMinigame.DealDamageToEnemy(damage);
                Destroy(gameObject);
            }
        }
    }
}
