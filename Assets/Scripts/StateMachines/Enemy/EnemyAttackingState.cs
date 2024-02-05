using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossfadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossfadeDuration);
        stateMachine.Weapon.SetDamage(stateMachine.AttackDamage, stateMachine.Knockback);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }

}
