using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CommonCollection
{
    #region InputValue

    public static float InputX { get { return Input.GetAxisRaw("Horizontal"); } }

    public static bool AttackBtn { get { return Input.GetMouseButtonDown(0); } }

    public static bool KickBtn { get { return Input.GetMouseButtonDown(1); } }

    public static bool CrouchBtn { get { return Input.GetKey(KeyCode.S); } }

    public static bool JumpBtn { get { return Input.GetKeyDown(KeyCode.Space); } }

    public static bool DashBtn { get { return Input.GetKeyDown(KeyCode.LeftControl); } }

    public static bool MagicBtn { get { return Input.GetKeyDown(KeyCode.F); } }

    public static bool ChangeMagicQ { get { return Input.GetKeyDown(KeyCode.Q); } }

    public static bool ChangeMagicE { get { return Input.GetKeyDown(KeyCode.E); } }

    public static bool ESCBtn { get { return Input.GetKeyDown(KeyCode.Escape); } }

    #endregion

    #region LayerMask

    public static readonly int L_Tile = LayerMask.GetMask("Tile");

    public static readonly int L_Enemy = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Boss"); 

    public static readonly int L_EAttack = LayerMask.GetMask("E_Attack") | LayerMask.GetMask("E_Magic");

    public static readonly int L_Player = LayerMask.GetMask("Player");

    public static readonly int L_Ignore = LayerMask.GetMask("Ignore");

    public static readonly int L_PAttack = LayerMask.GetMask("P_Attack") | LayerMask.GetMask("P_Magic");

    #endregion

    public static bool AndLayerToLayerMask(int Layer, int _LayerMask)
    {
        string LayerName = LayerMask.LayerToName(Layer);

        int colLayer = LayerMask.GetMask(LayerName);

        if((colLayer & _LayerMask) != 0) { return true; }

        else { return false; }
    }
}
