using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    // Constructor
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        // TODO: Set & Activate Ragdoll

        stateMachine.Weapon.gameObject.SetActive(false);
    }
    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }  
}
