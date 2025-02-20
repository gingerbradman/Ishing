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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemyProjectile)
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
