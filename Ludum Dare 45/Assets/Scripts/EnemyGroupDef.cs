using System;
using UnityEngine;

[Serializable]
public class EnemyGroupDef
{
    [SerializeField] EnemyDef _EnemyDef = null;
    public EnemyDef EnemyDef => _EnemyDef;

    [SerializeField] int _EnemyCount = 1;
    public int EnemyCount => _EnemyCount;

    [SerializeField] float _Delay = 0.5f;
    public float Delay => _Delay;

    [SerializeField] float _Spacing = 0.5f;
    public float Spacing => _Spacing;
}