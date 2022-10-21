using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    // Enter
    public abstract void Enter();

    // Tick
    public abstract void Tick(float deltaTime);

    // Exit
    public abstract void Exit();
}
