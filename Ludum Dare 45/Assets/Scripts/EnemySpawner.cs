using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyWaypoint EnemyWaypoint = null;

    [SerializeField] EnemyWaveDef EnemyWaveDef = null;

    private Queue<EnemyDelay> _enemyQueue = null;

    public bool IsDone { get; private set; }

    private void FillEnemyQueue()
    {
        _enemyQueue = new Queue<EnemyDelay>();

        foreach (var group in EnemyWaveDef.Sequence)
        {
            _enemyQueue.Enqueue(new EnemyDelay(null, group.Delay));

            for (int i = 0; i < group.EnemyCount; i++)
            {
                _enemyQueue.Enqueue(new EnemyDelay(group.EnemyDef, group.Spacing));
            }
        }
    }

    public void Spawn()
    {
        FillEnemyQueue();

        StartCoroutine(DoSpawn());
    }

    private IEnumerator DoSpawn()
    {
        while (_enemyQueue.Count > 0)
        {
            EnemyDelay enemyDelay = _enemyQueue.Dequeue();

            yield return new WaitForSeconds(enemyDelay.Delay);

            if (enemyDelay.EnemyDef != null)
            {
                Enemy enemy = Instantiate(enemyDelay.EnemyDef.Prefab, transform);
                enemy.Init(enemyDelay.EnemyDef, EnemyWaypoint);
            }
        }

        IsDone = true;

        yield return null;
    }

    private class EnemyDelay
    {
        public EnemyDef EnemyDef { get; }

        public float Delay { get; }

        public EnemyDelay(EnemyDef enemyDef, float delay)
        {
            EnemyDef = enemyDef;
            Delay = delay;
        }
    }
}
