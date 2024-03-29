using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Variables

    private Animator _anim;

    public Animator Anim { get { return _anim; } set { _anim = value; } }

    private Rigidbody2D _rb;

    public Rigidbody2D RB { get { return _rb; } set { _rb = value; } }

    private CapsuleCollider2D _collider;

    public CapsuleCollider2D Collider { get { return _collider; } set { _collider = value; } }

    private SpriteRenderer _render;

    public SpriteRenderer Render { get { return _render; } set { _render = value; } }

    private bool _isJump = false;

    private bool _isGrounded = true;

    public bool IsJump { get { return _isJump; } set { _isJump = value; } }

    private bool _doubleJump = false;

    public bool DoubleJump { get { return _doubleJump; } set { _doubleJump = value; } }

    private bool _isFlip;
    public bool IsFlip
    {
        get
        {
            if (this.transform.localScale.x < 0) _isFlip = true;

            if (this.transform.localScale.x > 0) _isFlip = false;

            return _isFlip;
        }
    }

    private bool _isSlope;

    public bool IsSlope { get { return _isSlope; } }

    private bool _isDead = false;

    private int C_StateIndex = 0;

    [SerializeField]
    private float _slopeCheckDistance = 0.5f;

    [SerializeField]
    private float _downRayDistance = 0.2f;

    private float _slopeDownAngle;

    private float _slopeDownAngleOld;

    private float _slopeSideAngle;

    private Vector2 _colsize;

    public Vector2 ColSize { get { return _colsize; } }

    private Vector2 _slopeNormalPerp;

    public Vector2 SlopeNormalPerp { get { return _slopeNormalPerp; } }

    #endregion

    #region Property

    [SerializeField]
    private PhysicsMaterial2D _noFriction;

    [SerializeField]
    private PhysicsMaterial2D _fullFriction;

    public PhysicsMaterial2D FullFriction { get { return _fullFriction; } }

    [System.NonSerialized]
    public PlayerInfo P_Info;

    [System.NonSerialized]
    public Magic_Info M_Info;
    public enum EState
    {
        Movement = 0,
        Jump = 1,
        Crouch = 2,
        Dash = 3,
        DJump = 4,
        Attack = 5,
        Kick = 6,
        Magic = 7,
        Hurt = 8,
        Dead = 9
    }

    private StateMachine<PlayerController> statemachine;

    private Dictionary<EState, IState<PlayerController>> m_state = new Dictionary<EState, IState<PlayerController>>();

    #endregion

    #region Method

    private void DeadTrigger()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            P_Info.SetDead();
        }
    }

    private void IsDead()
    {
        if(P_Info.Health <= 0 && !_isDead)
        {
            _isDead = true;
            ChangeState(EState.Dead);
        }
    }

    public void ChangeState(EState estate)
    {
        C_StateIndex = (int)estate;

        _anim.SetInteger("E_Type", C_StateIndex);

        statemachine.ChangeState(m_state[estate]);

        Debug.Log("Player Has Change Next State -> " + estate);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(CommonCollection.AndLayerToLayerMask(collision.gameObject.layer,CommonCollection.L_Enemy))
        {
            ChangeState(EState.Hurt);

            EnemyInfo info = collision.gameObject.GetComponent<EnemyController>().E_Info;

            P_Info.GetDmg(info);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CommonCollection.AndLayerToLayerMask(collision.gameObject.layer, CommonCollection.L_EAttack))
        {
            if(collision.gameObject.GetComponentInParent<EnemyController>() == null)
            {
                Magic_Info m_info = collision.GetComponent<Magic_Info>();

                P_Info.GetDmg(m_info);

                ChangeState(EState.Hurt);
            }
            else if(collision.gameObject.GetComponentInParent<EnemyController>() != null)
            {
                EnemyInfo info = collision.gameObject.GetComponentInParent<EnemyController>().E_Info;

                P_Info.GetDmg(info);

                ChangeState(EState.Hurt);
            }
            else { return; }
        }
    }

    public void GetHit()
    {
        P_Info.SetUpBeat();

        this.gameObject.layer = LayerMask.NameToLayer("Ignore");

        StartCoroutine(OnBeatTime());
    }

    private IEnumerator OnBeatTime()
    {
        float progerss = 0f;

        while (P_Info.Inveincible_Time > 0f)
        {
            progerss += Time.deltaTime * 1f;

            _render.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(0, 0, 0, 1f), Mathf.PingPong(progerss, 1f));

            yield return new WaitForEndOfFrame();
        }

        if (P_Info.Inveincible_Time <= 0f)
        {
            _render.color = new Color(1, 1, 1, 1);

            this.gameObject.layer = LayerMask.NameToLayer("Player");

            yield break;
        }
    }

    public void SlopeCheck()
    {
        Vector2 CheckPos = transform.position - new Vector3(0f, ColSize.y / 2);

        SlopeCheckVertical(CheckPos);

        SlopeCheckHorizontal(CheckPos);
    }

    private void SlopeCheckHorizontal(Vector2 CheckPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(CheckPos, transform.right, _slopeCheckDistance, CommonCollection.L_Tile);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(CheckPos, -transform.right, _slopeCheckDistance, CommonCollection.L_Tile);

        if (slopeHitFront)
        {
            _isSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            _isSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }

        else
        {
            _isSlope = false;

            _slopeSideAngle = 0.0f;
        }
    }

    private void SlopeCheckVertical(Vector2 CheckPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(CheckPos, Vector2.down, _slopeCheckDistance, LayerMask.GetMask("Tile"));

        if (hit)
        {
            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _slopeDownAngleOld)
            {
                _isSlope = true;
            }

            _slopeDownAngleOld = _slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.red);

            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (_isSlope && CommonCollection.InputX == 0.0f)
        {
            RB.sharedMaterial = _fullFriction;
        }
        else { RB.sharedMaterial = _noFriction; }

    }

    public bool CheckGrounded()
    {
        Vector2 cheskPos =this.transform.position - new Vector3(0, ColSize.y / 2);

        RaycastHit2D hit = Physics2D.Raycast(cheskPos, Vector2.down, _downRayDistance, CommonCollection.L_Tile);
        Debug.DrawRay(hit.point, Vector3.down, Color.white);
        if (!hit)
        {
            _isGrounded = false;
        }
        else if (hit)
        {
            _isGrounded = true;
        }
        return _isGrounded;
    }

    #endregion


    private void Awake()
    {
        P_Info = this.GetComponent<PlayerInfo>();

        P_Info.SetStatus();

        M_Info = this.gameObject.AddComponent<Magic_Info>();

        m_state.Add(EState.Movement, new Movement());

        m_state.Add(EState.Jump, new Jump());

        m_state.Add(EState.Crouch, new Crouch());

        m_state.Add(EState.Dash, new Dash());

        m_state.Add(EState.DJump, new DJump());

        m_state.Add(EState.Attack, new Attack());

        m_state.Add(EState.Kick, new Kick());

        m_state.Add(EState.Magic, new Magic());

        m_state.Add(EState.Hurt, new Hurt());

        m_state.Add(EState.Dead, new Dead());

        _anim = this.GetComponent<Animator>();

        _rb = this.GetComponent<Rigidbody2D>();

        _collider = this.GetComponent<CapsuleCollider2D>();

        _colsize = Collider.size;

        _render = this.GetComponent<SpriteRenderer>();

        statemachine = new StateMachine<PlayerController>(this, m_state[EState.Movement]);

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {


    }

    private void Update()
    {
        statemachine.HandleInput();

        statemachine.LogicUpdate();

        P_Info.GetUpBeat();

        IsDead();

        DeadTrigger();
    }

    private void FixedUpdate()
    {
        statemachine.PhysicsUpdate();
    }

}
