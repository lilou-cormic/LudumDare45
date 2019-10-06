using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Projectile")]
public class ProjectileDef : DefBase<ProjectileDef, Projectile>
{
    [SerializeField] int _Damage = 1;
    public int Damage => _Damage;

    [SerializeField] float _Radius = 0f;
    public float Radius => _Radius;

    [SerializeField] float _Speed = 5f;
    public float Speed => _Speed;

    [SerializeField] bool _IsFixedTarget = false;
    public bool IsFixedTarget => _IsFixedTarget;

    [SerializeField] bool _IsHoming = false;
    public bool IsHoming => _IsHoming;
}
