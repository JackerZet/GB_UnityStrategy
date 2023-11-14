using Abstractions;
using Abstractions.Items;
using UnityEngine;
using Utils.Extensions;
using Utils.QuickOutline;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IDamageable
    {
        private const int MIN_HEALTH_VALUE = 100;
        private const int MAX_HEALTH_VALUE = 1000;
        private const Outline.Mode OUTLINE_DEAFULT_MODE = Outline.Mode.OutlineAll;
        private const float OUTLINE_DEAFULT_WIDTH = 5f;
        private static readonly Color OUTLINE_DEAFULT_COLOR = Color.yellow;
        
        [SerializeField] 
        private Outline _outline;

        [Range(MIN_HEALTH_VALUE, MAX_HEALTH_VALUE)]
        [SerializeField]
        private float _maxHealth;

        [SerializeField] 
        private Sprite _icon;

        private float _health = MAX_HEALTH_VALUE;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        private void Awake()
        {
            if (_outline == null)
            {
                _outline = gameObject.GetOrAddComponent<Outline>();

                _outline.OutlineMode = OUTLINE_DEAFULT_MODE;
                _outline.OutlineColor = OUTLINE_DEAFULT_COLOR;
                _outline.OutlineWidth = OUTLINE_DEAFULT_WIDTH;
                
            }
            _outline.enabled = false;
        }

        public void Select()
        {
            _outline.enabled = true;
        }

        public void UnSelect()
        {
            _outline.enabled = false;
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
    }
}