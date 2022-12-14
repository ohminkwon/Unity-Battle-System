using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    // For Enemy Move & Chase
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }  
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }

    // For Enemy Attack
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int KnockbackForce { get; private set; }

    // ETC
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    public Health Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
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
        SwitchState(new EnemyImpactState(this));
    }
    private void Health_OnDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    // Check radius as a gizmo on window editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
