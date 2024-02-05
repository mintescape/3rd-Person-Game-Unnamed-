using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private const float crossfadeDuration = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, crossfadeDuration);
        stateMachine.InputReader.TargetEvent += OnTargetEnter;
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
                
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, stateMachine.AnimatorDampening, deltaTime);
            return;
        }

        FaceMovementDirection(movement, deltaTime);
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, stateMachine.AnimatorDampening, deltaTime);
        
    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTargetEnter;
        Debug.Log("Exit");
    }

    private Vector3 CalculateMovement()
    {
        Vector3 mvmt;
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        right.y = forward.y = 0;
        right.Normalize();
        forward.Normalize();

        mvmt = stateMachine.InputReader.MovementValue.x * right;
        mvmt += stateMachine.InputReader.MovementValue.y * forward;

        return mvmt;
    }

    void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), stateMachine.RotationDampening*deltaTime);
    }

    void OnTargetEnter()
    {
        if (!stateMachine.Targeter.SelectTarget()) return;

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}
