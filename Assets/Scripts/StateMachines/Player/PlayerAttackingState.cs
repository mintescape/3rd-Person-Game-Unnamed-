using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;
    private float previousFrameTime;
    private bool alreadyAppliedForce = false;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine)
    {
        this.attack = stateMachine.Attacks[AttackIndex]; //fetch the data of the attack that called this state
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration); //blend into a play attack's animation
        stateMachine.InputReader.IsAttacking = false;
        stateMachine.WeaponDamage.SetDamage(attack.Damage, attack.Knockback);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator);
        if(normalizedTime < 1)
        {
            if(normalizedTime >= attack.ForceTime)
            {
                TryAttackForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
            
        }
        else
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComboStateIndex == -1) { return; }

        if(normalizedTime < attack.ComboAttackTime)
        {
            return;
        }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private void TryAttackForce()
    {
        if (alreadyAppliedForce) return;
        stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }
}
