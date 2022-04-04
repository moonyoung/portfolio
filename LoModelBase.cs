using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoModel;

public class LoModelBase : LoRecycleData
{
	#region define layer
	public static int LAYER_OBJECT_PICK		= 9;
	public static int LAYER_ENEMY_CHAR 		= 12;
	public static int LAYER_ALLY_CHAR 		= 14;
	public static int LAYER_MAP_FLOOR		= 15;
	public static int LAYER_ASTAR_OBJECT	= 19;
	public static int LAYER_MAP_OBSTACLE	= 21;
	public static int LAYER_CUSTOM_LAYER	= 24;
	#endregion

    #region LoModelBaseProperty
	// system
	private float mfGameSpeed = 1.7f;
	// Chase 대상이 되는 hero 와 ally character 간의 거리 간격. 이 이상이면 모든 action 을 멈추고, Chase 대상이 되는 hero 를 따라 붙는다.
	protected float mfDistInterValWithChaseChar = 27.0f;		
	protected bool mbChaseHeroMode = false;		/* 현재 controlable char 를 추적해야 되는 상황인지.. */
	// Strategy mode 에서 COL 의 경우, 추적대상에게 근접했다고 판단되는 거리. 
	protected float mfDistInterValForStrategyMuster = 3.5f;
	// chase model 을 다시 따라붙어야 하는 거리 간격.
	protected float mfDistReChaseForStrategyMusterAI = 7.0f;

	/* 
	 * char 의 status, equipment 의 계산 근거가 되는 unit 및 equipment 관련 데이터.
	 * 모델 init 및 create 시, 셋팅해 줘야 한다.
	*/
	private LoPlayfabLomlUnit mPlayfabUnit = null;
	private LomlSsdUnitEquipment mUnitEquipMent = null;

    // base property
	private Vector3 mPosition;
	private bool mBSummoned = false;			// summon 이 됐는지..
	private bool mBDied = false;				// GameObject Destroy 와는 별개로 died 상태처리용.
	private float m_fMaxHP = 1000.0f;
    private float m_fHP = 100.0f;
    private float m_fMP = 20.0f;
	private float mfMoveSpeed = 2.0f;
	private float mfTurnSpeed = 320.0f;
	private float mfTargetResearchInterval = 0.5f;
	protected Rigidbody mBaseRigidBody = null;

		// for attack
	private int mIAttackInstanceID = 0;
	private int mIAttackInstanceID_02 = 0;
	private float mfAttackInterval = 2.0f;
	private int mAttackPower = 1;			// 양손의 공격력이 각기 다른경우 right
	private int mAttackPower2 = 1;			// left
	private int mMagicPower = 1;
	private int mSpecialPower = 1;
	private int mSpecial02Power = 1;
	private int mSpecial03Power = 1;
	private int mWeightPower = 1;	// 특수기 등에서 기본 데미지들에 가중치 데미지를 주는 경우 사용.

	private int mDefensivePower = 0;		// 방어력.
	private int mRecuperativePower = 0;		// 회복력.
	private float mRecuperativeCycleTime = 13f;	// 회복되는 주기 시간

	// 부가 기능에 의해 전투시 영향을 미치는 효과들
	private int mRestoreHpOfDamage		= 0;	// 데미지의 몇 HP 회복
	private int mRestoreHpOfDamagePer 	= 0;	// 데미지의 몇 % HP 회복	
	private int mRestoreSkillGageOfUsePer	= 0;	// 스킬 사용시 5% 즉시 회복

	private eLoModelAniState m_eAniState = eLoModelAniState.eAniState_None;		// 현재 animation state
	private eLoModelCharType m_eCharType = eLoModelCharType.eCharType_None;
	private eLoModelJob m_eJob = eLoModelJob.eJob_None;
	private eLoModelFlag m_eFlag = eLoModelFlag.eFlag_None;

	private eLoModelAiType m_eAiType = eLoModelAiType.eIdleAi_None;
	protected bool mBAutoTargetResearch = false;	// 특정 시가마다 적을 재탐지 할지를 결정. 

	// Ai Range type
	private float m_fAiRangeMaxDis = 0.0f;
	private float m_fAiRangeMinDis = 0.0f;
	private float m_fAiRangeMidDis = 0.0f;
	private float m_fAiDistRecheckTarget = 0.0f;	/* 얼마만큼 거리가 벌어지면 타겟을 재 설정하는지. */
	private float m_fAiCheckRangeInterval = 1.0f;

	// AI 
	protected bool mBEableNavMeshProcess = false;
	protected float mfEnableNavMeshProcessDelay = 0.0f;

	// Strategy 를 결정.
	protected eLoStrategy m_eLoStrategy = eLoStrategy.eLoStrategy_None;

	private MeshRenderer[] m_arrDamageMeshRenderer;

	// Lomodel 이 어디서 생성 되었는지에 따라 초기 셋팅을 달리 해주기 위해 사용.
	private LoModelScenario.eLoModelInitOn mEModelInitOn = LoModelScenario.eLoModelInitOn.eInitOn_None;

	// game reference
	protected RunData mGameMainRunData = null;
	protected GameMain mRefGameMain = null;

	// picked ui 표시할때 기준이 되는 transform.  캐릭터마다 다를수 있음. 특히 MiniPoe 의 경우 attack2 시 기본 object 와는 다르게 내부 object 의 위치가 변함.
	protected Transform mTrPickRefer = null;	

	// selected lomodel
	protected LoModelBase mCurSelectedControlModels;	// 현재 선택된 ally model 참조	

	// game run data
	private int mHittedCount = 0;	// hit 된 count 수
	
	// fixedUpdate 에서 분기 조건 사용대신, 콜백 연결을 변경하여 조건문 비교를 대신. 연산절약
	protected delegate void DelLoFixedUpdateControl();
	protected DelLoFixedUpdateControl mDelLoFixedUpdateContorl = null;
	protected virtual void FixedUpdateDelay(){}
	protected virtual void FixedUpdateMainLoop(){}
	protected virtual void FixedUpdateCustomScenario(){}
	public virtual void SetChangeScenario(LoScenario.eLoScenarioControl eScenarioControl){}

	public float GameSpeed
	{
		set { mfGameSpeed = value; }
		get { return mfGameSpeed; }
	}

	protected float DistIntervalWithChaseChar
	{
		get { return mfDistInterValWithChaseChar; }
		set { mfDistInterValWithChaseChar = value; }
	}

	protected bool ChaseHeroMode
	{
		get { return mbChaseHeroMode; }
		set { mbChaseHeroMode = value;}
	}

	protected float DistInterValForStrategyCOL
	{
		get { return mfDistInterValForStrategyMuster; }
		set { mfDistInterValForStrategyMuster = value; }
	}

	public LoPlayfabLomlUnit PlayfabLomlUnit
	{
		get { return mPlayfabUnit; }
		set { mPlayfabUnit = value; }
	}

	public LomlSsdUnitEquipment UnitEquipment
	{
		get { return mUnitEquipMent; }
		set { mUnitEquipMent = value; }
	}

	public Vector3 Position
	{
		set { mPosition = value; }
		get { return mPosition; }
	}

	public bool Summoned
	{
		set { mBSummoned = value; }
		get { return mBSummoned; }
	}

	public bool Died
	{
		set { mBDied = value; }
		get { return mBDied; }
	}

	public float MaxHp
	{
		set { m_fMaxHP = value;}
		get { return m_fMaxHP;}
	}

    public float Hp
    {
        set { m_fHP = value; }
        get { return m_fHP; }
    }

    public float Mp
    {
        set { m_fMP = value; }
        get { return m_fMP; }
    }

	public float MoveSpeed
	{
		set { mfMoveSpeed = value; }
		get { return mfMoveSpeed; }
	}

	public float TurnSpeed
	{
		set { mfTurnSpeed = value; }
		get { return mfTurnSpeed; }
	}

	public float TargetResearchInterval
	{
		set { mfTargetResearchInterval = value; }
		get { return mfTargetResearchInterval; }
	}

	public Rigidbody BaseRigidBody
	{
		set { mBaseRigidBody = value; }
		get { return mBaseRigidBody; }
	}

	public int AttackInstanceID
	{
		set { mIAttackInstanceID = value; }
		get { return mIAttackInstanceID; }
	}

	public int AttackInstanceID_02
	{
		set { mIAttackInstanceID_02 = value; }
		get { return mIAttackInstanceID_02; }
	}

	public float AttackInterval
	{
		set { mfAttackInterval = value; }
		get { return mfAttackInterval; }
	}

	public int AttackPower
	{
		set { mAttackPower = value; }
		get { return mAttackPower; }
	}

	public int AttackPower2Left
	{
		set { mAttackPower2 = value; }
		get { return mAttackPower2; }
	}

	public int MagicPower
	{
		set { mMagicPower = value; }
		get { return mMagicPower; }
	}

	public int SpecialPower
	{
		set { mSpecialPower = value; }
		get { return mSpecialPower; }
	}

	public int Special02Power
	{
		set { mSpecial02Power = value; }
		get { return mSpecial02Power; }
	}

	public int Special03Power
	{
		set { mSpecial03Power = value; }
		get { return mSpecial03Power; }
	}

	public int WeightPower
	{
		set { mWeightPower = value; }
		get { return mWeightPower; }
	}

	public int DefensivePower
	{
		set { mDefensivePower = value; }
		get { return mDefensivePower; }
	}

	public int RecuperativePower
	{
		set { mRecuperativePower = value; }
		get { return mRecuperativePower; }
	}

	public float RecuperativeCycleTime
	{
		set { mRecuperativeCycleTime = value;	}
		get { return mRecuperativeCycleTime; }
	}

	public int RestoreHpOfDamage
	{
		set { mRestoreHpOfDamage = value;}
		get { return mRestoreHpOfDamage;}
	}

	public int RestoreHpOfDamagePer
	{
		set { mRestoreHpOfDamagePer = value;}
		get { return mRestoreHpOfDamagePer;}
	}

	public int RestoreSkillGageOfUsePer
	{
		set { mRestoreSkillGageOfUsePer = value;}
		get { return mRestoreSkillGageOfUsePer;}
	}

	public eLoModelAniState AniState
	{
		set { m_eAniState = value; }
		get { return m_eAniState; }
	}

	public eLoModelCharType CharType
	{
		set { m_eCharType = value; }
		get { return m_eCharType; }
	}

    public eLoModelJob Job
    {
        set { m_eJob = value; }
        get { return m_eJob; }
    }

	public eLoModelFlag Flag 
	{
		set { m_eFlag = value; }
		get { return m_eFlag; }
	}

	public eLoModelAiType AiType
	{
		set { m_eAiType = value; }
		get { return m_eAiType; }
	}

	public bool AutoTargetReSearch
	{
		set { mBAutoTargetResearch = value;}
		get { return mBAutoTargetResearch;}
	}

	public float AiRangeMax
	{
		set { m_fAiRangeMaxDis = value; }
		get { return m_fAiRangeMaxDis; }
	}

	public float AiRangeMin
	{
		set { m_fAiRangeMinDis = value; }
		get { return m_fAiRangeMinDis; }
	}

	public float AiRangeMid
	{
		set { m_fAiRangeMidDis = value; }
		get { return m_fAiRangeMidDis; }
	}

	public float AiDistRecheckTarget
	{
		set { m_fAiDistRecheckTarget = value; }
		get { return m_fAiDistRecheckTarget; }
	}

	public float AiRangeCheckInterval
	{
		set { m_fAiCheckRangeInterval = value; }
		get { return m_fAiCheckRangeInterval; }
	}

	public Transform PickedRefer
	{
		set { mTrPickRefer = value; }
		get { return mTrPickRefer; }
	}

	public LoModelBase CurSelectedControlModel
	{
		set { mCurSelectedControlModels = value; }
		get { return mCurSelectedControlModels; }
	}

	public LoModelScenario.eLoModelInitOn LoModelInitOn
	{
		set { mEModelInitOn = value; }
		get { return mEModelInitOn; }
	}

	public eLoStrategy LoStrategy
	{
		set { m_eLoStrategy = value; }
		get { return m_eLoStrategy; }
	}

	public int HittedCount{
		set { mHittedCount = value; }
		get { return mHittedCount; }
	}
    #endregion

	public virtual void LoModelInitAndLoad()
	{
	}

	public virtual void SetGameSpeed(float fGameSpeed)
	{
		GameSpeed = fGameSpeed;
	}

	public virtual void SetHealType01(int nHealHp){}

	public virtual void SetMainHeroDied(bool bDied){}

	public virtual void SetStrategy(LoModel.eLoStrategy eStrategy){	LoStrategy = eStrategy;	}

	public virtual void LoadAudioData(){}
	
	// damage 에 대해 회복되는 HP 계산하여 반영.
	public virtual float GetRestoreHpThenDamage(float baseDamage){
		float restoreHP = 0;
		restoreHP += RestoreHpOfDamage;
		restoreHP += (int)(baseDamage * (RestoreHpOfDamagePer * 0.01f /* RestoreHpOfDamagePer 이 2% 처럼 단순 int 값이므로 100% 으로 변환*/));
		
		return restoreHP;
	}

#region for Rigidbody
	public void LoRigidBodyFreezeAll(){
		BaseRigidBody.constraints =
			RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ |
			RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}
	public void LoRigidBodyReleaseDefault(){
		BaseRigidBody.constraints =
			RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
	}
#endregion

#region for autoTargetResearch
	private IEnumerator CorAutoTargetResearch(float fInterval)
	{
		float fDelayTime = 0.0f;
		while(fDelayTime < fInterval)
		{
			fDelayTime += Time.deltaTime;
			yield return null;
		}
		if(fDelayTime >= fInterval)
			AutoTargetReSearch = true;
	}
	public void StartAutoTargetResearch(float fInterval)
	{
		AutoTargetReSearch = false;
		StartCoroutine(CorAutoTargetResearch(fInterval));
	}
	public void StopAutoTargetResearch()
	{
		AutoTargetReSearch = false;
		StopCoroutine(CorAutoTargetResearch(0));
	}
#endregion

}
