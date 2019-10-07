using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] SpriteRenderer SpriteRenderer = null;

    public ProjectileDef ProjectileDef { get; private set; }

    private Transform _target;

    private Vector3 _targetLocation;

    private bool _hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (ProjectileDef != null)
            rb.velocity = transform.up * ProjectileDef.Speed;
    }

    public void Init(ProjectileDef projectileDef, Transform target)
    {
        ProjectileDef = projectileDef;

        rb.velocity = transform.up * ProjectileDef.Speed;

        SpriteRenderer.sprite = projectileDef.DisplayImages[0];

        if (target != null && (ProjectileDef.IsHoming || ProjectileDef.IsFixedTarget))
        {
            _target = target;
            _targetLocation = _target.transform.position;
        }
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        if (ProjectileDef.IsHoming)
        {
            _targetLocation = _target.transform.position;

            rb.SetVelocityAndRotation(_targetLocation, ProjectileDef.Speed);
        }

        if (Vector3.Distance(transform.position, _targetLocation) < 0.1)
            Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasHit)
            return;

        _hasHit = true;

        Explode(collision.gameObject.GetComponent<Enemy>());
    }

    private void Explode(Enemy enemy = null)
    {
        if (ProjectileDef.Radius == 0)
        {
            enemy?.GetHit(ProjectileDef.Damage);
        }
        else
        {
            GameManager.Explosion(transform.position, ProjectileDef.Radius);

            var cols = Physics2D.OverlapCircleAll(transform.position, ProjectileDef.Radius, GameManager.EnemyLayerMask);

            foreach (var col in cols)
            {
                col.GetComponent<Enemy>().GetHit(ProjectileDef.Damage);
            }
        }

        Destroy(gameObject);
    }
}
