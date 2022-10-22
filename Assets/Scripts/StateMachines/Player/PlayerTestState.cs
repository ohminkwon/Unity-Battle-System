using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    // Constructor
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0;
        movement.z = stateMachine.InputReader.MovementValue.y;

        stateMachine.Controller.Move(movement * stateMachine.MoveSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
            return;

        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }
    public override void Exit()
    {    
    
    }
}
