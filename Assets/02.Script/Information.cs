using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    #region Variables

    protected int p_level = 1;

    public int P_Level { get { return p_level; } }

    protected int p_max_level = 100;

    public int P_Max_Level { get { return p_max_level; } }

    protected int hp;

    public int Health { get { return hp; } }

    protected int max_hp;

    public int Max_Health { get { return max_hp; } }

    protected int mp;

    public int Mana { get { return mp; } }

    protected int max_mp;

    public int Max_Mana { get { return max_mp; } }

    protected int p_exp;

    public int P_Exp { get { return p_exp; } }

    protected int p_maxexp;

    public int P_MaxExp { get { return p_maxexp; } }

    protected int damage;

    public int Damage { get { return damage; } }

    protected int e_exp;

    public int E_Exp { get { return e_exp; } }

    protected int armor;

    public int Armor { get { return armor; } }

    protected int i_recover;

    public int I_Recover { get { return i_recover; } }

    #endregion
    public void GetItem(ItemInfo other)
    {
        this.hp += other.RecoverHP;
        this.mp += other.RecoverMP;

        if (other.HPUP) this.max_hp += 50;

        if (other.MPUP) this.max_mp += 10;

        if (this.hp > max_hp) this.hp = max_hp;

        if (this.mp > max_mp) this.mp = max_mp;
    }
    public void GetDmg(Information other)
    {
        this.hp -= other.damage - this.armor;
    }

    public void GetExp(PlayerInfo player)
    {
        player.p_exp += this.e_exp;
        
        if(player.p_exp >= player.p_maxexp)
        {
            player.SetLevelUp(player);
        }
        float newExp = ((float)player.p_exp / (float)player.P_MaxExp) * 100; 

        UIManager.Instance.SetExpBar(newExp);
    }

    public float SetSliderBarValue()
    {
        return ((float)hp / (float)max_hp) * 100;
    }
}
