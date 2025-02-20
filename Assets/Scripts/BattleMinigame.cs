using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMinigame : MonoBehaviour
{
    #region Player
    public PlayerController playerController;
    public Slider playerSlider;
    public float playerSpeed;
    public int playwerHealth = 3;
    public GameObject playerHandle;
    public GameObject playerWeapon;
    #endregion

    #region Enemy
    public Enemy enemy;
    public Slider enemySlider;
    public Vector2 movementDirection;
    public float directionChangeTime;
    public float latestDirectionChangeTime;
    public float enemySpeed;
    public int enemyHealth = 3;
    public GameObject enemyHandle;
    public GameObject enemyWeapon;
    public Enemy.EnemyState enemyState;
    #endregion

    private void OnEnable()
    {
        this.transform.position = playerController.transform.position;
        ResetBattleGame();
    }

    private void ResetBattleGame()
    {
        SetupEnemySlider(enemySlider);
        SetupPlayerSlider(playerSlider);

        latestDirectionChangeTime = 0f;
        calculateNewMovementDirection();
    }

    public void SetEnemy(Enemy enemy)
    {  
        this.enemy = enemy;
        enemyState = enemy.enemyState;
        enemySpeed = enemy.battleSpeed;
        enemyHealth = enemy.health;
        enemyWeapon = enemy.enemyWeapon;
        movementDirection = enemy.movementDirection;
        directionChangeTime = enemy.directionChangeTime;
        latestDirectionChangeTime = enemy.latestDirectionChangeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            FillSlider(playerSlider, playerSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            FillSlider(playerSlider, playerSpeed * -1);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }
    }

    private void FixedUpdate()
    {
        if(Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calculateNewMovementDirection();
        }
    }

    public void EnemyAttack()
    {
        GameObject projectile = Instantiate(enemyWeapon, enemyHandle.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().isEnemyProjectile = true;
        projectile.GetComponent<Projectile>().battleMinigame = this;
    }

    private void calculateNewMovementDirection()
    {
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        FillSlider(enemySlider, movementDirection.x * enemySpeed);
    }

    public void PlayerAttack()
    {
        GameObject projectile = Instantiate(playerWeapon, playerHandle.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().isEnemyProjectile = false;
        projectile.GetComponent<Projectile>().battleMinigame = this;
    }

    public void FishMovement(float x)
    {
        FillSlider(enemySlider, x);
    }

    void FillSlider(Slider slider, float value)
    {
        float fillTime = Mathf.Abs(value) * Time.deltaTime;
        if (value > 0)
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

    public void DealDamageToPlayer(int damage)
    {
        playwerHealth -= damage;
        if (playwerHealth <= 0)
        {
            playerController.LoseBattle();
        }
    }

    public void DealDamageToEnemy(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            playerController.EndBattle();
        }
    }
}
