using UnityEngine;

public class SceneMusic : MonoBehaviour
{
	[SerializeField]
	private AudioClip _song;
    [SerializeField]
    private GEAudioClip _onPlaySong;

    private void Start()
    {
        _onPlaySong.Invoke(_song);
    }
}