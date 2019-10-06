using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Tower")]
public class TowerDef : DefBase<TowerDef, Tower>
{
    [SerializeField] float _Range = 2f;
    public float Range => _Range;

    [SerializeField] ProjectileDef _ProjectileDef = null;
    public ProjectileDef ProjectileDef => _ProjectileDef;

    [SerializeField] int _ProjectileCount = 1;
    public int ProjectileCount => _ProjectileCount;

    [SerializeField] float _FireRate = 0.2f;
    public float FireRate => _FireRate;

    [SerializeField] float _CoolDown = 0f;
    public float CoolDown => _CoolDown;
}
