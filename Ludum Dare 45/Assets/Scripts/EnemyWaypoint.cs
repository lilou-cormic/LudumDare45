using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    [SerializeField]
    private EnemyWaypoint _NextWaypoint = null;

    [SerializeField]
    private EnemyWaypoint[] NextWaypoints = null;

    private void OnValidate()
    {
        if (_NextWaypoint != null && NextWaypoints != null && NextWaypoints.Length == 0)
            NextWaypoints = new EnemyWaypoint[] { _NextWaypoint };
    }

    public EnemyWaypoint GetNextWaypoint() => NextWaypoints.GetRandom();

    private void OnDrawGizmos()
    {
        if (NextWaypoints != null)
        {
            foreach (var waypoint in NextWaypoints)
            {
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
            }
        }
    }
}
