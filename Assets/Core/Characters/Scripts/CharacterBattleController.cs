// Merle Roji

using UnityEngine;
using T02.TurnBasedSystem;
using System.Collections.Generic;

namespace T02.Characters.InBattle
{
    public abstract class CharacterBattleController : StateManager
    {
        #region VARIABLES

        [Header("Base Parameters")]
        [SerializeField] private CharacterStats _baseStats;
        private CharacterStatContainer _stats;
        [SerializeField] private Transform _attackPivot;
        [SerializeField] protected SpriteRenderer _characterSprite;
        [SerializeField] protected CharacterSkillList _skillList;
        protected float _gridMoveSpeed = 3f;
        protected bool _initiated = false;
        protected Turn _turnInfo = new Turn();
        protected bool _isTurn = false;
        protected TurnBasedManager _turnManager;
        protected CharacterBattleController _target;
        protected List<CharacterBattleController> _potentialTargets = new List<CharacterBattleController>();
        protected Animator _anim;
        protected bool _isAttacking = false;
        protected bool _isMoving = false;
        protected bool _hasCollidedWithTarget = false;

        protected List<StatusEffect> _statusEffects = new List<StatusEffect>();

        protected AudioSource _soundManager;
        [SerializeField] AudioClip _hurtSound;

        #endregion

        #region UNITY METHODS

        protected virtual void Awake()
        {
            CurrentState.OnEnterExecute(this);

            _anim = GetComponent<Animator>();

            _soundManager = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        }

        protected virtual void OnDisable()
        {
            _initiated = false;
        }

        protected virtual void OnTriggerEnter(Collider col)
        {
            // target collision for mid attack
            if (_target != null)
            {
                bool collidedWithTarget = col.gameObject == _target.gameObject;
                if (collidedWithTarget)
                {
                    _hasCollidedWithTarget = true;
                }
            }
        }

        protected virtual void OnTriggerExit(Collider col)
        {
            // target collision for mid attack
            if (_target != null)
            {
                bool collidedWithTarget = col.gameObject == _target.gameObject;
                if (collidedWithTarget)
                {
                    _hasCollidedWithTarget = false;
                }
            }
        }

        #endregion

        #region CHARACTER BATTLE METHODS

        /// <summary>
        /// Starts the chosen minigame.
        /// </summary>
        public virtual void StartMinigame() { }

        /// <summary>
        /// Checks for targets when targeting.
        /// </summary>
        public virtual void CheckForTargets() { }

        /// <summary>
        /// Confirms the target selection.
        /// </summary>
        /// <param name="targetID"></param>
        public virtual void ConfirmTarget(int targetID = 0)
        {
            if (_potentialTargets.Count > 0)
            {
                _target = _potentialTargets[targetID];
                _potentialTargets.Clear();
            }
        }

        #endregion

        #region INIT

        /// <summary>
        /// Encapsulate all of the inits.
        /// </summary>
        public void Init(TurnBasedManager turnManager)
        {
            InjectTurnManager(turnManager);
            InitStats();
            _initiated = true;
        }

        /// <summary>
        /// Injects the turn for the player.
        /// </summary>
        public void InitTurn(Turn turn)
        {
            _turnInfo = turn;
        }

        /// <summary>
        /// Resets the current HP.
        /// </summary>
        private void InitStats()
        {
            _stats = ScriptableObject.CreateInstance<CharacterStatContainer>();
            _stats.InjectStats(_baseStats);
        }

        #endregion

        #region HELPERS

        public Transform AttackPivot
        { get => _attackPivot; }

        public SpriteRenderer CharacterSprite
        { get => _characterSprite; }

        public CharacterStats BaseStats
        { get => _baseStats; }

        public CharacterStatContainer Stats
        { get => _stats; }

        public AudioSource SoundManager
        { get => _soundManager; }

        public AudioClip HurtSound
        { get => _hurtSound; }

        public float GridMoveSpeed
        { get => _gridMoveSpeed; }

        public TurnBasedManager TurnManager
        { get => _turnManager; }

        public Turn TurnInfo
        {
            get => _turnInfo;
            set => _turnInfo = value;
        }

        public List<CharacterBattleController> PotentialTargets
        {
            get => _potentialTargets;
            set => _potentialTargets = value;
        }

        public List<StatusEffect> StatusEffects
        {
            get => _statusEffects;
            set => _statusEffects = value;
        }

        public bool DoesTurnManagerAndTurnInfoExist()
        {
            return _turnInfo != null && _turnManager != null;
        }

        public bool IsTurn
        {
            get => _isTurn;
            set => _isTurn = value;
        }

        public CharacterBattleController Target
        {
            get => _target;
            set => _target = value;
        }

        public bool IsAttacking
        {
            get => _isAttacking;
            set => _isAttacking = value;
        }

        public bool IsMoving
        {
            get => _isMoving;
            set => _isMoving = value;
        }

        public bool HasCollidedWithTarget
        { get => _hasCollidedWithTarget; }

        public bool HasDied
        { get => _stats.CurrentHP <= 0; }

        public MinigameStateMachine[] SkillList
        { get => _skillList.Skills; }

        public Animator Anim
        { get => _anim; }

        public bool Initiated
        { get => _initiated; }

        /// <summary>
        /// Changes animation.
        /// </summary>
        /// <param name="newAnim"></param>
        public void ChangeAnimation(string newAnim)
        {
            AnimationQoL.ChangeAnimation(_anim, newAnim);
        }

        /// <summary>
        /// Injects the new encounter's turn manager into the characters.
        /// </summary>
        /// <param name="turnManager"></param>
        public void InjectTurnManager(TurnBasedManager turnManager)
        {
            if (_turnManager != null) _turnManager = null;
            _turnManager = turnManager;
        }

        /// <summary>
        /// Finishes a turn.
        /// </summary>
        public void FinishTurn()
        {
            _isTurn = false;
            _turnInfo.IsTurn = false;
            _turnInfo.WasTurnPreviously = true;
            _isAttacking = false;
            _turnManager.SetCameraFollow(null);
        }

        /// <summary>
        /// Adds a status effect to the character
        /// </summary>
        public void AddNewEffect(StatusEffect newEffect)
        {
            for (int i = 0; i < _statusEffects.Count; i++)
            {
                if (_statusEffects[i].Type == newEffect.Type) //Checks if the character already has that status effect
                {
                    if (_statusEffects[i].NumTurns < newEffect.NumTurns) //Sets the turn timer to be the largest amount between the old and new effect
                        _statusEffects[i].NumTurns = newEffect.NumTurns;
                    return;
                }
            }
            _statusEffects.Add(newEffect);
        }

        /// <summary>
        /// Kills a character in battle.
        /// </summary>
        public void KillCharacter()
        {
            _turnManager.RemoveFromTurnOrder(this);
            _turnManager.RemoveFromParty(this);
            ChangeAnimation("death");
        }

        #endregion
    }
}

