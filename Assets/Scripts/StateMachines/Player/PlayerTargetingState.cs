using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    private const float crossfadeDuration = 0.1f;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, crossfadeDuration);
        stateMachine.InputReader.TargetEvent += OnTargetExit;
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        FaceTarget();
        UpdateAnimator(deltaTime);
    }

    private void UpdateAnimator(float deltaTime)
    {
        float forward = Mathf.Round(stateMachine.InputReader.MovementValue.y);
        float right = Mathf.Round(stateMachine.InputReader.MovementValue.x);
        stateMachine.Animator.SetFloat(TargetingForwardHash, forward, stateMachine.AnimatorDampening, deltaTime);
        stateMachine.Animator.SetFloat(TargetingRightHash, right, stateMachine.AnimatorDampening, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTargetExit;
        Debug.Log("Leaving targeting");
    }

    void OnTargetExit()
    {
        stateMachine.Targeter.ExitTargeting();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 mvmt = new Vector3();

        mvmt += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        mvmt += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return mvmt;
    }

}
