using UnityEngine;
using TMPro;

public class GroundedUI : MonoBehaviour
{
    [SerializeField]
	private TextMeshProUGUI _grounded, _lastJump, _lastGrounded;

	[SerializeField]
	private BoolSO _isGroundedSO;
    [SerializeField]
    private PlayerJump _playerJump;

    private void FixedUpdate()
    {
        _grounded.text = _isGroundedSO.Value ? "Grounded" : "Not Grounded"; 

        _lastJump.text = "Last Jump Time: " + _playerJump._lastJumpTime.ToString();

        _lastGrounded.text = "Last Grounded Time: " + _playerJump._lastGroundedTime.ToString();
    }
}