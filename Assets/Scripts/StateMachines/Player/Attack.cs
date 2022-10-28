using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack 
{
    [field: SerializeField] public string animationName { get; private set; }
    [field: SerializeField] public float transitionDuration { get; private set; }
    [field: SerializeField] public int comboStateIndex { get; private set; } = -1;
    [field: SerializeField] public float comboAttackTime { get; private set; }

    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
}
