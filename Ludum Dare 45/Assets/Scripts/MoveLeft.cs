using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < -10)
            Destroy(gameObject);
    }
}
