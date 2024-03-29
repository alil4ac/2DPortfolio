using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Info : Information
{
    public enum E_pMagic
    {
        None = 0,
        Air = 1,
        Ice = 2,
        Thunder = 3,
    }

    public enum E_eMagic
    {
        Fireball = 0,
        MagicMisile = 1,
        VenomShot = 2,
        FireBlue = 3,
        E_Thunder = 4,
        E_Ice = 5,
        E_Air = 6,
    }

    private Vector2 SpawnPos;

    public Vector2 GetSpawnVec(int index, bool Isflip, Vector2 Pos)
    {
        if (index == 0) SpawnPos = Vector2.zero;

        if (index == 1)
        {
            if (Isflip) SpawnPos = Pos + new Vector2(-1f, -0.5f);

            else SpawnPos = Pos + new Vector2(1f, -0.5f);
        }
        if(index == 2)
        {
            if (Isflip) SpawnPos = Pos + new Vector2(-1f, -0.5f);

            else SpawnPos = Pos + new Vector2(1f, -0.5f);
        }
        if(index == 3)
        {
            if (Isflip) SpawnPos = Pos + new Vector2(-1f, -0.5f);

            else SpawnPos = Pos + new Vector2(1f, -0.5f);
        }

        return SpawnPos;
    }
    public void SetUpMagic(string name)
    {
        switch (name)
        {
            case "Air":
                this.damage = 15;
                break;
            case "Ice":
                this.damage = 25;
                break;
            case "Thunder":
                this.damage = 30;
                break;
            case "Fireball":
                this.damage = 35;
                break;
            case "FireBlue":
                this.damage = 15;
                break;
            case "E_Thunder":
                this.damage = 50;
                break;
            case "E_Ice":
                this.damage = 45;
                break;
            case "E_Air":
                this.damage = 30;
                break;
        }
    }
}
