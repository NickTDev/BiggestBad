// Merle Roji

using T02.Characters.InBattle;
using UnityEngine;

namespace T02.TurnBasedSystem
{
    /// <summary>
    /// Holds the information for a character's turn.
    /// </summary>
    [System.Serializable]
    public class Turn
    {
        public GameObject Character = null;
        public int Speed;
        public bool IsTurn;
        public bool WasTurnPreviously;

        public Turn()
        {
            Character = null;
            Speed = 0;
            IsTurn = false;
            WasTurnPreviously = false;
        }
    }
}