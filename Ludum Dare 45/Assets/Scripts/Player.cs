using PurpleCable;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public Health Health { get; private set; }

    public static int Cash { get; set; } = 0;

    private void Awake()
    {
        Health = GetComponent<Health>();
    }
}
