// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    public class EnemyBattleController : CharacterBattleController
    {
        #region VARIABLES

        [Header("Enemy Battle Parameters")]
        [SerializeField] private LayerMask _characterLayer;
        [SerializeField, Min(1)] private int _aggroRange = 9;
        private bool _isAggroed = false;
        private bool _hasBeenCountered = false;
        private int _chosenAttackIndex = 0;

        #endregion

        #region UNITY METHODS

        protected override void OnTriggerEnter(Collider col)
        {
            base.OnTriggerEnter(col);

            if (col.tag == "Counter") _hasBeenCountered = true;
        }

        protected override void OnTriggerExit(Collider col)
        {
            base.OnTriggerExit(col);

            if (col.tag == "Counter") _hasBeenCountered = false;
        }

        #endregion

        #region ENEMY BATTLE METHODS

        public override void CheckForTargets()
        {
            SkillList[_chosenAttackIndex].CheckForTargets(this);
        }

        public override void StartMinigame()
        {
            base.StartMinigame();

            Debug.Log("Enemy is attacking!");

            if (_target != null)
            {
                if (_target.transform.position.x > transform.position.x)
                {
                    _characterSprite.flipX = false;
                }
                else if (_target.transform.position.x < transform.position.x)
                {
                    _characterSprite.flipX = true;
                }
            }

            MinigameStateMachine firstAttack = Instantiate(SkillList[_chosenAttackIndex], this.transform.position, Quaternion.identity);
            firstAttack.InjectCharacter(this);
        }

        public override void ConfirmTarget(int targetID = 0)
        {
            base.ConfirmTarget(targetID);

            if (_target != null)
            {
                if (_target.transform.position.x > transform.position.x)
                {
                    _characterSprite.flipX = false;
                }
                else if (_target.transform.position.x < transform.position.x)
                {
                    _characterSprite.flipX = true;
                }
            }
        }

        #endregion

        #region HELPERS

        public LayerMask CharacterLayer
        { get => _characterLayer; }

        public int AggroRange
        { get => _aggroRange; }

        public bool IsAggroed
        {
            get => _isAggroed;
            set => _isAggroed = value;
        }

        public bool HasBeenCountered
        {
            get => _hasBeenCountered;
            set => _hasBeenCountered = value;
        }

        public int ChosenAttackIndex
        {
            get => _chosenAttackIndex;
            set => _chosenAttackIndex = value;
        }

        #endregion
    }
}