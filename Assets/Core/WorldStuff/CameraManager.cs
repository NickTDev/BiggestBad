using Cinemachine;
using T02.TurnBasedSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace T02
{
    public class CameraManager : MonoBehaviour
    {
        #region CONTROLS

        [SerializeField] private PlayerControlsReference _controlsRef;
        private PlayerControls _controls;

        private InputAction _rightStickInput;
        private Vector2 _rightStickMovement;
        private InputAction _arrowsInput;
        private Vector2 _arrowsMovement;

        private InputAction _zoomInInput;
        private InputAction _zoomOutInput;

        private InputAction _resetFollowInput;

        #endregion

        #region VARIABLES

        public Camera mainCamera;
        bool isBattleCamera; //True if free roam battle camera, false if locked world camera

        float zoomLevel; //current fov
        public float zoomSensitivity; //How fast it zooms
        public float maxZoom; //Max y level
        public float minZoom; //Min y level

        public float maxXBound;
        public float minXBound;
        public float maxZBound;
        public float minZBound;

        public float worldZoom; //Fov when not in battle
        public GameObject worldPlayer;

        public float moveSensitivity;
        private CinemachineVirtualCamera _virtualCam; // the cinemachine camera
        private CinemachineBrain _brainCam; // the cinemachine brain

        // tracking positions
        private Vector3 _lastPos;

        #endregion

        private void Awake()
        {
            _controls = _controlsRef.Controls;

            _rightStickInput = _controls.PlayerCamera.RightStick;
            _rightStickInput.performed += ctx => _rightStickMovement = ctx.ReadValue<Vector2>();

            _arrowsInput = _controls.PlayerCamera.ArrowKeys;
            _arrowsInput.performed += ctx => _arrowsMovement = ctx.ReadValue<Vector2>();

            _zoomInInput = _controls.PlayerCamera.ZoomIn;
            _zoomOutInput = _controls.PlayerCamera.ZoomOut;

            _resetFollowInput = _controls.PlayerCamera.ResetFollow;
        }

        private void Start()
        {
            isBattleCamera = false;
            _virtualCam = mainCamera.GetComponent<CinemachineVirtualCamera>();
            _brainCam = mainCamera.GetComponent<CinemachineBrain>();
            _virtualCam.Follow = worldPlayer.transform;
            _virtualCam.m_Lens.FieldOfView = worldZoom;
        }

        void LateUpdate()
        {
            if (isBattleCamera)
            {
                Vector3 newPos;
                if (Input.mouseScrollDelta.y > 0 || _zoomInInput.IsPressed()) //Zoom in
                {
                    if (_virtualCam.Follow != null) _virtualCam.Follow = null;

                    float move = Input.mouseScrollDelta.y * zoomSensitivity * Time.fixedDeltaTime;

                    if (_zoomInInput.IsPressed())
                        move += zoomSensitivity * Time.fixedDeltaTime;

                    newPos = new Vector3(_virtualCam.transform.position.x,
                                         _virtualCam.transform.position.y - move,
                                         _virtualCam.transform.position.z + move);
                }
                else if (Input.mouseScrollDelta.y < 0 || _zoomOutInput.IsPressed()) //Zoom out
                {
                    if (_virtualCam.Follow != null) _virtualCam.Follow = null;

                    float move = Input.mouseScrollDelta.y * zoomSensitivity * Time.fixedDeltaTime;

                    if (_zoomOutInput.IsPressed())
                        move -= zoomSensitivity * Time.fixedDeltaTime;

                    newPos = new Vector3(_virtualCam.transform.position.x,
                                         _virtualCam.transform.position.y - move,
                                         _virtualCam.transform.position.z + move);
                }
                else
                {
                    newPos = new Vector3(_virtualCam.transform.position.x,
                                         _virtualCam.transform.position.y,
                                         _virtualCam.transform.position.z);
                }
                if (newPos.y > minZoom && newPos.y < maxZoom)
                    _virtualCam.transform.position = newPos;

                if (_resetFollowInput.triggered)
                {
                    _virtualCam.Follow = FindObjectOfType<TurnBasedManager>().CurrentCharacterObject.transform;
                }

                MoveCamera();
            }
        }

        private void OnEnable()
        {
            _controls?.Enable();
        }

        private void OnDisable()
        {
            _controls?.Disable();
        }

        public void MoveCamera()
        {
            Vector2 rsMoveNormalized = _rightStickMovement.normalized;
            Vector2 arrowMoveNormalized = _arrowsMovement.normalized;

            if (_rightStickMovement.y >= 0.7f || arrowMoveNormalized.y >= 1) // up
            {
                if (_virtualCam.Follow != null) _virtualCam.Follow = null;

                if (mainCamera.transform.position.z < maxZBound)
                    mainCamera.transform.position += Vector3.forward * Time.deltaTime * moveSensitivity;
            }
            if (_rightStickMovement.y <= -0.7f || arrowMoveNormalized.y <= -1) // down
            {
                if (_virtualCam.Follow != null) _virtualCam.Follow = null;

                if (mainCamera.transform.position.z > minZBound)
                    mainCamera.transform.position += Vector3.back * Time.deltaTime * moveSensitivity;
            }
            if (_rightStickMovement.x <= -0.7f || arrowMoveNormalized.x <= -1) // left
            {
                if (_virtualCam.Follow != null) _virtualCam.Follow = null;

                if (mainCamera.transform.position.x > minXBound)
                    mainCamera.transform.position += Vector3.left * Time.deltaTime * moveSensitivity;
            }
            if (_rightStickMovement.x >= 0.7f || arrowMoveNormalized.x >= 1) // right
            {
                if (_virtualCam.Follow != null) _virtualCam.Follow = null;

                if (mainCamera.transform.position.x < maxXBound)
                    mainCamera.transform.position += Vector3.right * Time.deltaTime * moveSensitivity;
            }
        }

        public void swapCameraType()
        {
            if (isBattleCamera)
            {
                isBattleCamera = false;
                zoomLevel = worldZoom;
                _virtualCam.m_Lens.FieldOfView = worldZoom;
                _virtualCam.Follow = worldPlayer.transform;
            }
            else
            {
                _virtualCam.Follow = null;
                isBattleCamera = true;
            }
        }
    }
}