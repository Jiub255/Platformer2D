using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SlimeMovement : MonoBehaviour
{
	[SerializeField]
	private float _hopForce = 5f;

	[SerializeField]
	private float _timeBetweenHops = 2f;
	private float _timer;

	private Rigidbody2D _rb;
    private Transform _player;

    [SerializeField]
    private AudioClip _hopClip;
    [SerializeField]
    private GEAudioClip _onPlayHopSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
		_timer += Time.deltaTime;
		if (_timer > _timeBetweenHops)
        {
			_timer = 0;
            // The math just makes it so the slime always hops toward the player. 
            _rb.AddForce(new Vector2(Mathf.Sign(_player.position.x - transform.position.x) * _hopForce, 2 * _hopForce), ForceMode2D.Impulse);
        }
    }
}