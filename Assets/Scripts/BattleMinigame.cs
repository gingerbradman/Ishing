using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMinigame : MonoBehaviour
{
    #region Player
    public PlayerController playerController; // Reference to the player controller
    public Slider playerSlider; // Slider representing player's progress
    public float playerSpeed; // Speed of the player
    public int playwerHealth = 3; // Player's health
    public GameObject playerHandle; // Handle for the player's position
    public GameObject playerWeapon; // Player's weapon
    #endregion

    #region Enemy
    public Enemy enemy; // Reference to the enemy
    public Slider enemySlider; // Slider representing enemy's progress
    public Vector2 movementDirection; // Direction of enemy movement
    public float directionChangeTime; // Time interval for changing direction
    public float latestDirectionChangeTime; // Last time the direction was changed
    public float enemySpeed; // Speed of the enemy
    public int enemyHealth = 3; // Enemy's health
    public GameObject enemyHandle; // Handle for the enemy's position
    public GameObject enemyWeapon; // Enemy's weapon
    public Enemy.EnemyState enemyState; // State of the enemy
    private bool isTransitioningState = false; // Flag to check if state transition is in progress
    #endregion

    private void OnEnable()
    {
        // Set the initial position and reset the battle game
        this.transform.position = playerController.transform.position;
        ResetBattleGame();
    }

    private void ResetBattleGame()
    {
        // Setup sliders and initialize direction change time
        SetupEnemySlider(enemySlider);
        SetupPlayerSlider(playerSlider);
        latestDirectionChangeTime = 0f;
        calculateNewMovementDirection();
    }

    public void SetEnemy(Enemy enemy)
    {
        // Set enemy properties
        this.enemy = enemy;
        enemyState = enemy.enemyState;
        enemySpeed = enemy.battleSpeed;
        enemyHealth = enemy.health;
        enemyWeapon = enemy.enemyWeapon;
        movementDirection = enemy.movementDirection;
        directionChangeTime = enemy.directionChangeTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player input
        PlayerInput();
    }

    private void FixedUpdate()
    {
        // Handle enemy logic
        EnemyLogic();
    }

    #region Player Logic    

    public void PlayerInput()
    {
        // Move player slider based on input
        if (Input.GetKey(KeyCode.D))
        {
            FillSlider(playerSlider, playerSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            FillSlider(playerSlider, playerSpeed * -1);
        }

        // Handle player attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }
    }

    public void PlayerAttack()
    {
        // Instantiate and configure player projectile
        GameObject projectile = Instantiate(playerWeapon, playerHandle.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().isEnemyProjectile = false;
        projectile.GetComponent<Projectile>().battleMinigame = this;
        projectile.GetComponent<Projectile>().projectileType = Projectile.ProjectileType.Player;
    }

    #endregion

    #region Enemy Logic
    public void EnemyLogic()
    {
        if (!isTransitioningState)
        {
            switch (enemyState)
            {
                case Enemy.EnemyState.Idle:
                    StartCoroutine(TransitionState(Enemy.EnemyState.Moving, 2f)); // Example: transition to Moving state after 2 seconds
                    break;
                case Enemy.EnemyState.Moving:
                    StartCoroutine(TransitionState(Enemy.EnemyState.Attack, 3f)); // Example: transition to Attack state after 3 seconds
                    break;
                case Enemy.EnemyState.Attack:
                    StartCoroutine(TransitionState(Enemy.EnemyState.Idle, 1f)); // Example: transition to Idle state after 1 second
                    break;
                // Add more states as needed
            }
        }

        switch (enemyState)
        {
            case Enemy.EnemyState.Idle:
                // Logic for idle state
                EnemyIdle();
                break;
            case Enemy.EnemyState.Moving:
                // Logic for moving state
                EnemyMovement();
                break;
            case Enemy.EnemyState.Attack:
                // Logic for attack state
                EnemyAttack();
                break;
        }
    }

    private IEnumerator TransitionState(Enemy.EnemyState newState, float waitTime)
    {
        isTransitioningState = true;
        yield return new WaitForSeconds(waitTime);
        enemyState = newState;
        isTransitioningState = false;
    }

    private void EnemyIdle()
    {
        // Logic for idle state
        // For example, do nothing or wait for a certain condition to change state
    }

    public void EnemyMovement()
    {
        // Change direction if the time interval has passed
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calculateNewMovementDirection();
        }

        // Move enemy slider
        FillSlider(enemySlider, movementDirection.x * enemySpeed);
    }

    private void calculateNewMovementDirection()
    {
        // Calculate a new random movement direction
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void EnemyAttack()
    {
        // Instantiate and configure enemy projectile
        GameObject projectile = Instantiate(enemyWeapon, enemyHandle.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().isEnemyProjectile = true;
        projectile.GetComponent<Projectile>().battleMinigame = this;
        projectile.GetComponent<Projectile>().projectileType = Projectile.ProjectileType.Enemy;
    }

    #endregion

    #region Utility Methods 

    void FillSlider(Slider slider, float value)
    {
        // Fill the slider based on the value
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
        // Initialize enemy slider value
        slider.value = slider.maxValue / 2;
    }

    void SetupPlayerSlider(Slider slider)
    {
        // Initialize player slider value
        slider.value = slider.maxValue / 2;
    }

    public void DealDamageToPlayer(int damage)
    {
        // Reduce player health and check for game over
        playwerHealth -= damage;
        if (playwerHealth <= 0)
        {
            playerController.LoseBattle();
        }
    }

    public void DealDamageToEnemy(int damage)
    {
        // Reduce enemy health and check for victory
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            playerController.EndBattle();
        }
    }

    #endregion
}
