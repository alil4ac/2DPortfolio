using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public IState<T> CurrentState { get; private set; }

    private T m_Send;

    public StateMachine(T Send, IState<T> State)
    {
        m_Send = Send;

        Initialze(State);
    }

    public void Initialze(IState<T> StartState)
    {
        CurrentState = StartState;

        StartState.Enter(m_Send);
    }

    public void ChangeState(IState<T> NewState)
    {
        CurrentState.Exit(m_Send);

        CurrentState = NewState;

        CurrentState.Enter(m_Send);
    }

    public void HandleInput()
    {
        CurrentState.HandleInput(m_Send);
    }

    public void LogicUpdate()
    {
        CurrentState.LogicUpdate(m_Send);
    }

    public void PhysicsUpdate()
    {
        CurrentState.PhysicsUpdate(m_Send);
    }
}
