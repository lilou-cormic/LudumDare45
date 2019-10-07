using PurpleCable;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static LayerMask EnemyLayerMask => Instance.EnemyLayer;

    [SerializeField] LayerMask EnemyLayer;

    [SerializeField] GameObject ExplosionPrefab = null;

    [SerializeField] AudioClip ExplosionSound = null;

    [SerializeField] AudioClip BuildTowerSound = null;

    [SerializeField] TMPro.TextMeshProUGUI HPText = null;

    [SerializeField] TMPro.TextMeshProUGUI CashText = null;

    [SerializeField] TMPro.TextMeshProUGUI WaveText = null;

    [SerializeField] GameObject HurtPanel = null;

    [SerializeField] AudioClip HurtSound = null;

    public Player Player { get; private set; }

    private EnemySpawner[] _enemySpawners;

    private static bool _isContinue = false;

    private bool _isWinning = false;

    private int _Level = 1;
    public int Level { get => _Level; set { _Level = value; SetLevelText(); } }

    private void Awake()
    {
        Instance = this;

        Player = GetComponentInChildren<Player>();

        _enemySpawners = FindObjectsOfType<EnemySpawner>();

        HurtPanel.SetActive(false);

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

        if (!_isContinue)
            SetNewGame();
        else
            Level++;

        SetHPText();

        Player_CashChanged();

        foreach (var enemySpawner in _enemySpawners)
        {
            enemySpawner.Spawn();
        }
    }

    private void SetNewGame()
    {
        Level = 1;
        Player.Cash = 5;
        ScoreManager.ResetScore();
    }

    private void Update()
    {
        if (_isWinning)
            return;

        //FIXME
        if (_enemySpawners.All(x => x.IsDone))
        {
            if (FindObjectsOfType<Enemy>().Length == 0)
                StartCoroutine(DoWin());
        }
    }

    private IEnumerator DoWin()
    {
        ScoreManager.SetHighScore();

        _isWinning = true;
        _isContinue = true;

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Win");
    }

    private void OnDestroy()
    {
        Enemy.EnemyKilled -= Enemy_EnemyKilled;
        Enemy.EnemyReachedBase -= Enemy_EnemyReachedBase;
    }

    private void Enemy_EnemyKilled(EnemyDef enemyDef)
    {
        ScoreManager.AddPoints(enemyDef.Points);

        Player.Cash += enemyDef.Points;
        Player_CashChanged();
    }

    private void Enemy_EnemyReachedBase(EnemyDef enemyDef)
    {
        Player.Health.ChangeHP(-1);
        SetHPText();

        StartCoroutine(DoHurt());
    }

    private IEnumerator DoHurt()
    {
        HurtSound.Play();

        HurtPanel.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        HurtPanel.SetActive(false);
    }

    private void SetHPText() => HPText.text = "HP: " + Player.Health.CurrentHP;

    private void SetCashText() => CashText.text = "$" + Player.Cash;

    private void SetLevelText() => WaveText.text = "Wave: " + Level.ToString("00");

    private void Health_HPDepleted(Health obj)
    {
        StartCoroutine(DoGameOver());
    }

    private void Player_CashChanged()
    {
        SetCashText();
    }

    private IEnumerator DoGameOver()
    {
        ScoreManager.SetHighScore();

        _isContinue = false;

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("GameOver");
    }


    public TowerDef CurrentTowerDef;

    public static void BuildTower(BuildSpace buildSpace)
    {
        Instance.BuildTowerSound.Play();
        Player.Cash -= Instance.CurrentTowerDef.BuildCost;
        Instance.Player_CashChanged();

        var tower = Instantiate(Instance.CurrentTowerDef.Prefab, buildSpace.transform.position, Quaternion.Euler(0, 0, 90), buildSpace.transform);
        tower.Init(Instance.CurrentTowerDef);
    }

    public void SetCurrentTowerDef(TowerDef towerDef)
    {
        CurrentTowerDef = towerDef;
    }


    public static void Explosion(Vector3 position, float radius)
    {
        if (radius == 0)
            return;

        Instance.ExplosionSound.Play();

        var explosion = Instantiate(Instance.ExplosionPrefab, position, Quaternion.identity);
        explosion.transform.localScale = Vector3.one * radius;
        Destroy(explosion, 2f);
    }
}
