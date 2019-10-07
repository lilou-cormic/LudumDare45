using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] SpriteRenderers = null;

    //public TowerDef TowerDef { get; private set; }
    [SerializeField] TowerDef TowerDef;

    [SerializeField] Transform Turret = null;

    [SerializeField] Transform[] FirePoints = null;

    [SerializeField] GameObject Armed = null;

    private float _timeLeft = 0f;

    private Transform _Target = null;

    private void Start()
    {
        Init(TowerDef);
    }

    public void Init(TowerDef towerDef)
    {
        TowerDef = towerDef;

        for (int i = 0; i < towerDef.DisplayImages.Length; i++)
        {
            SpriteRenderers[i].sprite = towerDef.DisplayImages[i];
        }
    }

    private void Update()
    {
        AcquireTarget();

        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            SetIsArmed(true);

            Shoot();

            _timeLeft = TowerDef.CoolDown;
        }

        if (_Target != null)
            Turret.transform.SetRotation2D(_Target.position);
    }

    private void AcquireTarget()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, TowerDef.Range, GameManager.EnemyLayerMask);

        if (cols.Length == 0)
        {
            _Target = null;
            return;
        }

        if (cols.Length == 1)
        {
            _Target = cols[0].transform;
            return;
        }

        if (_Target != null && cols.Any(x => x.transform == _Target))
            return;

        float minDist = float.MaxValue;
        Transform nearestTarget = null;

        foreach (var col in cols)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);

            if (dist < minDist)
                nearestTarget = col.transform;
        }

        _Target = nearestTarget;
    }

    private void Shoot()
    {
        if (_Target == null)
            return;

        foreach (var firePoint in FirePoints)
        {
            var projectile = Instantiate(TowerDef.ProjectileDef.Prefab, firePoint.position, firePoint.rotation);
            projectile.Init(TowerDef.ProjectileDef, _Target);

            Destroy(projectile.gameObject, 10f);
        }

        SetIsArmed(false);
    }

    private void OnDrawGizmos()
    {
        if (_Target != null)
            Gizmos.DrawLine(transform.position, _Target.position);

        if (TowerDef != null)
            Gizmos.DrawWireSphere(transform.position, TowerDef.Range);
    }

    private void SetIsArmed(bool isArmed)
    {
        Armed.SetActive(isArmed);
    }
}
