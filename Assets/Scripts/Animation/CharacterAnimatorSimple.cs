using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorSimple : MonoBehaviour
{
	[SerializeField]
	private List<Sprite> sprites;

	private SpriteAnimation spriteAnimator;

    private void Start()
    {
        spriteAnimator = new SpriteAnimation(sprites, GetComponent<SpriteRenderer>());
        spriteAnimator.Start();
    }

    private void Update()
    {
        spriteAnimator.HandleUpdate();
    }
}