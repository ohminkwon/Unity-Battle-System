using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    // For optimization
    private readonly int PULLUP_HASH = Animator.StringToHash("PullUp");
    private const float CROSS_FADE_TIME = 0.1f;
    
    private Vector3 offset = new Vector3(0f, 2.325f, 0.65f);

    // Constructor
    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {      
        stateMachine.Animator.CrossFadeInFixedTime(PULLUP_HASH, CROSS_FADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return;

        // Teleport character for animation transition from climb to idle
        // TODO : Need to use target matching 

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(offset, Space.Self);
        stateMachine.Controller.enabled = true;

        stateMachine.SwitchState(new PlayerMoveState(stateMachine, false));
    }
    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }   
}
