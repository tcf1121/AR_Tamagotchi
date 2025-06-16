using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateAngry : PetState
{
    private int _animationHash;
    private float _countTime;

    public PetStateAngry(PetController petController, Animator petAnimator) : base(petController, petAnimator)
    {
    }

    protected override void Init()
    {
        _animationHash = Animator.StringToHash("IsAngry");
    }

    public override void Enter()
    {
        PetAnimator.SetBool(_animationHash, true);
        _countTime = PetController.AngryDuration;
    }

    public override void Update()
    {
        _countTime -= Time.deltaTime;
        if (_countTime <= 0)
        {
            PetController.ChangeState(PetStates.Idle);
        }
    }

    public override void Exit()
    {
        PetAnimator.SetBool(_animationHash, false);
        PetController.IsTailPet = false;
    }
}
