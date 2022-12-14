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
        // Set & Activate Ragdoll
        enemyStateMachine.Ragdoll.ToggleRagdoll(true);

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
