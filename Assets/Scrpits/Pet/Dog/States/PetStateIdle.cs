using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateIdle : PetState
{
    private int _sitAnimationHash;
    private float _countTime;
    private bool _isSit { get => PetAnimator.GetBool(_sitAnimationHash); }

    public PetStateIdle(PetController petController, Animator petAnimator) : base(petController, petAnimator)
    {
    }

    protected override void Init()
    {
        _sitAnimationHash = Animator.StringToHash("IsSit");
    }

    public override void Enter()
    {
        _countTime = PetController.IdleSitDelay;
    }

    public override void Update()
    {
        PetController.StateTransitions();

        if (PetController.CurrentState != this) return;

        if (_isSit) return;

        _countTime -= Time.deltaTime;
        if (_countTime <= 0)
        {
            PetAnimator.SetBool(_sitAnimationHash, true);
        }
    }

    public override void Exit()
    {
        PetAnimator.SetBool(_sitAnimationHash, false);
        Debug.Log(_isSit);
    }
}
