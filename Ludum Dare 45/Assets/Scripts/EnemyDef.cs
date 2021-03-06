﻿using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemyDef : DefBase<EnemyDef, Enemy>
{
    [SerializeField] int _Points = 1;
    public int Points => _Points;

    [SerializeField] float _Speed = 1f;
    public float Speed => _Speed;

    [SerializeField] int _MaxHP = 1;
    public int MaxHP => _MaxHP;

    [SerializeField] AudioClip _DieSound;
    public AudioClip DieSound => _DieSound;
}
