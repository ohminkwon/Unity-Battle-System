using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // Input Manage
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    // For Basic Move State
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }

    // For Target State
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public float TargetingMoveSpeed { get; private set; }

    // For Attack State
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    // For Dodge Action
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float DodgeCooldown { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }

    // ETC
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerMoveState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += Health_OnTakeDamage;
        Health.OnDie += Health_OnDie;
    }
    private void OnDisable()
    {
        Health.OnTakeDamage -= Health_OnTakeDamage;
        Health.OnDie -= Health_OnDie;
    }
    private void Health_OnTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    private void Health_OnDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void SetDodgeTime(float dodgetime)
    {
        PreviousDodgeTime = dodgetime;
    }
}
