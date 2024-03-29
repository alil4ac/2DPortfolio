using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    void Enter(T Send);

    void HandleInput(T Send);

    void LogicUpdate(T Send);

    void PhysicsUpdate(T Send);

    void Exit(T Send);
}
