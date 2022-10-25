using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private List<Target> targets = new List<Target>();
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
            return;

        targets.Add(target);
        target.OnDestroyEvent += RemoveTarget;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
            return;

        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0)
            return false;

        CurrentTarget = targets[0];
        cinemachineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }
    public void Cancel()
    {
        if (CurrentTarget == null)
            return;

        cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyEvent -= RemoveTarget;
        targets.Remove(target);
    }

}
