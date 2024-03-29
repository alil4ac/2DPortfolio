using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Patrol : IState<EnemyController>
{
    float targetPos;
    float PatrolTime;
    int RandomX;

    Vector2 pos;
    public void Enter(EnemyController Send)
    {
        if(Send.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PatrolTime = 3f;
            RandomX = (Random.Range(-1, 2));
        }

        if(Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            PatrolTime = 1f;
            RandomX = Random.Range(0, Send.Patrolpos.Length);
        }
    }

    public void Exit(EnemyController Send)
    {
        if (Send.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PatrolTime = 3f;
            RandomX = (Random.Range(-1, 2));
        }
        if (Send.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PatrolTime = 1f;
            RandomX = (Random.Range(-1, 2));
        }
    }

    public void HandleInput(EnemyController Send)
    {
            if (Send.RB.velocity.x < -0.1f)
            {

                Send.transform.localScale = Send.E_Info.LocalScaleL;

                Send.Anim.SetFloat("Horizontal", -1f);
            }

            else if (Send.RB.velocity.x > 0.1f)
            {
                Send.transform.localScale = Send.E_Info.LocalScaleR;

                Send.Anim.SetFloat("Horizontal", 1f);
            }

            else
            {
                Send.Anim.SetFloat("Horizontal", 0f);
            }
    }

    public void LogicUpdate(EnemyController Send)
    {
        PatrolTime -= Time.deltaTime;
        if(Send.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            targetPos = Vector3.Distance(GameManager.Instance.PlayerTr.position, Send.transform.position);

            if (PatrolTime <= 0 && targetPos > 3f)
            {
                Send.ChangeState(EnemyController.EState.E_Patrol);
            }
            if (targetPos <= 3f)
            {
                Send.ChangeState(EnemyController.EState.E_Trace);
            }
        }

        if(Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            pos = Send.Patrolpos[RandomX].position - Send.transform.position;
            float dis = Vector2.Distance(Send.transform.position, pos);
            if (PatrolTime <= 0f)
            {
                Send.ChangeState(EnemyController.EState.E_AI);
            }
            if (dis <= 0.5f)
            {
                Send.ChangeState(EnemyController.EState.E_AI);
            }
        }
    }

    public void PhysicsUpdate(EnemyController Send)
    {
        if (Send.gameObject.layer == LayerMask.NameToLayer("Enemy") &&
            Send.Anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            Send.RB.velocity = new Vector2((float)RandomX * Send.E_Info.E_PatrolSpeed, 0);
        }

        if(Send.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Send.RB.velocity = new Vector2(pos.normalized.x * Send.E_Info.E_PatrolSpeed, pos.normalized.y * Send.E_Info.E_PatrolSpeed);
        }

    }
}
