using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{    
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    private List<Target> targets = new List<Target>();
    private Camera mainCamera;

    public Target CurrentTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

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

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);
       
            //if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            //    continue;

            if (!target.GetComponentInChildren<Renderer>().isVisible)
                continue;

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if(toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null)
            return false;

        CurrentTarget = closestTarget;
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
