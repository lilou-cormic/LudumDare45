using PurpleCable;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public static event Action<EnemyDef> EnemyKilled;
    public static event Action<EnemyDef> EnemyReachedBase;

    private Rigidbody2D rb;

    public Health Health { get; private set; }

    [SerializeField] SpriteRenderer[] SpriteRenderers = null;

    public EnemyDef EnemyDef { get; private set; }

    private EnemyWaypoint _NextWaypoint;
    private EnemyWaypoint NextWaypoint
    {
        get => _NextWaypoint;

        set
        {
            _NextWaypoint = value;

            if (_NextWaypoint != null)
                _targetPosition = _NextWaypoint.transform.position;
        }
    }

    private Vector3 _targetPosition;

    private bool _isDead = false;

    public void Init(EnemyDef enemyDef, EnemyWaypoint startWaypoint)
    {
        EnemyDef = enemyDef;
        NextWaypoint = startWaypoint;

        for (int i = 0; i < enemyDef.DisplayImages.Length; i++)
        {
            SpriteRenderers[i].sprite = enemyDef.DisplayImages[i];
        }
    }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDead)
            return;

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1)
        {
            transform.position = _targetPosition;
            rb.velocity = Vector3.zero;

            if (NextWaypoint == null)
            {
                Win();
                return;
            }

            NextWaypoint = NextWaypoint.GetNextWaypoint();
        }

        rb.SetVelocityAndRotation(_targetPosition, EnemyDef.Speed);
    }

    public void GetHit(int damage)
    {
        if (_isDead)
            return;

        Health.ChangeHP(-damage);

        if (Health.CurrentHP <= 0)
            Die();
    }

    private IEnumerator DoGetHit()
    {
        foreach (var spriteRenderer in SpriteRenderers)
        {
            spriteRenderer.color = Color.red;
        }

        yield return new WaitForSecondsRealtime(0.2f);

        foreach (var spriteRenderer in SpriteRenderers)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void Die()
    {
        if (_isDead)
            return;

        EnemyKilled?.Invoke(EnemyDef);

        _isDead = true;

        StartCoroutine(DoDie());
    }

    private IEnumerator DoDie()
    {
        rb.velocity = Vector3.zero;

        //TODO Enemy.DoDie
        //Die animation
        //Explosion whatever
        EnemyDef.DieSound.Play();

        Destroy(gameObject);
        yield return null;
    }

    public void Win()
    {
        if (_isDead)
            return;

        //TODO Enemy.Win

        EnemyReachedBase?.Invoke(EnemyDef);

        Destroy(gameObject);
    }
}
