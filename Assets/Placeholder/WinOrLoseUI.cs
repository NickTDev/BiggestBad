// Merle Roji

using T02.Characters.InBattle;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace T02.TurnBasedSystem
{
    public class WinOrLoseUI : MonoBehaviour
    {
        private TurnBasedManager _turnManager;

        [SerializeField] private string _winScene;
        [SerializeField] private string _loseScene;

        private void Awake()
        {
            _turnManager = GetComponent<TurnBasedManager>();
        }

        private void Update()
        {
            if (!_turnManager.FinishedInjecting) { return; }

            if (_turnManager.PlayerParty.Count <= 0)
            {
                Debug.Log("Player Died");
                SceneManager.LoadScene(_loseScene);
            }
                
            if (_turnManager.EnemyParty.Count <= 0)
            {
                Debug.Log("Enemy Died");
                SceneManager.LoadScene(_winScene);
            } 
        }
    }
}