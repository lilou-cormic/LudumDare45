using UnityEngine;

public static class RotationExtensions
{
    public static void SetRotation2D(this Transform transform, Vector3 to)
    {
        SetRotation2D(transform, transform.position, to);
    }

    public static void SetRotation2D(this Transform transform, Vector3 from, Vector3 to)
    {
        var direction = from.Direction(to);

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public static void SetVelocityAndRotation(this Rigidbody2D rb, Vector3 target, float speed)
    {
        var direction = rb.transform.position.Direction(target);

        rb.velocity = direction * speed;
        SetRotation(rb);
    }

    public static void SetRotation(this Rigidbody2D rb)
    {
        rb.rotation = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
    }

    private static Vector3 Direction(this Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }
}
