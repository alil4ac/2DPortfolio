using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : IState<PlayerController>
{
    public void Enter(PlayerController Send)
    {

    }

    public void Exit(PlayerController Send)
    {

    }

    public void HandleInput(PlayerController Send)
    {
        if (!CommonCollection.CrouchBtn) { Send.ChangeState(PlayerController.EState.Movement); }
            
        if (CommonCollection.AttackBtn) { Send.ChangeState(PlayerController.EState.Attack); }
            
        if (CommonCollection.InputX < 0) { Send.transform.localScale = Send.P_Info.SetLeftVec; }

        if (CommonCollection.InputX > 0) { Send.transform.localScale = Send.P_Info.SetRightVec; }
    }

    public void LogicUpdate(PlayerController Send)
    {

    }

    public void PhysicsUpdate(PlayerController Send)
    {

    }
}
