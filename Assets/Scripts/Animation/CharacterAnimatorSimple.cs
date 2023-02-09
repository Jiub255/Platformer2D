using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorSimple : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> sprites;

    [SerializeField]
    private float _timePerFrame = 0.16f; 

	private SpriteAnimation spriteAnimator;

    private void Start()
    {
        spriteAnimator = new SpriteAnimation(sprites, GetComponent<SpriteRenderer>(), _timePerFrame);
        spriteAnimator.Start();
    }

    private void Update()
    {
        spriteAnimator.HandleUpdate();
    }
}