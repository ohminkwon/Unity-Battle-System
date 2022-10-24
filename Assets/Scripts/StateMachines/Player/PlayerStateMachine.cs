using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // Input Manager
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }

    public Transform mainCameraTransform { get; private set; }

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerMoveState(this));
    }
}
