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
        // Set & Activate Ragdoll
        stateMachine.Ragdoll.ToggleRagdoll(true);

        stateMachine.Weapon.gameObject.SetActive(false);
    }
    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }  
}
