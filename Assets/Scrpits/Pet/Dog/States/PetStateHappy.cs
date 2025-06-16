using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateHappy : PetState
{
    private int _animationHash;
    private float _countTime;

    public PetStateHappy(PetController petController, Animator petAnimator) : base(petController, petAnimator)
    {
    }

    protected override void Init()
    {
        _animationHash = Animator.StringToHash("IsHappy");
    }

    public override void Enter()
    {
        PetAnimator.SetBool(_animationHash, true);
        _countTime = PetController.HappyDuration;
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
        PetController.IsHeadPet = false;
    }
}
