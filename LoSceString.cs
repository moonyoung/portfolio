using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LoScenarioString
{	
	public class LoSceStringData
	{
		int mMentType;		// 1 : normal, 2 : mind
		int mLine;
		String mStrString;
		
		public LoSceStringData(int nMentType, int nLine, String strString)
		{
			mMentType = nMentType;
			mLine = nLine;
			mStrString = strString;
		}
		public int MentType{
			get { return mMentType;}
		}
		public int Line{			
			get { return mLine;}
		}
		public String SceString{			
			get { return mStrString;}
		}
	}	

	public class LoSceStringBase {
		public virtual int GetNumString(){
			return 0;
		}		
		public virtual string GetSceString(int nSceIndex){
			return null;
		}
		public virtual LoSceStringData GetStringData(int nSceIndex){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoCommonStr eCommonStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoGameStr eGameStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoRewardStr eRewardStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoQuestStr eQuestStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoCommentScenario eCommentScenario){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenario.eLoScenario eLoScenario, LoScenarioString.eLoCommentScenario eCommentScenario){
			return null;
		}
        public virtual LoSceStringData GetStringData(LoScenarioString.eLoWeaponStr eWeaponStrId){
            return null;
        }
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoMainMenuStr eMainMenuStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoMainMenuGameStr eMainMenuGameStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoMainMenuScenarioStr eMainMenuScenarioStrId){
			return null;
		}
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoServerStr eServerStrId){
			return null;
		} 
		public virtual LoSceStringData GetStringData(LoScenarioString.eLoTitleSceneStr eTitleSceneStrId){
			return null;
		}
		public virtual string GetString(LoScenarioString.eLoScenarioCharName eCharNameIndex){
			return null;
		}	
	}

	public class LoSceString {
		public static int STR_LOAD_COMMON		= 0x0001;
		public static int STR_LOAD_CHARNAME		= 0x0002;
		public static int STR_LOAD_COMMENT		= 0x0004;
		public static int STR_LOAD_SCENARIO		= 0x0008;
		public static int STR_LOAD_GAME			= 0x0010;
		public static int STR_LOAD_REWARD		= 0x0020;
		public static int STR_LOAD_QUEST		= 0x0040;
		public static int STR_LOAD_WEAPON       = 0x0080;
		public static int STR_LOAD_MAINMENU		= 0x0100;
		public static int STR_LOAD_MAINMENU_GAME= 0x0200;
		public static int STR_LOAD_MAINMENU_SCENARIO = 0x0400;
		public static int STR_LOAD_SERVER		= 0x0800;
		public static int STR_LOAD_TITLESCENE	= 0x1000;

		private static LoSceString _instance = null;
		private LoSceStrCommon 	mCommonStr = null;
		private LoSceStrGame	mGameStr = null;
		private LoSceStrReward  mRewardStr = null;
		private LoSceStrQuest 	mQuestStr = null;
		private LoSceStrCharName mCharName = null;
		private LoSceStrComment mComment = null;
		private LoSceStrWeapon mWeaponStr = null;
		private LoSceStrMainMenu mMainMenuStr = null;
		private LoSceStrMainMenuGame mMainMenuGameStr = null;
		private LoSceStrMainMenuScenario mMainMenuScenarioStr = null;
		private LoSceStrServer	mServerStr = null;
		private LoSceStrTitle mTitleSceneStr = null;
		private LoSceStringBase mCurScenario = null;		
		
		private int mNumCommentString = 0;

		public int NumCommentString{
			set{ mNumCommentString = value;}
			get{ return mNumCommentString;}
		}

		public void LoadLomlString(int nLoadStrCategory)
		{
			if((nLoadStrCategory & STR_LOAD_COMMON) == STR_LOAD_COMMON){
				if (mCommonStr != null) { mCommonStr.Dispose(); }
				LoadCommonString();
			}
			if((nLoadStrCategory & STR_LOAD_COMMENT) == STR_LOAD_COMMENT){
				if (mComment != null) { mComment.Dispose(); }
				LoadCommentString();
			}
			if((nLoadStrCategory & STR_LOAD_CHARNAME) == STR_LOAD_CHARNAME){
				if (mCharName != null) { mCharName.Dispose(); }
				LoadCharNameString();
			}
			if((nLoadStrCategory & STR_LOAD_GAME) == STR_LOAD_GAME){
				if (mGameStr != null) { mGameStr.Dispose(); }
				LoadGameString();
			}
			if((nLoadStrCategory & STR_LOAD_REWARD) == STR_LOAD_REWARD){
				if (mRewardStr != null) { mRewardStr.Dispose(); }
				LoadRewardString();
			}
			if((nLoadStrCategory & STR_LOAD_QUEST) == STR_LOAD_QUEST){
				if (mQuestStr != null) { mQuestStr.Dispose(); }
				LoadQuestString();
			}
            if((nLoadStrCategory & STR_LOAD_WEAPON) == STR_LOAD_WEAPON){
				if (mWeaponStr != null) { mWeaponStr.Dispose(); }
				LoadWeaponString();
            }
			if((nLoadStrCategory & STR_LOAD_MAINMENU) == STR_LOAD_MAINMENU){
				if (mMainMenuStr != null){ mMainMenuStr.Dispose(); }
				LoadMainMenuString();
			}
			if((nLoadStrCategory & STR_LOAD_MAINMENU_GAME) == STR_LOAD_MAINMENU_GAME){
				if (mMainMenuGameStr != null){ mMainMenuGameStr.Dispose(); }
				LoadMainMenuGameString();
			}
			if((nLoadStrCategory & STR_LOAD_MAINMENU_SCENARIO) == STR_LOAD_MAINMENU_SCENARIO){
				if (mMainMenuScenarioStr != null){ mMainMenuScenarioStr.Dispose(); }
				LoadMainMenuScenarioString();
			}
			if((nLoadStrCategory & STR_LOAD_SERVER) == STR_LOAD_SERVER){
				if(mServerStr != null){	mServerStr.Dispose(); }
				LoadServerString();
			}
			if((nLoadStrCategory & STR_LOAD_TITLESCENE) == STR_LOAD_TITLESCENE){
				if(mTitleSceneStr != null){ mTitleSceneStr.Dispose(); }
				LoadTitleSceneString();
			}
		}

		public void ReleaseLomlString(int nReleaseStrCategory)
		{
			if((nReleaseStrCategory & STR_LOAD_COMMON) == STR_LOAD_COMMON){				
				if(mCommonStr != null){
					mCommonStr.Dispose();	
					mCommonStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_COMMENT) == STR_LOAD_COMMENT){				
				if(mComment != null){
					mComment.Dispose();		
					mComment = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_CHARNAME) == STR_LOAD_CHARNAME){
				if(mCharName != null){
					mCharName.Dispose();		
					mCharName = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_GAME) == STR_LOAD_GAME){				
				if(mGameStr != null){
					mGameStr.Dispose();		
					mGameStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_REWARD) == STR_LOAD_REWARD){
				if(mRewardStr != null){
					mRewardStr.Dispose();	
					mRewardStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_QUEST) == STR_LOAD_QUEST){	
				if(mQuestStr != null){
					mQuestStr.Dispose();		
					mQuestStr = null;
				}
			}
            if((nReleaseStrCategory & STR_LOAD_WEAPON) == STR_LOAD_WEAPON){
                if(mWeaponStr != null) { 
					mWeaponStr.Dispose();	
					mWeaponStr = null;
				}
            }
			if((nReleaseStrCategory & STR_LOAD_MAINMENU) == STR_LOAD_MAINMENU){
				if(mMainMenuStr != null) { 
					mMainMenuStr.Dispose();	
					mMainMenuStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_MAINMENU_GAME) == STR_LOAD_MAINMENU_GAME){
				if(mMainMenuGameStr != null) { 
					mMainMenuGameStr.Dispose();	
					mMainMenuGameStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_MAINMENU_SCENARIO) == STR_LOAD_MAINMENU_SCENARIO){
				if(mMainMenuScenarioStr != null){
					mMainMenuScenarioStr.Dispose();
					mMainMenuScenarioStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_SERVER) == STR_LOAD_SERVER){
				if(mServerStr != null) { 
					mServerStr.Dispose(); 	
					mServerStr = null;
				}
			}
			if((nReleaseStrCategory & STR_LOAD_TITLESCENE) == STR_LOAD_TITLESCENE){
				if(mTitleSceneStr != null){ 
					mTitleSceneStr.Dispose(); 
					mTitleSceneStr = null;
				}
			}
		}

		public void LoadSceString(LoScenarioString.eLoScenarioScene eSceScene)
		{			
			switch (eSceScene) {		
			case eLoScenarioScene.eLoSceScene_Stage01Sce01:				
				mCurScenario = new LoSceStrStage01Sce01 (LoSystem.LoLanguage.LoLan_Kor);
				break;
			case eLoScenarioScene.eLoSceScene_Stage01First:
				mCurScenario = new LoSceStrStage01First (LoSystem.LoLanguage.LoLan_Kor);
				break;			
			default:
				break;
			}
		}

		public void LoadCommonString()
		{
			mCommonStr = new LoSceStrCommon(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadCharNameString()
		{
			mCharName = new LoSceStrCharName (LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadGameString()
		{
			mGameStr = new LoSceStrGame(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadRewardString()
		{
			mRewardStr = new LoSceStrReward(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadQuestString()
		{
			mQuestStr = new LoSceStrQuest(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadCommentString()
		{
			mComment = new LoSceStrComment(LoSystem.LoLanguage.LoLan_Kor);
			mNumCommentString = mComment.GetNumString();
		}

		public void LoadCommentString(eLoScenarioScene eSceneScenario)
		{
			mComment = new LoSceStrComment(eSceneScenario, LoSystem.LoLanguage.LoLan_Kor);
			mNumCommentString = mComment.GetNumString();			
		}

        public void LoadWeaponString()
        {
			mWeaponStr = new LoSceStrWeapon(LoSystem.LoLanguage.LoLan_Kor);
        }

		public void LoadMainMenuString()
		{
			mMainMenuStr = new LoSceStrMainMenu(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadMainMenuGameString()
		{
			mMainMenuGameStr = new LoSceStrMainMenuGame(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadMainMenuScenarioString()
		{
			mMainMenuScenarioStr = new LoSceStrMainMenuScenario(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadServerString()
		{
			mServerStr = new LoSceStrServer(LoSystem.LoLanguage.LoLan_Kor);
		}

		public void LoadTitleSceneString()
		{
			mTitleSceneStr = new LoSceStrTitle(LoSystem.LoLanguage.LoLan_Kor);
		}

		public string GetSceString(int index)
		{			
			return mCurScenario.GetSceString (index);
		}

		public LoSceStringData GetCommonString(LoScenarioString.eLoCommonStr eCommonStrId)
		{
			return mCommonStr.GetStringData(eCommonStrId);
		}

		public LoSceStringData GetGameString(LoScenarioString.eLoGameStr eGameStrId)
		{
			return mGameStr.GetStringData(eGameStrId);
		}

		public LoSceStringData GetRewardString(LoScenarioString.eLoRewardStr eRewardStrId)
		{
			return mRewardStr.GetStringData(eRewardStrId);
		}

		public LoSceStringData GetQuestString(LoScenarioString.eLoQuestStr eQuestStrId)
		{
			return mQuestStr.GetStringData(eQuestStrId);			
		}

		public string GetCharName(LoScenarioString.eLoScenarioCharName eCharNameIndex)
		{
			return mCharName.GetString (eCharNameIndex);
		}

		public LoSceStringData GetCommentString(eLoCommentScenario eCommentScenario)
		{
			return mComment.GetStringData(eCommentScenario);
		}

		public LoSceStringData GetCommentString(LoScenario.eLoScenario eLoScenario, eLoCommentScenario eCommentScenario)
		{
			return mComment.GetStringData(eLoScenario, eCommentScenario);			
		}

		public LoSceStringData GetCommentString(int nIndex)
		{
			return mComment.GetStringData(nIndex);			
		}

        public LoSceStringData GetWeaponString(LoScenarioString.eLoWeaponStr eWeaponStrId)
        {
			return mWeaponStr.GetStringData(eWeaponStrId);
        }

		public LoSceStringData GetMainMenuString(LoScenarioString.eLoMainMenuStr eMainMenuStrId)
		{
			return mMainMenuStr.GetStringData(eMainMenuStrId);
		}

		public LoSceStringData GetMainMenuGameString(LoScenarioString.eLoMainMenuGameStr eMainMenuGameStrId)
		{
			return mMainMenuGameStr.GetStringData(eMainMenuGameStrId);
		}

		public LoSceStringData GetMainMenuScenarioString(LoScenarioString.eLoMainMenuScenarioStr eMainMenuScenarioStrId)
		{
			return mMainMenuScenarioStr.GetStringData(eMainMenuScenarioStrId);
		}

		public LoSceStringData GetServerString(LoScenarioString.eLoServerStr eServerStrId)
		{
			return mServerStr.GetStringData(eServerStrId);
		}

		public LoSceStringData GetTitleSceneString(LoScenarioString.eLoTitleSceneStr eTitleSceneStrId)
		{
			return mTitleSceneStr.GetStringData(eTitleSceneStrId); 
		}

		public void ClearLoadCurSceneString()
		{
			mCurScenario = null;
		}

		public static LoSceString Instance
		{
			get {
				if (_instance == null) {
					_instance = new LoSceString ();
				}
				return _instance;
			}
		}
	}

	public enum eLoScenarioCharName
	{
		eLoSceCharName_MainHero	= 0,
		eLoSceCharName_Commander2ndCorps,
		eLoSceCharName_Commander1stCorps,
		eLoSceCharName_WhiteMage,

		eLoSceCharName_KingOfVaran,
	}

	public enum eLoScenarioScene
	{		
		eLoSceScene_Stage01Sce01,
		eLoSceScene_Stage01First,
		eLoSceScene_Stage01,
	}
	
	public enum eLoCommentScenario
	{
		eLoCommentSce_None = 0,
		eLoCommentSce_NormalRandom,
		eLoCommentSce_Stage01_01,
	}

	public enum eLoGameStr
	{
		eLoGameStr_None				= 0,
		eLoGameStr_Varan,		
	}

	public enum eLoRewardStr
	{
		eLoRewardStr_None				= 0,
		eLoRewardStr_ObjClearStageTitle,
		eLoRewardStr_ObjClearStageLevelTitle,
		eLoRewardStr_ObjLoClassTitle,
		eLoRewardStr_ObjReputationTitle,
		eLoRewardStr_ObjEnemyKillTitle,
		eLoRewardStr_ObjAllyLevelUpTitle,
		eLoRewardStr_ObjAllyReachLevelTitle,
		eLoRewardStr_ObjBuyItem,
		eLoRewardStr_ObjManageWeatponMakeCount,
		eLoRewardStr_ObjManageWeatponUpgradeCount,
		eLoRewardStr_ObjManageWeatponSellCount,
		eLoRewardStr_ObjMakeWeaponCountTitle,
		eLoRewardStr_ObjUpgradeWeaponCountTitle,
		eLoRewardStr_ObjSellWeaponCountTitle,
	}

	public enum eLoQuestStr
	{
		eLoQuestStr_None				= 0,
		eLoQuestStr_QuestMainTitle,
		eLoQuestStr_GoalTitle,
		eLoQuestStr_Welcome_Title,
		eLoQuestStr_Welcome_Des,
		eLoQuestStr_FinishedTutorial_Title,
		eLoQuestStr_FinishedTutorial_Des,
		eLoQuestStr_ClearedEasy_Title,
		eLoQuestStr_ClearedEasy_Des,
		eLoQuestStr_ClearedMedium_Title,
		eLoQuestStr_ClearedMedium_Des,
		eLoQuestStr_ClearedHard_Title,
		eLoQuestStr_ClearedHard_Des,
		eLoQuestStr_KillUglyFrog_Title,
		eLoQuestStr_KillUglyFrog_Des,
		eLoQuestStr_KillBlueGumbarr_Title,
		eLoQuestStr_KillBlueGumbarr_Des,
		eLoQuestStr_KillAkeleton_Title,
		eLoQuestStr_KillAkeleton_Des,
		eLoQuestStr_KillMiniPoe_Title,
		eLoQuestStr_KillMiniPoe_Des,
		eLoQuestStr_KillBlMardo_Title,
		eLoQuestStr_KillBlMardo_Des,
		eLoQuestStr_KillBoboKang_Title,
		eLoQuestStr_KillBoboKang_Des,
		eLoQuestStr_KillRoePlo_Title,
		eLoQuestStr_KillRoePlo_Des,
		eLoQuestStr_KillEnemy_Title,
		eLoQuestStr_KillEnemy_Des,
	}

	public enum eLoCommonStr
	{
		eLoCommStr_None						= 0,		

		eLoCommStr_Weapon,
		eLoCommStr_Sword,
		eLoCommStr_LongSword,
		eLoCommStr_Bow,
		eLoCommStr_Staff,
		eLoCommStr_Wand,
		eLoCommStr_Arrow,
		eLoCommStr_Shield,
		eLoCommStr_Acc,
		eLoCommStr_Scroll,

		eLoCommStr_Hp,
		eLoCommStr_LoEnegy,
		eLoCommStr_AttackPower,
		eLoCommStr_PhysicalAttackPower,
		eLoCommStr_MagicalAttackPower,
		eLoCommStr_DefencePower,
		eLoCommStr_RecuperativeAmount,
		eLoCommStr_Exp,
		eLoCommStr_NotUsed,
		eLoCommStr_Equipped,
		eLoCommStr_DoEquip,
		eLoCommStr_Up,
		eLoCommStr_TimeToDuration,
		eLoCommStr_TotalSum,
		eLoCommStr_Second,
		eLoCommStr_Coin,
		eLoCommStr_Gem,
		eLoCommStr_Release,
		eLoCommStr_Progress,
		eLoCommStr_Cancel,
		eLoCommStr_Confirm,
		eLoCommStr_Completed,
		eLoCommStr_Upgrade,
		eLoCommStr_SellConfirm,
		eLoCommStr_BuyConfirm,
		eLoCommStr_TermHero,
		eLoCommStr_On,
		eLoCommStr_Off,
		eLoCommStr_Yes,
		eLoCommStr_No,

		// system
		eLoCommStr_System_ExitGameTitle,
		eLoCommStr_System_ExitGameDes,

		/***** MainMenu  *****/
		eLoCommStr_MainMenu_Level,
		eLoCommStr_MainMenu_StatHpTitle,
		eLoCommStr_MainMenu_StatAttackTitle,		
		eLoCommStr_MainMenu_StatRightHandAttackTitle,
		eLoCommStr_MainMenu_StatDefenceTitle,
		eLoCommStr_MainMenu_StatRecuperativePowerTitle,
		eLoCommStr_MainMenu_StatExt,
		eLoCommStr_MainMenu_StatExtLeftHandTitle,
		eLoCommStr_MainMenu_StatExtWhiteMageMagicAttackTitle,
		eLoCommStr_MainMenu_StatExtRedWitchMagicAttackTitle,
		eLoCommStr_MainMenu_SkillTitle,		
		
		// store
		eLoCommStr_Store_RestPurchaseAvailableTitle,

		// bottom menu
		eLoCommStr_MainMenu_BottomMenu_Store,
		eLoCommStr_MainMenu_BottomMenu_ManageTeam,
		eLoCommStr_MainMenu_BottomMenu_ManageWeapon,
		eLoCommStr_MainMenu_BottomMenu_MakeWeapon,
		eLoCommStr_MainMenu_BottomMenu_Reward,
		eLoCommStr_MainMenu_BottomMenu_Summons,
		eLoCommStr_MainMenu_BottomMenu_WorldMap,
		eLoCommStr_MainMenu_BottomMenu_MgrScroll,
		// mgr character
		eLoCommStr_MainMenu_MgrChar_JoinTeam,
		eLoCommStr_MainMenu_MgrChar_LeaveTeam,
		eLoCommStr_MainMenu_MgrChar_ConstructTitle,
		eLoCommStr_MainMenu_MgrChar_NotyetBatch,
		eLoCommStr_MainMenu_MgrChar_PossessionHeroTitle,
		eLoCommStr_MainMenu_MgrChar_EquipWeaponListTitle,
		eLoCommStr_MainMenu_MgrChar_TimeToReuse,
		eLoCommStr_MainMenu_MgrChar_ChargingTime,
		eLoCommStr_MainMenu_MgrChar_SetupSkill,
		eLoCommStr_MainMenu_MgrChar_ReleaseSkill,
		eLoCommStr_MainMenu_MgrChar_ReleaseSKillUseAny,
		eLoCommStr_MainMenu_MgrChar_QuestionReleaseSkill,
		eLoCommStr_MainMenu_MgrChar_QuestionUpgradeSkill,
		eLoCommStr_MainMenu_MgrChar_TouchOneMore,
		eLoCommStr_MainMenu_MgrChar_InsufficientExp,
		eLoCommStr_MainMenu_MgrChar_WeaponUpgradeGuideDes,
		eLoCommStr_MainMenu_MgrChar_QuestionUpgradeWeaponFor,
		eLoCommStr_MainMenu_MgrChar_EquipWeapon,
		eLoCommStr_MainMenu_MgrChar_ReleaseWeapon,
		eLoCommStr_MainMenu_MgrChar_ReplaceWeaponMsg,			

		// Settings
		eLoCommStr_Settings_Title,
		eLoCommStr_Settings_Effect_Title,
		eLoCommStr_Settings_Bg_Title,

		// Upgrade
		eLoCommStr_MainMenu_Upgrade_StrToUpgrade,

		// Material 
		eLoCommStr_MaterialName_Iron,
		eLoCommStr_MaterialName_Jewel,
		eLoCommStr_MaterialName_Emerald,
		eLoCommStr_MaterialName_Gold,
		eLoCommStr_MaterialName_Leather,
		eLoCommStr_MaterialName_Wood,

		// Make Weapon
        eLoCommStr_MakeWeapon_MakeWeaponTitle,      // 무기 제조
		eLoCommStr_MakeWeapon_CustomOrderTitle,		// 주문 제작.
		eLoCommStr_MakeWeapon_DoMakeCustomOrder,		// 제작 외뢰
		eLoCommStr_MakeWeapon_RequestMakeWeaponToMaster,// 무기 제작 요청
		eLoCommStr_MakeWeapon_WeaponClassUp,		// 무기 강화
		eLoCommStr_MakeWeapon_TryWeaponClassUpTitle,	// 강화하기
		eLoCommStr_MakeWeapon_FailedClassUp,			// 강화 실패
		eLoCommStr_MakeWeapon_SuccessClassUp,			// 강화 성공
		eLoCommStr_MakeWeapon_ImpossibleReason_LackCoin_Title,	// Coin 부족
		eLoCommStr_MakeWeapon_ImpossibleReason_LackCoin_Des,	// Coin 부족
		eLoCommStr_MakeWeapon_ImpossibleReason_LackCoinForClassUp_Des, // Coin 부족으로 무기강화를 할수 없음.
		eLoCommStr_MakeWeapon_ImpossibleReason_NotNeed_Material_Title,	// 재료부족
		eLoCommStr_MakeWeapon_ImpossibleReason_NotNeed_Material_Des,	// 재료부족
		eLoCommStr_MakeWeapon_ImpossibleReason_Full_WeaponSlot_Title,	// 무기고가 꽉참.
		eLoCommStr_MakeWeapon_ImpossibleReason_Full_WeaponSlot_Des,		// 무기고가 꽉참.
		eLoCommStr_MakeWeapon_ReturnMakeWeaponFirstStepDes,
		eLoCommStr_MakeWeapon_CompleteMakeWeaponMsgTitle,				// 무기제조 성공.
		eLoCommStr_MakeWeapon_CompleteMasterMentClass01,
		eLoCommStr_MakeWeapon_CompleteMasterMentClass02,
		eLoCommStr_MakeWeapon_CompleteMasterMentClass03,
		eLoCommStr_MakeWeapon_CompleteMasterMentClass04,
        eLoCommStr_MakeWeapon_AddedEffectTitle,         // 추가됨

		// weapon grade,   1:기본, 2:상급, 3:희귀, 4:영웅, 5:전설, 6:신화
		eLoCommStr_WeaponGrade_Normal,
		eLoCommStr_WeaponGrade_High,
		eLoCommStr_WeaponGrade_Rare,

		eLoCommStr_WeaponGrade_Hero,		
		eLoCommStr_WeaponGrade_Legendary,
		eLoCommStr_WeaponGrade_Mythic,

		// Server
		eLoCommStr_Server_MakingCustomOrderTitle,	// 주문 제작 중..
		eLoCommStr_Server_MakingCustomOrder,		// 장인이 주기를 만들고 있습니다...		
		

		// related skill
		eLoCommStr_Skill_MainHero_PowerSmash,
		eLoCommStr_Skill_MainHero_PowerUp,
		eLoCommStr_Skill_MainHero_SpinSlash,
		eLoCommStr_Skill_WhiteMage_AllHeal,
		eLoCommStr_Skill_AreminNok_MultipleShot,
		eLoCommStr_Skill_RedWitch_HurricaneFlame,
		eLoCommStr_Skill_WhiteKnight_LegendSword,
		eLoCommStr_Skill_GuardNon_MultipleShot,
		eLoCommStr_Skill_DarkKnight_DarkSlam,

		eLoCommStr_Skill_MainHero_PowerSmash_Des,
		eLoCommStr_Skill_MainHero_PowerUp_Des,
		eLoCommStr_Skill_MainHero_SpinSlash_Des,
		eLoCommStr_Skill_WhiteMage_AllHeal_Des,
		eLoCommStr_Skill_AreminNok_MultipleShot_Des,
		eLoCommStr_Skill_RedWitch_HurricaneFlame_Des,
		eLoCommStr_Skill_WhiteKnight_LegendSword_Des,
		eLoCommStr_Skill_GuardNon_MultipleShot_Des,
		eLoCommStr_Skill_DarkKnight_DarkSlam_Des,

		eLoCommStr_Skill_MainHero_PowerSmash_AttackDes,
		eLoCommStr_Skill_MainHero_PowerUp_AttackDes,
		eLoCommStr_Skill_MainHero_SpinSlash_AttackDes,
		eLoCommStr_Skill_WhiteMage_AllHeal_AttackDes,
		eLoCommStr_Skill_AreminNok_MultipleShot_AttackDes,
		eLoCommStr_Skill_RedWitch_HurricaneFlame_AttackDes,
		eLoCommStr_Skill_WhiteKnight_LegendSword_AttackDes,
		eLoCommStr_Skill_GuardNon_MultipleShot_AttackDes,
		eLoCommStr_Skill_DarkKnight_DarkSlam_AttackDes,

		// additional effect
		eLoCommStr_AddEffect_RestoreThenDamageTitle,
		eLoCommStr_AddEffect_RestoreThenDamagePerTitle,
		eLoCommStr_AddEffect_RestoreThenDamageDes,

		eLoCommStr_AddEffect_RestoreSkillGageTitle,
		eLoCommStr_AddEffect_RestoreSkillGageDes,

		// advertisement
		eLoCommStr_Adv_PlaySpeedMode2XTitle,
		eLoCommStr_Adv_RecvTicket_Plus,
		eLoCommStr_Adv_PullTicketTitle,	
		eLoCommStr_Adv_EarnRewardPlaySpeed2xTicket,

		// world map
		eLoCommStr_WorldMap_TitleUsedPlaySpeedModeTicket,
	}

    public enum eLoWeaponStr
    {
		// weapon name
        eLoWeaponStr_None,
		eLoWeaponStr_Sword_Type01,
		eLoWeaponStr_Sword_Type02,
		eLoWeaponStr_Sword_Type03,
		eLoWeaponStr_Sword_Type04,
		eLoWeaponStr_Sword_Type05,
		eLoWeaponStr_Sword_Type06,
		eLoWeaponStr_Sword_Type07,
		eLoWeaponStr_Sword_Type08,
		eLoWeaponStr_Sword_Type09,

		eLoWeaponStr_LongSword_Type01,
		eLoWeaponStr_LongSword_Type02,
		eLoWeaponStr_LongSword_Type03,
		eLoWeaponStr_LongSword_Type04,
		eLoWeaponStr_LongSword_Type05,
		eLoWeaponStr_LongSword_Type06,

		eLoWeaponStr_Bow_Type01,
		eLoWeaponStr_Bow_Type02,
		eLoWeaponStr_Bow_Type03,
		eLoWeaponStr_Bow_Type04,
		eLoWeaponStr_Bow_Type05,
		eLoWeaponStr_Bow_Type06,

		eLoWeaponStr_Staff_Type01,
		eLoWeaponStr_Staff_Type02,
		eLoWeaponStr_Staff_Type03,
		eLoWeaponStr_Staff_Type04,
		eLoWeaponStr_Staff_Type05,

		eLoWeaponStr_Wand_Type01,
		eLoWeaponStr_Wand_Type02,
		eLoWeaponStr_Wand_Type03,
		eLoWeaponStr_Wand_Type04,

		eLoWeaponStr_Arrow_Type01,
		eLoWeaponStr_Arrow_Type02,
		eLoWeaponStr_Arrow_Type03,
		eLoWeaponStr_Arrow_Type04,

		eLoWeaponStr_Shield_Type01,
		eLoWeaponStr_Shield_Type02,
		eLoWeaponStr_Shield_Type03,
		eLoWeaponStr_Shield_Type04,

		eLoWeaponStr_Acc_Type01,
		eLoWeaponStr_Acc_Type02,
		eLoWeaponStr_Acc_Type03,
		eLoWeaponStr_Acc_Type04,
		eLoWeaponStr_Acc_Type05,
		eLoWeaponStr_Acc_Type06,
	}

	public enum eLoMainMenuStr
    {
        eLoMainMenuStr_None,
		eLoMainMenuStr_ScrollSortName,
		eLoMainMenuStr_ScrollSortCount,
		eLoMainMenuStr_ScrollSortType,
		eLoMainMenuStr_ScrollSortClass,
		eLoMainMenuStr_ScrollSellPrice,
		eLoMainMenuStr_ScrollDecideSellQuantity,

		eLoMainMenuStr_NotApplyScroll,				// 적용 불가한 주문서.
		eLoMainMenuStr_NotUpgradeScrollType,		// 업그래이드용 주문서가 아닙니다.
		eLoMainMenuStr_NotMakeScrollType,			// 제작용 주문서가 아닙니다.
		eLoMainMenuStr_NotApplyScrollDes,			// 적용 불가능한 주문서 입니다.
		eLoMainMenuStr_NotApplyScrollOverLapDes,	// 이미 같은 주문서가 적용된 상태입니다.
		eLoMainMenuStr_NotApplyScrollNotAssociationDes,	// 조합 불가능한 주문서 입니다.
		eLoMainMenuStr_AlreadySimilarScrollTitle,	// 한번 더 체크 권합니다.
		eLoMainMenuStr_AlreadySimilarScrollDes,		// 적용이 가능하지만, 이미 비슷한 종류의 주문서가 설정된 상태입니다.
		eLoMainMenuStr_NoHaveScrollTitle,			// 보유 주문서 없음
		eLoMainMenuStr_NoHaveScrollMsg,				// 보유중인 주문서가 없습니다.

		// Scroll Title
		eLoMainMenuStr_Scroll_AddEffect_Class01_Title,
		eLoMainMenuStr_Scroll_AddEffect_Class02_Title,
		eLoMainMenuStr_Scroll_AddEffect_Class03_Title,
		eLoMainMenuStr_Scroll_AddEffect_Class04_Title,

		eLoMainMenuStr_Scroll_AddAttack_Class01_Title,
		eLoMainMenuStr_Scroll_AddAttack_Class02_Title,
		eLoMainMenuStr_Scroll_AddAttack_Class03_Title,
		eLoMainMenuStr_Scroll_AddAttack_Class04_Title,

		eLoMainMenuStr_Scroll_MakeLevelWeapon_Class01_Title,
		eLoMainMenuStr_Scroll_MakeLevelWeapon_Class02_Title,
		eLoMainMenuStr_Scroll_MakeLevelWeapon_Class03_Title,
		eLoMainMenuStr_Scroll_MakeLevelWeapon_Class04_Title,

		eLoMainMenuStr_Scroll_ClassUp_AddRate_Class01_Title,
		eLoMainMenuStr_Scroll_ClassUp_AddRate_Class02_Title,
		eLoMainMenuStr_Scroll_ClassUp_AddRate_Class03_Title,
		eLoMainMenuStr_Scroll_ClassUp_AddRate_Class04_Title,
	
		eLoMainMenuStr_Scroll_MakeClassWeapon_Class01_Title,
		eLoMainMenuStr_Scroll_MakeClassWeapon_Class02_Title,
		eLoMainMenuStr_Scroll_MakeClassWeapon_Class03_Title,
		eLoMainMenuStr_Scroll_MakeClassWeapon_Class04_Title,

		eLoMainMenuStr_Scroll_MakeClass_AddEffect_Class01_Title,
		eLoMainMenuStr_Scroll_MakeClass_AddEffect_Class02_Title,
		eLoMainMenuStr_Scroll_MakeClass_AddEffect_Class03_Title,
		eLoMainMenuStr_Scroll_MakeClass_AddEffect_Class04_Title,
		
		// Scroll Description
		eLoMainMenuStr_Scroll_Des_AddEffect_LowLevel_01,
		eLoMainMenuStr_Scroll_Des_AddEffect_LowLevel_02,
		eLoMainMenuStr_Scroll_Des_AddEffect_LowLevel_03,
		eLoMainMenuStr_Scroll_Des_AddEffect_LowLevel_04,
		eLoMainMenuStr_Scroll_Des_AddEffect_LowLevel_05,
		eLoMainMenuStr_Scroll_Des_AddEffect_MediumLevel_01,
		eLoMainMenuStr_Scroll_Des_AddEffect_MediumLevel_02,
		eLoMainMenuStr_Scroll_Des_AddEffect_MediumLevel_03,
		eLoMainMenuStr_Scroll_Des_AddEffect_MediumLevel_04,
		eLoMainMenuStr_Scroll_Des_AddEffect_MediumLevel_05,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighLevel_01,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighLevel_02,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighLevel_03,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighLevel_04,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighLevel_05,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighestLevel_01,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighestLevel_02,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighestLevel_03,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighestLevel_04,
		eLoMainMenuStr_Scroll_Des_AddEffect_HighestLevel_05,

		eLoMainMenuStr_Scroll_Des_AddAttack_A07_R70,
		eLoMainMenuStr_Scroll_Des_AddAttack_A09_R70,
		eLoMainMenuStr_Scroll_Des_AddAttack_A12_R70,
		eLoMainMenuStr_Scroll_Des_AddAttack_A17_R70,
		eLoMainMenuStr_Scroll_Des_AddAttack_A23_R70,
		eLoMainMenuStr_Scroll_Des_AddAttack_A29_R70,
		eLoMainMenuStr_Scroll_Des_AddAttack_A32_R100,
		eLoMainMenuStr_Scroll_Des_AddAttack_A36_R100,
		eLoMainMenuStr_Scroll_Des_AddAttack_A42_R100,
		eLoMainMenuStr_Scroll_Des_AddAttack_A50_R60,

		eLoMainMenuStr_Scroll_Des_Make70_Level3,
		eLoMainMenuStr_Scroll_Des_Make70_Level5,
		eLoMainMenuStr_Scroll_Des_Make100_Level5,
		eLoMainMenuStr_Scroll_Des_ClassUp_AddRate20,
		eLoMainMenuStr_Scroll_Des_ClassUp_AddRate30,
		eLoMainMenuStr_Scroll_Des_ClassUp_AddRate40,
		eLoMainMenuStr_Scroll_Des_ClassUp_AddRate50,
		eLoMainMenuStr_Scroll_Des_ClassUp_AddRate100,
		eLoMainMenuStr_Scroll_Des_MakeMediumClass_Weapon,
		eLoMainMenuStr_Scroll_Des_MakeHighClass_Weapon,
		eLoMainMenuStr_Scroll_Des_MakeHighestClass_Weapon,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectMedium03_MClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectMedium05_MClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectHigh03_HClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectHigh05_HClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectHighest03_HClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectHighest05_HClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectHighest03_HEClass,
		eLoMainMenuStr_Scroll_Des_Make_AddEffectHighest05_HEClass,
	}

	public enum eLoMainMenuGameStr
	{
		eLoMainMenuGameStr_None						= 0,
		eLoMainMenuGameStr_Varan					= 1,
		eLoMainMenuGameStr_VaranCastle				= 2,
		eLoMainMenuGameStr_VaranForest				= 3,
		eLoMainMenuGameStr_VaranField				= 4,
		eLoMainMenuGameStr_TombEnterOfVaranKings	= 5,
		eLoMainMenuGameStr_TombOfVaranKings			= 6,
		eLoMainMenuGameStr_PoPenBeach				= 7,

		eLoMainMenuGameStr_Stage_Difficulty,
		eLoMainMenuGameStr_Stage_Easy,
		eLoMainMenuGameStr_Stage_Medium,
		eLoMainMenuGameStr_Stage_Hard,			
	}

	public enum eLoMainMenuScenarioStr
	{
		eLoMmScenarioStr_None						= 0,
		eLoMmScenarioStr_FinishedPrevSceneAfter_01,
		eLoMmScenarioStr_FinishedPrevSceneAfter_02,
		eLoMmScenarioStr_FinishedPrevSceneAfter_03,
		eLoMmScenarioStr_FinishedPrevSceneAfter_04,

		eLoMmScenarioStr_AfterClearVaranField_01,
		eLoMmScenarioStr_AfterClearVaranField_02,
		eLoMmScenarioStr_AfterClearVaranField_03,
		eLoMmScenarioStr_AfterClearVaranField_04,

		eLoMmScenarioStr_AfterClearEnterKingTomb_01,
		
	}

	public enum eLoServerStr
	{
		eLoServerStr_None	= 0,		
		eLoServerStr_ReleasedNewVersion,
		eLoServerStr_GoToStore,
		eLoServerStr_LogingToServer,	
		eLoServerStr_ConnectionError,
		eLoServerStr_ConnectionErrorDes01,
		eLoServerStr_ServerError,
		eLoServerStr_ServerErrorDes01,
		eLoServerStr_TitleLogOutStatus,
		eLoServerStr_DesLogOutStatus,

		eLoServerStr_ConfirmGotoStore,
		eLoServerStr_ExitGame,
		eLoServerStr_ExitGameTitle,
		eLoServerStr_RestartGame,	
		eLoServerStr_RestartGameTitle,
		// main menu server
		eLoServerStr_SuccessReConnectServer,
		eLoServerStr_OkReConnectServerDes,
		eLoServerStr_TitleSuccessBuy,		
		eLoServerStr_TitleFailedBuy,
		eLoServerStr_TitleWorkNeedChargeCoin,
		eLoServerStr_TitleWorkNeedChargeGem,
		eLoServerStr_FailedWorkNeedChargeCoin,
		eLoServerStr_FailedWorkNeedChargeGem,
		eLoServerStr_FailedWorkNeedSyncServer,
		eLoServerStr_PlayFabServerErrorTryReStart,	

		eLoServerStr_TitleEarnedLoReward,
		eLoServerStr_DesEarnedLoReward,
		eLoServerStr_FailedEarnedLoReward,
		eLoServerStr_TitleEarnedQuestReward,
		eLoServerStr_DesEarnedQuestReward,
		eLoServerStr_FailedEarnedQuestReward,

		eLoServerStr_TitleSaveLoOpenReward,
		eLoServerStr_DesSaveLoOpenReward,
		eLoServerStr_FailedSaveLoOpenReward,

		eLoServerStr_TitleSyncRewardAD,
		eLoServerStr_DesSyncRewardAD,
		eLoServerStr_SuccessSyncRewardAD,
		eLoServerStr_FailedSyncRewardAD,

		eLoServerStr_TitleSyncLoScenario,
		eLoServerStr_DesSyncLoScenario,
		eLoServerStr_SuccessSyncLoScenario,
		eLoServerStr_FailedSyncLoScenario,
		eLoServerStr_TitleSavedGameResultToServer,
		eLoServerStr_DesSavedGameResultToServer,
		eLoServerStr_TitleFailedSaveGameResult,
		eLoServerStr_TitleOkUpgradeChar,
		eLoServerStr_DesOkUpgradeChar,
		eLoServerStr_TitleFailedUpgradeChar,		
		eLoServerStr_OkUpgradeWeaponSentence01,
		eLoServerStr_TitleOkUpgradeWeapon,
		eLoServerStr_TitleFailedUpgradeWeapon,
		eLoServerStr_TitleOkUpgradeSkill,
		eLoServerStr_OkUpgradeSkillSentence01,
		eLoServerStr_TitleFailedUpgradeSkill,
		eLoServerStr_TitleOkReleaseSkill,
		eLoServerStr_OkReleaseSkillSentence01,
		eLoServerStr_TitleFailedReleaseSkill,
		eLoServerStr_TitleOkReleaseTeamSlot,
		eLoServerStr_OkReleaseTeamSlotSentence01,
		eLoServerStr_TitleFailedReleaseTeamSlot,	
		eLoServerStr_TitleOkReleaseHero,
		eLoServerStr_OkReleaseHeroSentence01,
		eLoServerStr_TitleFailedReleaseHero,	
		eLoServerStr_OkBuyWeaponSentence01,		
		eLoServerStr_TitleOkSell,
		eLoServerStr_DesOkReceivedSellCoin,
		eLoServerStr_TitleFailedSell,
		eLoServerStr_FailedSellLastestEquipment,
		eLoServerStr_TitleOkSceollSell,
		eLoServerStr_TitleFailedSceollSell,		
		eLoServerStr_OkBuyCoinSentence01,		
		eLoServerStr_OkBuyScrollSentence01,		
		eLoServerStr_DesOkBuyAccSentence01,
		eLoServerStr_TitleBuyItemNum,
			// command
		eLoServerStr_TitleDoingUpgrade,
		eLoServerStr_TitleUpgrade,
		eLoServerStr_TitleCommdSaveGameResultData,
		eLoServerStr_DesCommdSaveGameResultData,	
		eLoServerStr_TitleCommdGainLoReward,
		eLoServerStr_DesCommdGainLoReward,
		eLoServerStr_TitleCommdSaveLoOpenReward,
		eLoServerStr_DesCommdSaveLoOpenReward,
		eLoServerStr_TitleCommdGainLoQuest,
		eLoServerStr_DesCommdGainLoQuest,
		eLoServerStr_TitleCommdUpgradeCharLackLoEnegy,
		eLoServerStr_DesCommdUpgradeCharLackLoEnegy,
		eLoServerStr_TitleCommdReleaseTeamSlot,
		eLoServerStr_DesCommdReleaseTeamSlot,
		eLoServerStr_TitleDoingBuyItem,
		eLoServerStr_TitleBuyItem,
		eLoServerStr_TitleCommdSellScroll,
		eLoServerStr_DesCommdSellScroll,
		eLoServerStr_TitleDoingSellItem,
		eLoServerStr_TitleSellItem,
		eLoServerStr_TitleLoadUserScrollData,
		eLoServerStr_DesLoadUserScrollData,
		eLoServerStr_TitleLoadUserAccData,
		eLoServerStr_DesLoadUserAccData,
		eLoServerStr_TitleCheckContentUpdateInfor,
		eLoServerStr_DesCheckContentUpdateInfor,
		eLoServerStr_TitleSellItemNum,
		eLoServerStr_TitleCommdRelease,
		eLoServerStr_DesCommdRelease,
	}

	public enum eLoTitleSceneStr
	{
		eLoTitleSceneStr_None = 0,
		eLoTitleSceneStr_NoUserData,
		eLoTitleSceneStr_CreateNewUserData,
		eLoTitleSceneStr_WriteUserProfileOnServer,
		eLoTitleSceneStr_CompleteNewUserData,
		eLoTitleSceneStr_StartNewStroy,
		eLoTitleSceneStr_StepSuccessLoginThenCreateUserData,				
		eLoTitleSceneStr_StepOrganizeWeaponData,
		eLoTitleSceneStr_StepOrganizeCharacter,	
		eLoTitleSceneStr_StepOrganizeGameData,
		eLoTitleSceneStr_StepCheckGameVersion,
		eLoTitleSceneStr_StepGetUserDataAll,
		eLoTitleSceneStr_StepReleaseNewVesionDes01,
		eLoTitleSceneStr_StepFailedCheckVersionThenRestarGame,		
		eLoTitleSceneStr_Confirm,		
	}
}
