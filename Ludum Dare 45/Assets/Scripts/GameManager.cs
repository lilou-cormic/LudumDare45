using PurpleCable;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player Player { get; private set; }

    private EnemySpawner[] _enemySpawners;

    private void Awake()
    {
        Instance = this;

        Player = GetComponentInChildren<Player>();

        _enemySpawners = FindObjectsOfType<EnemySpawner>();

        Enemy.EnemyKilled += Enemy_EnemyKilled;
        Enemy.EnemyReachedBase += Enemy_EnemyReachedBase;
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        Player.Health.HPDepleted += Health_HPDepleted;

        //TODO ScoreManager.ResetScore(); ??

        ScoreManager.ResetScore();

        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.Spawn();
        }
    }

    private void OnDestroy()
    {
        Enemy.EnemyKilled -= Enemy_EnemyKilled;
        Enemy.EnemyReachedBase -= Enemy_EnemyReachedBase;
    }

    private void Enemy_EnemyKilled(EnemyDef enemyDef)
    {
        ScoreManager.AddPoints(enemyDef.Points);
    }

    private void Enemy_EnemyReachedBase(EnemyDef enemyDef)
    {
        //TODO Enemy_EnemyReachedBase?

        Player.Health.ChangeHP(-enemyDef.Points);
    }

    private void Health_HPDepleted(Health obj)
    {
        SceneManager.LoadScene("GameOver");
    }
}
