using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    // Constructor
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero ,deltaTime);
    }

    protected void RotatePlayerToTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null)
            return;

        Vector3 targetDir = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        targetDir.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(targetDir);
    }
}
