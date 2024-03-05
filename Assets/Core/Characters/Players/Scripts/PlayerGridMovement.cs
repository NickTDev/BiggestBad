// Merle Roji

using System.Collections;
using System.Collections.Generic;
using T02;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls Player physics logic in the overworld. Supports Grid-Based movement.
/// </summary>
public class PlayerGridMovement : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private float _baseMoveSpeed = 1f;

    private bool _isMoving = false;
    private Vector2 _movement;
    private Animator _anim;
    private SpriteRenderer _sprite;

    #endregion

    #region CONTROLS

    private PlayerControls _controls;
    private InputAction _moveInput;
    private Vector3 toMove;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        _isMoving = false;
        _anim = GetComponent<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        InitControls();
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, toMove, _baseMoveSpeed * Time.deltaTime);
            if ((toMove - transform.position).sqrMagnitude < Mathf.Epsilon)
            {
                transform.position = toMove;
                AnimationQoL.ChangeAnimation(_anim, "idle");
                _isMoving = false;
            }
        }
    }

    private void OnEnable()
    {
        _controls?.Enable();
    }

    private void OnDisable()
    {
        _controls?.Enable();
    }

    #endregion

    #region OVERWORLD PHYSICS METHODS

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void InitControls()
    {
        // setup new controls
        _controls = new PlayerControls();
        InputSystem.pollingFrequency = 180f;

        // initialize all controls
        _moveInput = _controls.PlayerOverworld.Move;
        _moveInput.performed += ctx => _movement = ctx.ReadValue<Vector2>();
    }

    /// <summary>
    /// Checks input from the player, whether it be from gamepad or keyboard.
    /// </summary>
    private void CheckInput()
    {
        // grid movement
        if (!_isMoving)
        {
            if (_movement != Vector2.zero)
            {
                var targetPos = transform.position; // init target position with own position
                var normalizedMovement = _movement.normalized;

                if (normalizedMovement.x != 0 && _movement.y == 0)
                {
                    if (_movement.x < 0) //Checks if left is unwalkable
                    {
                        _sprite.flipX = true;

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.left, out hit, 1.0f))
                            if (hit.transform.tag == "Obstacle")
                                return;
                    }
                    else //Checks if right is unwalkable
                    {
                        _sprite.flipX = false;

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.right, out hit, 1.0f))
                            if (hit.transform.tag == "Obstacle")
                                return;
                    }
                    targetPos.x += _movement.x;
                }

                if (normalizedMovement.y != 0 && _movement.x == 0)
                {
                    if (_movement.y > 0) //Checks if up is unwalkable
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1.0f))
                            if (hit.transform.tag == "Obstacle")
                                return;
                    }
                    else //Checks if down is unwalkable
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, Vector3.back, out hit, 1.0f))
                            if (hit.transform.tag == "Obstacle")
                                return;
                    }
                    targetPos.z += _movement.y; // z instead of y because in 3D, z is forward
                }

                _isMoving = true;
                AnimationQoL.ChangeAnimation(_anim, "walk");
                //StartCoroutine(Move(targetPos));
                toMove = targetPos;
            }
        }

        //Debug.Log("X: " + _movement.x + ", Y: " + _movement.y);
    }

    /// <summary>
    /// Moves the player towards a target position.
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    IEnumerator Move(Vector3 targetPos)
    {
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _baseMoveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        _isMoving = false;
    }

    #endregion
}
