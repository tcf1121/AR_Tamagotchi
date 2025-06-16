using System;
using UnityEngine;
using System.Collections.Generic;

public class PetController : MonoBehaviour
{
    [field: SerializeField] public Animator PetAnimator { get; private set; }

    private Dictionary<PetStates, PetState> _states = new();
    public PetState CurrentState { get; private set; }
    [SerializeField] private LayerMask _foodLayer;

    [field: SerializeField] public float AngryDuration { get; private set; }
    [field: SerializeField] public float HappyDuration { get; private set; }
    [field: SerializeField] public float IdleSitDelay { get; private set; }

    public FoodController Food { get; set; }
    public Transform MoveTransform { get; set; }
    public bool IsStateIdle { get => CurrentState == _states[PetStates.Idle]; }

    public bool IsHeadPet { get; set; }
    public bool IsTailPet { get; set; }

    private void Awake() => Init();
    private void Update() => HandleControl();
    private void OnTriggerEnter(Collider other)
    {
        if (_foodLayer == (1 << other.gameObject.layer))
        {
            Food = ComponentRegistry
                .GetAs<FoodController>(other.gameObject);
        }
    }

    public void ChangeState(PetStates state)
    {
        CurrentState.Exit();
        CurrentState = _states[state];
        CurrentState.Enter();
    }

    public void ConsumeFood()
    {
        Food.ConsumeFood();
        Food = null;
    }

    private void HandleControl()
    {
        CurrentState.Update();
    }

    public void StateTransitions()
    {
        if (Food != null)
        {
            float foodDist = Vector3.Distance(transform.position, Food.transform.position);

            if (foodDist > 0.4f)
            {
                MoveTransform = Food.transform;
                ChangeState(PetStates.Move);
            }
            else
            {
                ChangeState(PetStates.Eat);
            }
            return;
        }

        if (MoveTransform != null &&
            Vector3.Distance(transform.position, MoveTransform.position) > 1f)
        {
            ChangeState(PetStates.Move);
            return;
        }

        if (IsHeadPet)
        {
            ChangeState(PetStates.Happy);
            return;
        }

        if (IsTailPet)
        {
            ChangeState(PetStates.Angry);
        }
    }

    private void Init()
    {
        _states.Add(PetStates.Idle, new PetStateIdle(this, PetAnimator));
        _states.Add(PetStates.Move, new PetStateMove(this, PetAnimator));
        _states.Add(PetStates.Eat, new PetStateEat(this, PetAnimator));
        _states.Add(PetStates.Happy, new PetStateHappy(this, PetAnimator));
        _states.Add(PetStates.Angry, new PetStateAngry(this, PetAnimator));

        CurrentState = _states[PetStates.Idle];
        CurrentState.Enter();
    }
}
