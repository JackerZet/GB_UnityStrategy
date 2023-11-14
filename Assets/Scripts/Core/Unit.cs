using Abstractions;
using Abstractions.Items;
using Core.CommandExecutors;
using Core.CommandsRealization;
using UnityEngine;
using Utils.Extensions;
using Utils.QuickOutline;

namespace Core
{
    public class Unit : MonoBehaviour, ISelectable, IDamageable, IDamageDealer, IAutomaticAttacker
    {
        private const int MIN_HEALTH_VALUE = 50;
        private const int MAX_HEALTH_VALUE = 200;
        private const Outline.Mode OUTLINE_DEAFULT_MODE = Outline.Mode.OutlineAll;
        private const float OUTLINE_DEAFULT_WIDTH = 5f;
        private static readonly Color OUTLINE_DEAFULT_COLOR = Color.green;
        private static readonly int DEAD = Animator.StringToHash("Dead");
        
        [SerializeField] 
        private int _damage = 25;
        
        [Range(MIN_HEALTH_VALUE, MAX_HEALTH_VALUE)]
        [SerializeField]
        private float _maxHealth;

        [SerializeField] 
        private Sprite _icon;
        
        [SerializeField] 
        private Outline _outline;
        
        [SerializeField]
        private Animator _animator;

        [SerializeField] 
        private CommandExecutorStop _stopCommandExecutor;
        
        [SerializeField] private float _visionRadius = 3f;
        
        private float _health = MIN_HEALTH_VALUE;

        public int Damage => _damage;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        public float VisionRadius => _visionRadius;
        
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
            if (!this)
                return;
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
                _animator.SetTrigger(DEAD);
                Invoke(nameof(Destroy), 1f);
            }
        }

        private async void Destroy()
        {
            await _stopCommandExecutor.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }
    }
}