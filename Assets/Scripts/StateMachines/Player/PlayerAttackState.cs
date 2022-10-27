using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private float previousFrameTime;
    private Attack attack;

    // Constructor
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {       
        stateMachine.Animator.CrossFadeInFixedTime(attack.animationName, attack.transitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        RotatePlayerToTarget();

        float normalizedTime = GetNormalizedTime();
        if(normalizedTime > previousFrameTime && normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime); // TODO
            }
        }
        else
        {
            // TODO: back to locomotion
        }

        previousFrameTime = normalizedTime;
    }
    public override void Exit()
    {
        
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }       
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
}
