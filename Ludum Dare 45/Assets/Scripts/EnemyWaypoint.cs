using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    [SerializeField]
    private EnemyWaypoint[] NextWaypoints = null;

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
