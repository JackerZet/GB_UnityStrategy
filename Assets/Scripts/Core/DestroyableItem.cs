using System;
using Abstractions;
using Abstractions.Items;
using UnityEngine;

namespace Core
{
    public class DestroyableItem : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _maxHealth;
        
        private float _health;

        public float Health => _health;
        public float MaxHealth => _maxHealth;

        private void Awake()
        {
            _health = _maxHealth;
        }
        
        public void ReceiveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            _health -= amount;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        float IHealthHolder.Health => _health;
    }
}