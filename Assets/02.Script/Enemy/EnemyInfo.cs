using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : Information
{
    public enum EType
    {
        Bat = 0,
        Mummy = 1,
        Skeleton = 2,
        Slime = 3,
        Spider = 4,
        Sorcerer = 5,
        Demon = 6,
        Ghost = 7,
        Phantom = 8,
        GhostWolf = 9,
        Wendigo = 10,
        Angel = 11,
    }

    private Vector3 localscaleL;

    public Vector3 LocalScaleL { get { return localscaleL; } }

    private Vector3 localscaleR;

    public Vector3 LocalScaleR { get { return localscaleR; } }

    private float e_movespeed;

    public float E_MoveSpeed { get { return e_movespeed; } }

    private float e_patrolspeed;

    public float E_PatrolSpeed { get { return e_patrolspeed; } }

    private float e_attackrange;

    private bool usemagic;

    public bool UseMagic { get { return usemagic; } }

    public float E_AttackRange { get { return e_attackrange; } }

    private float ResetParam;

    private float attacktime;

    public float AttackTime { get { return attacktime; } }

    private float hittime = 0.5f;

    public float HitTime { get { return hittime; } }

    private float patternDelayTime = 3f;

    public float PatternDelayTime { get { return patternDelayTime; } }

    public void ResetHitTime()
    {
        hittime = 0.5f;
    }

    public void SetHitTime()
    {
        hittime -= Time.deltaTime;
    }

    public void ResetAttackTime()
    {
        attacktime = ResetParam;
    }
    public void SetAttackTime()
    {
        if (attacktime > 0) attacktime -= Time.deltaTime;
    }

    public void SetInfo(string name)
    {
        switch (name)
        {
            case "Mummy":
                hp = 30;
                damage = 15;
                armor = 3;
                e_exp = 20;
                e_movespeed = 1f;
                e_patrolspeed = 1f;
                e_attackrange = 0f;
                attacktime = 0f;
                ResetParam = 0f;
                usemagic = false;
                localscaleL = new Vector3(-1, 1, 1);
                localscaleR = new Vector3(1, 1, 1);
                break;

            case "Skeleton":
                hp = 8;
                damage = 5;
                armor = 1;
                e_exp = 10;
                e_movespeed = 1f;
                e_patrolspeed = 1f;
                e_attackrange = 0f;
                attacktime = 0f;
                ResetParam = 0f;
                usemagic = false;
                localscaleL = new Vector3(1, 1, 1);
                localscaleR = new Vector3(-1, 1, 1);
                break;

            case "Slime":
                hp = 55;
                damage = 25;
                armor = 3;
                e_exp = 25;
                e_patrolspeed = 0.5f;
                e_movespeed = 0.5f;
                e_attackrange = 0f;
                attacktime = 0f;
                ResetParam = 0f;
                usemagic = false;
                localscaleL = new Vector3(2, 2, 1);
                localscaleR = new Vector3(-2, 2, 1);
                break;

            case "Spider":
                hp = 25;
                damage = 10;
                armor = 1;
                e_exp = 15;
                e_movespeed = 1.25f;
                e_patrolspeed = 1f;
                e_attackrange = 0.5f;
                attacktime = 2f;
                ResetParam = attacktime;
                usemagic = false;
                localscaleL = new Vector3(1, 1, 1);
                localscaleR = new Vector3(-1, 1, 1);
                break;

            case "Demon":
                hp = 75;
                damage = 30;
                armor = 5;
                e_exp = 50;
                e_movespeed = 1f;
                e_patrolspeed = 0.5f;
                e_attackrange = 5f;
                attacktime = 1f;
                ResetParam = attacktime;
                usemagic = true;
                localscaleL = new Vector3(1, 1, 1);
                localscaleR = new Vector3(-1, 1, 1);
                break;

            case "Ghost":
                hp = 20;
                damage = 8;
                armor = 1;
                e_exp = 15;
                e_movespeed = 1.25f;
                e_patrolspeed = 1f;
                e_attackrange = 2f;
                attacktime = 2f;
                ResetParam = attacktime;
                usemagic = true;
                localscaleL = new Vector3(1, 1, 1);
                localscaleR = new Vector3(-1, 1, 1);
                break;

            case "Phantom":
                hp = 125;
                max_hp = 125;
                armor = 3;
                damage = 25;
                e_exp = 75;
                e_movespeed = 0.5f;
                e_patrolspeed = 0.5f;
                e_attackrange = 10f;
                attacktime = 2f;
                ResetParam = attacktime;
                usemagic = true;
                patternDelayTime = 1f;
                localscaleL = new Vector3(-2, 2, 1);
                localscaleR = new Vector3(2, 2, 1);
                break;

            case "GhostWolf":
                hp = 150;
                max_hp = 150;
                armor = 6;
                damage = 40;
                e_exp = 100;
                e_movespeed = 2f;
                e_patrolspeed = 2.5f;
                e_attackrange = 1f;
                attacktime = 2.5f;
                ResetParam = attacktime;
                usemagic = true;
                patternDelayTime = 0.1f;
                localscaleL = new(2, 2, 1);
                localscaleR = new(-2, 2, 1);
                break;

            case "Angel":
                hp = 250;
                max_hp = 250;
                armor = 8;
                damage = 50;
                e_exp = 1000;
                e_movespeed = 2f;
                e_patrolspeed = 1.5f;
                attacktime = 3f;
                ResetParam = attacktime;
                patternDelayTime = 1.5f;
                usemagic = true;
                localscaleL = new Vector3(1.5f, 1.5f, 1f);
                localscaleR = new Vector3(1.5f, 1.5f, 1f);
                break;
        }
    }
}
