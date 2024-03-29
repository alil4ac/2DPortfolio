using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AI : IState<EnemyController>
{

    float delaytime = 3f;
    public void Enter(EnemyController Send)
    {
        Send.RB.velocity = Vector2.zero;
        if(Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            delaytime = Send.E_Info.PatternDelayTime;
        }
    }

    public void Exit(EnemyController Send)
    {
        if (Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Send.Pattern.SetValue();
            delaytime = Send.E_Info.PatternDelayTime;
        }
    }

    public void HandleInput(EnemyController Send)
    {

    }

    public void LogicUpdate(EnemyController Send)
    {
        if (Send.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Send.ChangeState(EnemyController.EState.E_Patrol);
        }
        else if(Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            delaytime -= Time.deltaTime;
            if(delaytime <= 0)
            {
                Send.ChangeState(Send.Pattern.CallPattern());
            }
        }

    }

    public void PhysicsUpdate(EnemyController Send)
    {

    }
}
