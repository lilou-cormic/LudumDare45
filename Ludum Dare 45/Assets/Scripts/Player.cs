using PurpleCable;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }
}
