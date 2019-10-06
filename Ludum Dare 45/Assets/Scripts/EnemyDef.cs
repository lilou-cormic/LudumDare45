using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemyDef : ScriptableObject, IEqualityComparer<EnemyDef>
{
    public string Name { get => name; set => name = value; }

    [SerializeField] string _DisplayName = null;
    public string DisplayName => _DisplayName;

    [SerializeField] Sprite[] _DisplayImages = null;
    public Sprite[] DisplayImages => _DisplayImages;

    [SerializeField] Enemy _EnemyPrefab = null;
    public Enemy EnemyPrefab => _EnemyPrefab;

    [SerializeField] int _Points = 1;
    public int Points => _Points;

    [SerializeField] float _Speed = 3f;
    public float Speed => _Speed;

    [SerializeField] AudioClip _DieSound;
    public AudioClip DieSound => _DieSound;

    public override string ToString()
        => !string.IsNullOrWhiteSpace(DisplayName) ? DisplayName : Name;

    public override int GetHashCode()
        => Name?.GetHashCode() ?? base.GetHashCode();

    public override bool Equals(object other)
        => Equals(this, other as EnemyDef);

    #region IEqualityComparer<EnemyDef>

    public bool Equals(EnemyDef x, EnemyDef y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(null, x))
            return false;

        if (ReferenceEquals(null, y))
            return false;

        return x.Name == y.Name;
    }

    public int GetHashCode(EnemyDef obj)
        => obj.GetHashCode();

    #endregion
}
