using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 totalMovement = motion + stateMachine.ForceReciever.Movement;
        stateMachine.CharacterController.Move(totalMovement * deltaTime);
    }

    protected void FaceTarget()
    {
        if(stateMachine.Targeter.CurrentTarget != null)
        {
            Vector3 targetDirection = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
            targetDirection.y = 0;
            stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
            //stateMachine.transform.LookAt(stateMachine.Targeter.CurrentTarget.transform);
        }
    }

    protected void ReturnToLocomotion()
    {
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
}
