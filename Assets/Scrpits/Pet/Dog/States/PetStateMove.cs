using UnityEngine;

public class PetStateMove : PetState
{
    private float _stopDistance = 0.3f;
    private float _minSpeed = 2f;
    private float _speedPerMeter = 0.8f;

    private int _animHashSpeed;

    public PetStateMove(PetController petController, Animator petAnimator) : base(petController, petAnimator) { }

    protected override void Init()
    {
        _animHashSpeed = Animator.StringToHash("Speed");
    }

    public override void Enter()
    {
        PetAnimator.SetFloat(_animHashSpeed, _minSpeed);
    }

    public override void Update()
    {
        Vector3 targetPos = PetController.MoveTransform.position;
        Vector3 curPos = PetController.transform.position;
        Vector3 dir = (targetPos - curPos).normalized;
        float dist = Vector3.Distance(curPos, targetPos);

        if (dist <= _stopDistance)
        {
            PetController.ChangeState(PetStates.Idle);
            return;
        }

        PetController.transform.rotation = Quaternion.LookRotation(dir);

        float rawSpeed = dist * _speedPerMeter;
        float speed = Mathf.Max(_minSpeed, rawSpeed);

        Vector3 newHeight = PetController.transform.position;
        newHeight.y = targetPos.y;
        PetController.transform.position = newHeight;

        PetController.transform.Translate(dir * (speed * Time.deltaTime), Space.World);

        PetAnimator.SetFloat(_animHashSpeed, speed);
    }

    public override void Exit()
    {
        PetAnimator.SetFloat(_animHashSpeed, 0f);
    }
}