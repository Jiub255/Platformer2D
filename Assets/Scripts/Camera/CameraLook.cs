using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CameraFollow))]
public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private BoolSO _isGroundedSO;

	private CameraFollow _cameraFollow;

    private InputAction _lookAction;

    private void Awake()
    {
        _cameraFollow = GetComponent<CameraFollow>();
    }

    private void Start()
    {
        _lookAction = S.I.IM.PC.Gameplay.Look;

        _lookAction.started += OnPressLook;
        _lookAction.canceled += OnReleaseLook;
        PlayerJump.OnLeftGround += OnReleaseLook;
    }

    private void OnDisable()
    {
        _lookAction.started -= OnPressLook;
        _lookAction.canceled -= OnReleaseLook;
        PlayerJump.OnLeftGround -= OnReleaseLook;
    }

    private void OnPressLook(InputAction.CallbackContext obj)
    {
        if (_isGroundedSO.Value)
        {
            _cameraFollow.LookOffsetMod = _lookAction.ReadValue<float>();
        }
    }

    private void OnReleaseLook(InputAction.CallbackContext obj)
    {
        _cameraFollow.LookOffsetMod = 0f;
    }

    private void OnReleaseLook()
    {
        _cameraFollow.LookOffsetMod = 0f;
    }
}