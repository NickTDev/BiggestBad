// Merle Roji

using UnityEngine;
using UnityEngine.InputSystem;
using T02.TurnBasedSystem;

namespace T02.Characters.InBattle
{
    /// <summary>
    /// Controls the logic of a Player during battle.
    /// </summary>
    public class PlayerBattleController : CharacterBattleController
    {
        #region VARIABLES

        [Header("Player Battle Parameters")]
        [SerializeField] private GameObject _tileSelectorPrefab;
        private GameObject _tileSelector;
        [SerializeField] private Material _validTileMaterial;
        [SerializeField] private Material _invalidTileMaterial;

        [Header("Add the counter attack minigame in here.")]
        [SerializeField] private ShieldCounter _counterAttackPrefab;

        #endregion

        #region CONTROLS

        [SerializeField] private PlayerControlsReference _controlsReference;
        private PlayerControls _controls;
        private InputAction _player1Action;
        private InputAction _player2Action;
        private InputAction _player3Action;
        private InputAction _player4Action;

        private InputAction _targetConfirm;
        private InputAction _targetCancel;

        private InputAction _leftStickInput;
        private InputAction _dpadInput;
        private Vector2 _leftStickMovement;
        private Vector2 _dpadMovement;
        private bool _pressedMove = false;

        #endregion

        #region UNITY METHODS

        protected override void Awake()
        {
            base.Awake();

            InitControls();
        }

        private void OnDisable()
        {
            _controls?.Disable();
        }

        #endregion

        #region INIT

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        private void InitControls()
        {
            // setup new controls
            _controls = _controlsReference.Controls;

            // initialize controls
            _player1Action = _controls.PlayerBattle.Player1Action;
            _player2Action = _controls.PlayerBattle.Player2Action;
            _player3Action = _controls.PlayerBattle.Player3Action;
            _player4Action = _controls.PlayerBattle.Player4Action;

            _targetConfirm = _controls.PlayerBattle.TargetConfirm;
            _targetCancel = _controls.PlayerBattle.TargetCancel;

            // movement
            _leftStickInput = _controls.PlayerBattle.LeftJoystick;
            _leftStickInput.performed += ctx => _leftStickMovement = ctx.ReadValue<Vector2>();
            _dpadInput = _controls.PlayerBattle.Movement;
            _dpadInput.performed += ctx => _dpadMovement = ctx.ReadValue<Vector2>();
            _pressedMove = false;
        }

        #endregion

        #region PLAYER BATTLE METHODS

        /// <summary>
        /// To be called when the minigame needs to be started up.
        /// </summary>
        public override void StartMinigame()
        {
            base.StartMinigame();

            MinigameStateMachine firstAttack = Instantiate(SkillList[_turnManager.SkillMenuSelection], this.transform.position, Quaternion.identity);
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

        /// <summary>
        /// To be called when the counter minigame must begin.
        /// </summary>
        public void StartCounter(EnemyBattleController engagedEnemy)
        {
            ShieldCounter counterAttack = Instantiate(_counterAttackPrefab, Vector3.zero, Quaternion.identity);
            counterAttack.InjectCharacter(this);
            counterAttack.EngagedEnemy = engagedEnemy;
        }

        public override void CheckForTargets()
        {
            SkillList[_turnManager.SkillMenuSelection].CheckForTargets(this);
        }

        /// <summary>
        /// Spawns the Tile Selector object.
        /// </summary>
        public void SpawnTileSelector()
        {
            if (_tileSelector == null)
            {
                //Instantiate(Resources.Load("PlayerTarget"), player.transform.position + Vector3.right + (Vector3.down * 0.4f), Quaternion.identity);
                _tileSelector = Instantiate(_tileSelectorPrefab, transform.position + (Vector3.down * 0.4f), Quaternion.identity);
            }
        }

        /// <summary>
        /// Destroys the Tile Selector object.
        /// </summary>
        public void DestroyTileSelector()
        {
            if (_tileSelector != null )
            {
                Destroy(_tileSelector.gameObject);
            }
        }

        #endregion

        #region HELPERS

        // south
        public bool Player1ActionPressed
        { get => _player1Action.triggered; }

        public bool Player1ActionHeld
        { get => _player1Action.IsPressed(); }

        public bool Player1ActionReleased
        { get => _player1Action.WasReleasedThisFrame(); }

        // east
        public bool Player2ActionPressed
        { get => _player2Action.triggered; }

        public bool Player2ActionHeld
        { get => _player2Action.IsPressed(); }

        public bool Player2ActionReleased
        { get => _player2Action.WasReleasedThisFrame(); }

        // west
        public bool Player3ActionPressed
        { get => _player3Action.triggered; }

        public bool Player3ActionHeld
        { get => _player3Action.IsPressed(); }

        public bool Player3ActionReleased
        { get => _player3Action.WasReleasedThisFrame(); }

        // north
        public bool Player4ActionPressed
        { get => _player4Action.triggered; }

        public bool Player4ActionHeld
        { get => _player4Action.IsPressed(); }

        public bool Player4ActionReleased
        { get => _player4Action.WasReleasedThisFrame(); }

        // left stick
        public Vector2 LeftStickMovement
        { get => _leftStickMovement; }

        public Vector2 DPadMovement
        { get => _dpadMovement; }

        public bool PressedMove
        {
            get => _pressedMove;
            set => _pressedMove = value;
        }

        public bool IsPlayerUsingGamepad
        {
            get
            {
                bool usingGamepad = Gamepad.all.Count > 0;
                return usingGamepad;
            }
        }

        public bool TargetConfirmPressed
        { get => _targetConfirm.triggered; }

        public bool TargetCancelPressed
        { get => _targetCancel.triggered; }

        public GameObject TileSelector
        { get => _tileSelector; }

        /// <summary>
        /// Checks to see if the tile is valid, then changes the material.
        /// </summary>
        /// <param name="isValid"></param>
        public void ValidTileColoring(bool isValid)
        {
            if (_tileSelector == null) return;

            // get renderer
            Renderer tileRenderer = _tileSelector.GetComponent<Renderer>();

            if (isValid) // change to valid material if valid
            {
                if (tileRenderer.material != _validTileMaterial)
                    tileRenderer.material = _validTileMaterial;
            }
            else // change to invalid tile material if invalid
            {
                if (tileRenderer.material != _invalidTileMaterial)
                    tileRenderer.material = _invalidTileMaterial;
            }
        }

        /// <summary>
        /// Enables the player battle controls.
        /// </summary>
        public void EnableBattleControls()
        {
            _controls?.PlayerBattle.Enable();
        }

        /// <summary>
        /// Disables the player battle controls.
        /// </summary>
        public void DisableBattleControls()
        {
            _controls?.PlayerBattle.Disable();
        }

        /// <summary>
        /// Disables the menu controls.
        /// </summary>
        public void EnableMenuControls()
        {
            _controls?.PlayerMenu.Enable();
        }

        /// <summary>
        /// Disables the menu controls.
        /// </summary>
        public void DisableMenuControls()
        {
            _controls?.PlayerMenu.Disable();
        }

        #endregion
    }
}
