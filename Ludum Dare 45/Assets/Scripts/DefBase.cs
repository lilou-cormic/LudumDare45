using System.Collections.Generic;
using UnityEngine;

public class DefBase<TDef, TPrefab> : ScriptableObject, IEqualityComparer<TDef>
    where TDef : DefBase<TDef, TPrefab>
    where TPrefab : MonoBehaviour
{
    public string Name { get => name; set => name = value; }

    [SerializeField] string _DisplayName = null;
    public string DisplayName => _DisplayName;

    [SerializeField] Sprite[] _DisplayImages = null;
    public Sprite[] DisplayImages => _DisplayImages;

    [SerializeField] TPrefab _Prefab = null;
    public TPrefab Prefab => _Prefab;

    public override string ToString()
        => !string.IsNullOrWhiteSpace(DisplayName) ? DisplayName : Name;

    public override int GetHashCode()
        => Name?.GetHashCode() ?? base.GetHashCode();

    public override bool Equals(object other)
        => Equals(this, other as TDef);

    #region IEqualityComparer<EnemyDef>

    public bool Equals(TDef x, TDef y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(null, x))
            return false;

        if (ReferenceEquals(null, y))
            return false;

        return x.Name == y.Name;
    }

    public int GetHashCode(TDef obj)
        => obj.GetHashCode();

    #endregion
}
