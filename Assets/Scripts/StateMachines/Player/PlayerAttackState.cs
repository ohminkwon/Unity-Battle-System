using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    // Constructor
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        
    }
    public override void Tick(float deltaTime)
    {
        Debug.Log("Attack State");
    }
    public override void Exit()
    {
        
    }    
}
