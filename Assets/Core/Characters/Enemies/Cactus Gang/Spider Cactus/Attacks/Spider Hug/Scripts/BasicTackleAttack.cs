// Merle Roji

using UnityEngine;

namespace T02.Characters.InBattle
{
    public class BasicTackleAttack : MinigameStateMachine
    {
        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            _damage = _baseDamage;
            CurrentState.OnEnterExecute(this);
        }

        #endregion

        #region HELPERS

        public override void CheckForTargets(CharacterBattleController character)
        {
            //// get array of character colliders
            //Collider[] hitColliders = null;
            //hitColliders = Physics.OverlapSphere(character.transform.position, _range, _characterLayer, QueryTriggerInteraction.Collide);
            //Debug.Log("It's " + (hitColliders.Length > 0) + " that targets were found!!");
            //
            //// add the colliders into the potential target list
            //if (hitColliders != null)
            //{
            //    foreach (Collider col in hitColliders)
            //    {
            //        if (col.TryGetComponent<CharacterBattleController>(out CharacterBattleController potentialTarget) && potentialTarget != character)
            //        {
            //            Debug.Log(col.name + "is in range!");
            //            character.PotentialTargets.Add(potentialTarget);
            //        }
            //    }
            //}
        }

        #endregion
    }
}