using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Information
{

    private float p_Speed = 1f;

    public float P_Speed { get { return p_Speed; } }

    private Vector2 p_DashPose = new(7f,0f);

    public Vector2 P_DashPose { get { return p_DashPose; } }

    private Vector2 _jumpPose = new(0f, 5f);

    public Vector2 JumpPose { get { return _jumpPose; } }

    private Vector3 _setLeftVec = new(-1,1,1);

    public Vector3 SetLeftVec { get { return _setLeftVec; } }

    private Vector3 _setRightVec = new(1, 1, 1);

    public Vector3 SetRightVec { get { return _setRightVec; } }

    private float _invincible_Time = 0f;

    public float Inveincible_Time { get { return _invincible_Time; } }


    private int _equipMagic = 0;

    public int EquipMagic { get { return _equipMagic; } }

    public void SetUpBeat()
    {
        if (_invincible_Time <= 0) _invincible_Time = 5f;
    }

    public void GetUpBeat()
    {
        if (_invincible_Time > 0) _invincible_Time -= Time.deltaTime;
    }

    public void SetStatus()
    {
        hp = 100;
        max_hp = 100;
        mp = 100;
        max_mp = 100;
        damage = 5;
        armor = 2;
        p_exp = 0;
        p_maxexp = 20;
    }

    public void SetLevelUp(PlayerInfo player)
    {
        while(player.p_exp >= player.p_maxexp)
        {
            player.p_exp -= player.p_maxexp;
            player.p_level++;
            player.max_hp += 10;
            player.hp = max_hp;
            player.max_mp += 3;
            player.mp = max_mp;
            player.damage += 3;
            player.p_maxexp += 10;
        }
        SoundManager.Instance.LevelUpSound();
        UIManager.Instance.GetHpBar(player, "Player");
        UIManager.Instance.SetLevelUp(player);
    }

    public void ChangeMagic_Q()
    {
        if (_equipMagic == 0) _equipMagic = 3;
        else _equipMagic--;
    }

    public void ChangeMagic_E()
    {
        if (_equipMagic == 3) _equipMagic = 0;
        else _equipMagic++;
    }

    public void SetDead()
    {
        hp = 0;
    }

}
