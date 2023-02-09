using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
	private List<Sprite> _frames;
	private SpriteRenderer _spriteRenderer;
	private float _framerate; 

	private int _currentFrame; 
	private float _timer; 

	// 0.16f ~ 1/60
	public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float framerate = 0.16f)
    {
		_frames = frames;
		_spriteRenderer = spriteRenderer;
		_framerate = framerate;
    }

	public void Start()
    {
		_currentFrame = 0;
		_timer = 0f;
		_spriteRenderer.sprite = _frames[0];
    }

	public void HandleUpdate()
    {
		_timer += Time.deltaTime;
		if (_timer > _framerate)
        {
			_currentFrame++;
			if (_currentFrame >= _frames.Count)
            {
				_currentFrame = 0; 
            }

			_spriteRenderer.sprite = _frames[_currentFrame];
			_timer = 0f;
        }
    }
}