using UnityEngine;

[System.Serializable]
public abstract class PetState
{
    protected PetController PetController;
    protected Animator PetAnimator;

    public PetState(PetController petController, Animator petAnimator)
    {
        PetController = petController;
        PetAnimator = petAnimator;
        Init();
    }

    protected abstract void Init();

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
