using System;
using UnityEngine;

namespace PurpleCable
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private int _MaxHP;
        public int MaxHP { get => _MaxHP; set => _MaxHP = value; }

        public int CurrentHP { get; private set; }

        public float Percent => CurrentHP / (float)MaxHP;

        public static event Action<Health> HealthAdded;

        public static event Action<Health> HealthRemoved;

        public event Action<Health> HPChanged;

        public event Action<Health> HPDepleted;

        private void OnEnable()
        {
            CurrentHP = MaxHP;
            HealthAdded?.Invoke(this);
        }

        private void OnDisable()
        {
            HealthRemoved?.Invoke(this);
        }

        public void ChangeHP(int amount)
        {
            CurrentHP = Mathf.Clamp(CurrentHP + amount, 0, MaxHP);

            HPChanged?.Invoke(this);

            if (CurrentHP == 0)
                HPDepleted?.Invoke(this);
        }
    }
}
