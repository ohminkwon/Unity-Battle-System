using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;

    // Constructor
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {       
        stateMachine.Animator.CrossFadeInFixedTime(attack.animationName, attack.transitionDuration);

        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        RotatePlayerToTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");
        if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)            
                TryApplyForce();            

            if (stateMachine.InputReader.IsAttacking)            
                TryComboAttack(normalizedTime); 
            
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            else
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        previousFrameTime = normalizedTime;
    }
    public override void Exit()
    {
        
    }    

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.comboStateIndex == -1)
            return;

        if (normalizedTime < attack.comboAttackTime)
            return;

        stateMachine.SwitchState
        (
            new PlayerAttackState
            (
                stateMachine, 
                attack.comboStateIndex
            )
        );
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce)
            return;

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }
}
