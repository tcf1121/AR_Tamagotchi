using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateEat : PetState
{
    private int _animationHash;
    private float _countTime;

    public PetStateEat(PetController petController, Animator petAnimator) : base(petController, petAnimator)
    {
    }

    protected override void Init()
    {
        _animationHash = Animator.StringToHash("IsEat");
    }

    public override void Enter()
    {
        PetAnimator.SetBool(_animationHash, true);
        _countTime = PetController.Food.EatDuration;
    }

    public override void Update()
    {
        _countTime -= Time.deltaTime;
        if (_countTime <= 0)
        {
            PetController.ConsumeFood();
            PetController.ChangeState(PetStates.Idle);
        }
    }

    public override void Exit()
    {
        PetAnimator.SetBool(_animationHash, false);
    }
}
