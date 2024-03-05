// Merle Roji

using System.Collections.Generic;
using UnityEngine;
using T02.Characters.InBattle;
using T02.UI;
using T02.Characters;
using T02.PathSystem;
using TMPro;
using Cinemachine;
using UnityEngine.TextCore.Text;

namespace T02.TurnBasedSystem
{
    /// <summary>
    /// Handles the turns of the characters in the Turn Order.
    /// </summary>
    public class TurnBasedManager : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private TextMeshProUGUI _currentCharacterText;
        private GameObject _currentCharacter;
        private GameObject _previousCurrentCharacter;

        private int _roundCounter = 0;
        private int _turnCounter = 0;

        [Header("Character Lists")]
        [SerializeField] private CharacterPartyManager _incomingPlayers;
        [SerializeField] private CharacterPartyManager _incomingEnemies;
        [SerializeField] private List<Turn> _turnOrder;
        [SerializeField] private List<CharacterBattleController> _allCharacters;
        [SerializeField] private List<CharacterBattleController> _allSpawnedCharacters;
        [SerializeField] private List<CharacterBattleController> _playerParty;
        [SerializeField] private List<CharacterBattleController> _enemyParty;

        private BattleMenuUI _battleMenu;
        private SkillMenuUI _skillMenu;

        private WorldGrid _battlefieldGrid;
        private Pathfinding _battlefieldPathfinding;

        [Header("Spawn Lists")]
        [SerializeField] private Transform[] _playerSpawns;
        [SerializeField] private Transform[] _enemySpawns;

        private bool _finishedInjecting = false;

        private Camera _mainCam;
        private CinemachineVirtualCamera _virtualCam;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _mainCam = Camera.main;
            _virtualCam = _mainCam.GetComponent<CinemachineVirtualCamera>();

            _battleMenu = GetComponentInChildren<BattleMenuUI>();
            ToggleBattleMenu(false);
            _skillMenu = GetComponentInChildren<SkillMenuUI>();
            ToggleSkillMenu(false);

            _battlefieldGrid = GetComponentInChildren<WorldGrid>();
            _battlefieldPathfinding = GetComponentInChildren<Pathfinding>();

            FillCharacterList();
            SpawnCharacters();
            FillTurnOrder();
            SortBySpeed();
            ResetTurns();

            _roundCounter = 0;
            _turnCounter = 0;
        }

        private void Update()
        {
            UpdateTurns();
        }

        #endregion

        #region TURN MANAGING

        /// <summary>
        /// Fills the turn order.
        /// </summary>
        private void FillCharacterList()
        {
            // new lists of all characters
            _allCharacters.AddRange(_incomingPlayers.CharacterParty);
            _allCharacters.AddRange(_incomingEnemies.CharacterParty);

            for(int i = 0; i < _allCharacters.Count; i++)
            {
                if (_allCharacters[i].CompareTag("Player"))
                    _playerParty.Add(_allCharacters[i]);

                if (_allCharacters[i].CompareTag("Enemy"))
                    _enemyParty.Add(_allCharacters[i]);
            }
        }

        /// <summary>
        /// Spawns the characters into the map.
        /// </summary>
        private void SpawnCharacters()
        {
            // spawn players
            for (int p = 0; p < _playerParty.Count; p++)
            {
                PlayerBattleController newPlayer = Instantiate(_playerParty[p], _playerSpawns[p].position, Quaternion.identity) as PlayerBattleController;
                _playerParty[p] = newPlayer;
                _playerParty[p].gameObject.name = newPlayer.BaseStats.Name;
                _allSpawnedCharacters.Add(newPlayer);
                _turnOrder.Add(newPlayer.TurnInfo);
            }

            // spawn enemies
            for (int e = 0; e < _enemyParty.Count; e++)
            {
                EnemyBattleController newEnemy = Instantiate(_enemyParty[e], _enemySpawns[e].position, Quaternion.identity) as EnemyBattleController;
                _enemyParty[e] = newEnemy;
                _enemyParty[e].gameObject.name = newEnemy.BaseStats.Name;
                _allSpawnedCharacters.Add(newEnemy);
                _turnOrder.Add(newEnemy.TurnInfo);
            }
        }

        /// <summary>
        /// Injects all of the required information into the turn order.
        /// </summary>
        private void FillTurnOrder()
        {
            // initialize all of the turns for the characters.
            for (int c = 0; c < _allSpawnedCharacters.Count; c++)
            {
                _allSpawnedCharacters[c].Init(this);
                _turnOrder[c].Character = _allSpawnedCharacters[c].gameObject;
                Debug.Log(_turnOrder[c].Character.name);
                _turnOrder[c].Character.name = _allSpawnedCharacters[c].Stats.Name;
                _turnOrder[c].Speed = _allSpawnedCharacters[c].Stats.BaseSpeed;
                _allSpawnedCharacters[c].InitTurn(_turnOrder[c]);
                Debug.Log(_allSpawnedCharacters[c].name + " injected");
            }

            _finishedInjecting = true;
        }

        /// <summary>
        /// Resets the turn order.
        /// </summary>
        private void ResetTurns()
        {
            for (int i = 0; i < _turnOrder.Count; i++)
            {
                if (_turnOrder[i] != null)
                {
                    if (i == 0)
                    {
                        _turnOrder[i].IsTurn = true;
                        _turnOrder[i].WasTurnPreviously = false;
                        _currentCharacter = _turnOrder[i].Character;
                        _previousCurrentCharacter = _turnOrder[i].Character;

                        if (_currentCharacter != null)
                        {
                            // if the character is an enemy, only display their turn if they're aggroed.
                            CharacterBattleController character = _currentCharacter.GetComponent<CharacterBattleController>();
                            if (character is EnemyBattleController)
                            {
                                EnemyBattleController enemy = character as EnemyBattleController;
                                if (enemy.IsAggroed)
                                {
                                    _currentCharacterText.text = character.Stats.Name + "'s Turn";
                                    _virtualCam.Follow = _currentCharacter.transform;
                                }
                            }

                            // if the character is a player, continue the tutorial once the movement part is complete.
                            if (character is PlayerBattleController)
                            {
                                if (TutorialIndex >= 1)
                                {
                                    TutorialIndex++;
                                    //Debug.Log(TutorialIndex);
                                }

                                _currentCharacterText.text = character.Stats.Name + "'s Turn";
                                _virtualCam.Follow = _currentCharacter.transform;
                            }
                        }

                        Debug.Log($"{_currentCharacter.name} is the new Selected Character.");
                    }
                    else
                    {
                        _turnOrder[i].IsTurn = false;
                        _turnOrder[i].WasTurnPreviously = false;
                    }
                }
            }

            _roundCounter++;
        }

        /// <summary>
        /// Updates the turn order.
        /// </summary>
        private void UpdateTurns()
        {
            for (int i = 0; i < _turnOrder.Count; i++)
            {
                if (_turnOrder[i] != null)
                {
                    if (!_turnOrder[i].WasTurnPreviously)
                    {
                        _turnOrder[i].IsTurn = true;
                        _currentCharacter = _turnOrder[i].Character;

                        if (_previousCurrentCharacter != _currentCharacter)
                        {
                            Debug.Log($"{_previousCurrentCharacter} was the last Selected Character.\n" +
                                      $"{_currentCharacter.name} is the new Selected Character.");

                            _previousCurrentCharacter = _currentCharacter;

                            if (_currentCharacter != null)
                            {
                                // if the character is an enemy, only display their turn if they're aggroed.
                                CharacterBattleController character = _currentCharacter.GetComponent<CharacterBattleController>();
                                if (character is EnemyBattleController)
                                {
                                    EnemyBattleController enemy = character as EnemyBattleController;
                                    if (enemy.IsAggroed)
                                    {
                                        _currentCharacterText.text = character.Stats.Name + "'s Turn";
                                        _virtualCam.Follow = _currentCharacter.transform;
                                    } 
                                }

                                // if the character is a player, continue the tutorial once the movement part is complete.
                                if (character is PlayerBattleController)
                                {
                                    if (TutorialIndex >= 1)
                                    {
                                        TutorialIndex++;
                                        //Debug.Log(TutorialIndex);
                                    }

                                    _currentCharacterText.text = character.Stats.Name + "'s Turn";
                                    _virtualCam.Follow = _currentCharacter.transform;
                                }
                            }

                            _turnCounter++;
                        }

                        break;
                    }
                    else if (i == _turnOrder.Count - 1 && _turnOrder[i].WasTurnPreviously)
                    {
                        ResetTurns();
                    }
                }
            }
        }

        /// <summary>
        ///  Removes given character from the turn order.
        /// </summary>
        /// <param name="character"></param>
        public void RemoveFromTurnOrder(CharacterBattleController character)
        {
            for (int i = 0; i < _turnOrder.Count; i++)
            {
                if (character.gameObject == _turnOrder[i].Character)
                {
                    _turnOrder.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes the character from the respective party.
        /// </summary>
        /// <param name="character"></param>
        public void RemoveFromParty(CharacterBattleController character)
        {
            if (character.tag == "Player")
                _playerParty.Remove(character);
            else if (character.tag == "Enemy")
                _enemyParty.Remove(character);
        }

        #endregion

        #region HELPERS

        public List<Turn> TurnOrder { get => _turnOrder; }

        public int RoundCount
        { get => _roundCounter; }

        public List<CharacterBattleController> PlayerParty
        { get => _playerParty; }

        public List<CharacterBattleController> EnemyParty
        { get => _enemyParty; }

        public bool FinishedInjecting
        { get => _finishedInjecting; }

        public GameObject CurrentCharacterObject
        { get => _currentCharacter; }

        public CharacterBattleController CurrentCharacter
        { get => _currentCharacter.GetComponent<CharacterBattleController>(); }

        public MinigameStateMachine[] CurrentCharacterAttackList
        { get => CurrentCharacter.SkillList; }

        // battle menu
        public bool PressedSkillOptionInMenu
        { get => _battleMenu.PressedSkillOption; }

        public bool PressedMoveOptionInMenu
        { get => _battleMenu.PressedMoveOption; }

        public bool PressedEndTurnOptionInMenu
        { get => _battleMenu.PressedEndTurnOption; }

        public bool PressedCancelMove
        { get => _battleMenu.PressedCancel; }

        /// <summary>
        /// Toggles the Battle Menu on or off.
        /// </summary>
        /// <param name="onOrOff"></param>
        public void ToggleBattleMenu(bool onOrOff)
        {
            _battleMenu.gameObject.SetActive(onOrOff);

            if (onOrOff == true)
            {
                _battleMenu.InitMenuSelections();
            }
        }

        public void SetCameraFollow(Transform newFollow)
        {
            _virtualCam.Follow = newFollow;
        }

        // skill menu
        public int SkillMenuSelection
        { get => _skillMenu.MenuIndex; }

        public bool PressedSelectSkill
        { get => _skillMenu.PressedSelect; }

        public bool PressedCancelSkill
        { get => _skillMenu.PressedCancel; }

        // grid
        public WorldGrid BattleGrid
        { get => _battlefieldGrid; }

        public Pathfinding BattlePathfinding
        { get => _battlefieldPathfinding; }

        public int TutorialIndex
        {
            get => _battleMenu.TutorialIndex;
            set
            {
                _battleMenu.TutorialIndex = value;
                _battleMenu.ChangeTutorialDisplay();
            }
        }

        /// <summary>
        /// Toggles the Skill Menu on or off.
        /// </summary>
        /// <param name="onOrOff"></param>
        public void ToggleSkillMenu(bool onOrOff)
        {
            _skillMenu.gameObject.SetActive(onOrOff);

            if (onOrOff == true)
            {
                _skillMenu.InitMenuSelections();
            }
        }

        /// <summary>
        /// Toggles the Energy Alert Panel on or off.
        /// </summary>
        /// <param name="onOrOff"></param>
        public void ToggleEnergyAlertPanel(bool onOrOff)
        {
            _skillMenu.ToggleEnergyAlertPanel(onOrOff);
        }

        /// <summary>
        /// Sorts the turn order by speed.
        /// </summary>
        public void SortBySpeed()
        {
            _turnOrder.Sort((a, b) =>
            {
                // store the speeds
                var speedA = a.Speed;
                var speedB = b.Speed;
            
                // sort the speeds
                return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
            });
        }

        #endregion
    }
}