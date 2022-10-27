using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private Attack attack;

    // Constructor
    public PlayerAttackState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        Debug.Log(attack.animationName);
        stateMachine.Animator.CrossFadeInFixedTime(attack.animationName, 0.1f);
    }
    public override void Tick(float deltaTime)
    {
        //Debug.Log("TICK: Attack State");        
    }
    public override void Exit()
    {
        
    }    
}
