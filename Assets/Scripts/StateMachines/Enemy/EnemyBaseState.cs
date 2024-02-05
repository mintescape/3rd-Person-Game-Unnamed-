using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
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

    protected void FacePlayer()
    {
        if (stateMachine.Player != null)
        {
            Vector3 targetDirection = stateMachine.Player.transform.position - stateMachine.transform.position;
            targetDirection.y = 0;
            stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
            //stateMachine.transform.LookAt(stateMachine.Targeter.CurrentTarget.transform);
        }
    }

    protected bool IsInChaseRange()
    {
        float distToPlayerSqr = Vector3.SqrMagnitude(stateMachine.Player.transform.position - stateMachine.transform.position);

        return distToPlayerSqr <= stateMachine.PlayerDetectionRange * stateMachine.PlayerDetectionRange;        
    }
    protected bool IsInAttackRange()
    {
        float distToPlayerSqr = Vector3.SqrMagnitude(stateMachine.Player.transform.position - stateMachine.transform.position);

        return distToPlayerSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
