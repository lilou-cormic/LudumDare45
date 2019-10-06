using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public static event Action<EnemyDef> EnemyKilled;
    public static event Action<EnemyDef> EnemyReachedBase;

    private Rigidbody2D rb;

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

    public void Init(EnemyDef enemyDef, EnemyWaypoint startWaypoint)
    {
        EnemyDef = enemyDef;
        NextWaypoint = startWaypoint;

        for (int i = 0; i < enemyDef.DisplayImages.Length; i++)
        {
            SpriteRenderers[i].sprite = enemyDef.DisplayImages[i];
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

        var direction = (_targetPosition - transform.position).normalized;

        rb.velocity = direction * EnemyDef.Speed * Time.deltaTime;
        rb.rotation = -Quaternion.LookRotation(direction).eulerAngles.x;
    }

    public void Die()
    {
        EnemyKilled?.Invoke(EnemyDef);

        StartCoroutine(DoDie());
    }

    private IEnumerator DoDie()
    {
        //TODO Enemy.DoDie
        //Die animation
        //Explosion whatever
        EnemyDef.DieSound.Play();

        Destroy(gameObject);
        yield return null;
    }

    public void Win()
    {
        //TODO Enemy.Win

        EnemyReachedBase?.Invoke(EnemyDef);

        Destroy(gameObject);
    }
}
