using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    // Constructor
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        // TODO: Set & Activate Ragdoll

        enemyStateMachine.Weapon.gameObject.SetActive(false);

        GameObject.Destroy(enemyStateMachine.Target);     
    }
    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }
}
