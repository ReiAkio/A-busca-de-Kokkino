using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController2 _playerController;
    public string currentAnimation;

    public float AnimationSpeed = 1;
    public float speedMultiplier = 1;
    private void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController2>();
        _animator = gameObject.GetComponent<Animator>();
        _animator.Play("Walk_Left");
    }

    private void Update()
    {
        string nextAnimation = "";
        if (_playerController.IsWalking)
        {
            if (_playerController.HorizontalMovement > 0)
            {
                nextAnimation = "Walk_Right";
            }
            else
            {
                nextAnimation = "Walk_Left";
            }
        }else if (_playerController.IsIdle)
        {
            if (currentAnimation == "Walk_Right")
            {
                nextAnimation = "Idle_Right";
            }
            
            if (currentAnimation == "Walk_Left")
            {
                nextAnimation = "Idle_Left";
            }
        }

        AnimationSpeed = _playerController.CurrentSpeed * speedMultiplier;
        if (currentAnimation != nextAnimation)
        {
            _animator.Play(nextAnimation);
            _animator.speed = AnimationSpeed;
        }

        currentAnimation = nextAnimation;
    }
}
