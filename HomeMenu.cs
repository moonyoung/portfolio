using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour {
	private GameMain mRefGameMain = null;
	public GameObject gRefGameMain = null, gCommonUIHandle = null;
	private CommonUIHandle mCommonUiHandle = null;

	public GameObject gMainMenuObj = null, gTopMenuObj = null, gBottomMenuObj = null;

	public GameObject gMgrSettings = null, gWorldMapObj = null, gPanStageDlg = null, gMgrCharacter = null, gMgrQuest = null, gMgrRewardMenu = null, gMgrSummons = null, gMgrMakeWeapon = null, gMgrScroll = null;

	public GameObject gWeaponManager = null;

	public GameObject gStoreObj = null, gMainMenuServer = null;

	public GameObject gMgrRewardAD = null;

#if UNITY_ANDROID
	private GameObject mExitGameYesNoDlg = null; 
#endif

	public void BtnBack(eMainMenuState eCurState, eMainMenuState eNextState)
	{	
		switch (eCurState) {
		case eMainMenuState.eSettings:
			if(gMgrSettings){
				gMgrSettings.GetComponent<MgrSettings>().Leave_MgrSetting();
				gMgrSettings.SetActive(false);
			}
			break;

		case eMainMenuState.eStore:
			if(gBottomMenuObj)
				gBottomMenuObj.SetActive(true);
			if(gStoreObj){
				gStoreObj.SetActive(false);				
			}
			break;
			
		case eMainMenuState.eManageCharacter:
			if (gBottomMenuObj)
				gBottomMenuObj.SetActive (true);
			if (gMgrCharacter) {			
				gMgrCharacter.SetActive (false);
				gMgrCharacter.GetComponent<MgrCharacterMenu> ().ReturnMainMenu ();
			}
			break;

		case eMainMenuState.eManageScroll:
			if(gBottomMenuObj)
				gBottomMenuObj.SetActive(true);
			if(gMgrScroll){
				gMgrScroll.SetActive(false);
				gMgrScroll.GetComponent<MgrScrollMenu>().ReturnMainMenu();
			}
			break;

		case eMainMenuState.eQuest:
			if(gBottomMenuObj)
				gBottomMenuObj.SetActive(true);
			if(gMgrQuest){
				gMgrQuest.SetActive(false);
				gMgrQuest.GetComponent<MgrQuestMenu>().ReturnMainMenu();
			}
			break;

        case eMainMenuState.eReward:
            if (gBottomMenuObj)
                gBottomMenuObj.SetActive(true);
            if (gMgrRewardMenu) {
                gMgrRewardMenu.SetActive(false);
                gMgrRewardMenu.GetComponent<MgrRewardMenu>().ReturnMainMenu();
            }
            break;

        case eMainMenuState.eManageSummons:
			if(gBottomMenuObj)
				gBottomMenuObj.SetActive(true);
			if(gMgrSummons){
				gMgrSummons.SetActive(false);
				gMgrSummons.GetComponent<MgrSummonsMenu>().ReturnMainMenu();
			}
			break;

		case eMainMenuState.eManageMakeWeapon:			
			if(gBottomMenuObj)
				gBottomMenuObj.SetActive(true);
			if(gMgrMakeWeapon){
				gMgrMakeWeapon.SetActive(false);
				gMgrMakeWeapon.GetComponent<MgrMakeWeapon>().ReturnMainMenu();
			}
			break;

		case eMainMenuState.eManageWeapon:
			if (gBottomMenuObj)
				gBottomMenuObj.SetActive (true);
			if (gWeaponManager) {			
				gWeaponManager.SetActive (false);
			}
			break;

		case eMainMenuState.eWorldMap:
			if (gBottomMenuObj)
				gBottomMenuObj.SetActive (true);
			if (gWorldMapObj) {								
				gPanStageDlg.SetActive (false);
				gWorldMapObj.SetActive (false);
				gWorldMapObj.GetComponent<WorldMapMenu> ().LeaveWorldMap ();
			}
			break;

		case eMainMenuState.eAdvertisementPlaySpeed:
			if(gMgrRewardAD){
				gMgrRewardAD.SetActive(false);
				gMgrRewardAD.GetComponent<LoRewardAd>().Leave_AD(eMainMenuState.eAdvertisementPlaySpeed);
			}
			break;
		
		default:
			break;
		}
		// MainMenu 진입시 보여야 할 공통 UI Show.
		Debug.Log("=============== Show ShowOnMainMenuUI 공통 UI 보여줌.");
		ShowOnMainMenuUI();
	}

#region for enter module callback then pressed Button.
	public void cbBtnSettings()
	{	
		if(mCommonUiHandle.EnableCommonUI().Equals(true)){
			PlaySFXBtnPress(1);
			if(gMgrSettings){
				gMgrSettings.SetActive(true);
				gMgrSettings.GetComponent<MgrSettings>().Enter_MgrSetting();
			}
			HideOnMainMenuUI();
			mCommonUiHandle.SetActiveBackButton ();
			mCommonUiHandle.PushState (eMainMenuState.eSettings);
		}		
	}

	public void cbBtnStore()
	{		
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive (false);
		if (gStoreObj) {
			gStoreObj.SetActive (true);
			gStoreObj.GetComponent<LomlStore> ().InitStore ();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.SetActiveBackButton ();
		mCommonUiHandle.PushState (eMainMenuState.eStore);
	}

	public void cbBtnWorldMap()
	{
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive (false);
		if (gWorldMapObj) {
			gWorldMapObj.SetActive (true);
			gWorldMapObj.GetComponent<WorldMapMenu> ().FreshWorldMap ();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.SetActiveBackButton ();
		mCommonUiHandle.PushState (eMainMenuState.eWorldMap);
	}

	public void cbBtnQuest()
	{
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive(false);
		if(gMgrQuest){
			gMgrQuest.SetActive(true);
			gMgrQuest.GetComponent<MgrQuestMenu>().Enter_MgrQuestMenu();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.PushState(eMainMenuState.eQuest);
	}

    public void cbBtnReward()
    {
        PlaySFXBtnPress(1);
        if (gBottomMenuObj)
            gBottomMenuObj.SetActive(false);
        if (gMgrRewardMenu){
            gMgrRewardMenu.SetActive(true);
            gMgrRewardMenu.GetComponent<MgrRewardMenu>().Enter_MgrRewardMenu();
        }
		HideOnMainMenuUI();
        mCommonUiHandle.PushState(eMainMenuState.eReward);
    }

	public void cbBtnManageCharacter()
	{		
		PlaySFXBtnPress(1);		
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive (false);
		if (gMgrCharacter) {			
			gMgrCharacter.SetActive (true);
			gMgrCharacter.GetComponent<MgrCharacterMenu> ().Enter_MgrCharacterUI ();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.PushState (eMainMenuState.eManageCharacter);
	}

	public void cbBtnManageWeapon()
	{
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive (false);
		if (gWeaponManager) {			
			gWeaponManager.SetActive (true);
			gWeaponManager.GetComponent<WeaponManager> ().LoadWeaponManager ();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.PushState (eMainMenuState.eManageWeapon);
	}

	public void cbBtnMakeWeapon()
	{		
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive(false);
		if(gMgrMakeWeapon){
			gMgrMakeWeapon.SetActive(true);
			gMgrMakeWeapon.GetComponent<MgrMakeWeapon>().Enter_MgrMakeWeaponUI();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.PushState(eMainMenuState.eManageMakeWeapon);
	}

	public void cbBtnSummons()
	{
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive(false);
		if(gMgrSummons){
			gMgrSummons.SetActive(true);
			gMgrSummons.GetComponent<MgrSummonsMenu>().Enter_MgrSummonsMenu();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.PushState(eMainMenuState.eManageSummons);
	}

	public void cbBtnMgrScroll()
	{
		PlaySFXBtnPress(1);
		if(gBottomMenuObj)
			gBottomMenuObj.SetActive(false);
		if(gMgrScroll){
			gMgrScroll.SetActive(true);
			gMgrScroll.GetComponent<MgrScrollMenu>().EnterMgrScroll();
		}
		HideOnMainMenuUI();
		mCommonUiHandle.PushState(eMainMenuState.eManageScroll);		
	}

	/* 광고 처럼, 한 광고 메뉴에서 여러개의 다른 타입의 광고 시나리오로 분리하는 경우, 모듈 처리는 각 모듈에서 하고,
	   main menu 의 MenuState 관리를 위해 home menu 의 함수들을 불러야하는 경우를 위해 각 광고별로 만들어 호출하도록 한다.
	*/
	public void EnterPlaySpeedMode2X()
	{
		PlaySFXBtnPress(1);		
		HideOnMainMenuUI();	// 호출은 하되 다시 광고만 살려준다.
		if(gMgrRewardAD)
			gMgrRewardAD.SetActive(true);
		mCommonUiHandle.PushState(eMainMenuState.eAdvertisementPlaySpeed);
	}
#endregion

	// MainMenu 위의 흩뿌려져 있는 UI 들에 대한, 메뉴 진입시 show 처리
	public void ShowOnMainMenuUI()
	{
		if(gMgrRewardAD){
			Debug.Log("===>> Call HomeMenu. Show On AD UI");
			gMgrRewardAD.GetComponent<LoRewardAd>().ShowLoRewardADasChecking();
		}
	}
	// MainMenu 위의 흩뿌려져 있는 UI 들에 대한, 메뉴 진입시 hide 처리
	public void HideOnMainMenuUI()
	{
		if(gMgrRewardAD)
			gMgrRewardAD.SetActive(false);
	}

	// 처음 main menu 진입시 UI 초기셋팅
	private void InitUIObject()
	{
		InitUITextMultiLanguage();

		gMainMenuObj.SetActive (true);
		gTopMenuObj.SetActive (true);
		gBottomMenuObj.SetActive (true);		
		gMgrSettings.SetActive(false);
		gWeaponManager.SetActive (false);
		gStoreObj.SetActive (false);

		if(gMgrSummons){
			gMgrSummons.SetActive(false);
			gMgrSummons.GetComponent<MgrSummonsMenu>().OffSummonWorld();
		}
		if (gMgrCharacter) {
			gMgrCharacter.SetActive (false);
			gMgrCharacter.GetComponent<MgrCharacterMenu> ().OffCharStatusWorld();
		}
		if(gMgrMakeWeapon){
			gMgrMakeWeapon.SetActive(false);						
			gMgrMakeWeapon.GetComponent<MgrMakeWeapon>().OffForgeWorld();
		}
		HideOnMainMenuUI();

	#if UNITY_ANDROID
		LinkPropertyExitGameScenarioForAndroid();
	#endif
	}

	private void Awake()
	{
		// aspect ratio 에 따른 canvas matchWidthOrHeight 설정. 
		LoSettings.Instance.SetScreenMatchWithAspectRatio(GameObject.Find("Canvas").GetComponent<CanvasScaler>());	

		gRefGameMain = GameObject.Find ("GameMain");
		mRefGameMain = gRefGameMain.GetComponent<GameMain> ();		
		LoadAssetBundle ();			
	}

	public void Start()
	{		
		// Load String
		LoScenarioString.LoSceString.Instance.LoadLomlString(
				LoScenarioString.LoSceString.STR_LOAD_COMMON | LoScenarioString.LoSceString.STR_LOAD_WEAPON | 
				LoScenarioString.LoSceString.STR_LOAD_MAINMENU_GAME | LoScenarioString.LoSceString.STR_LOAD_MAINMENU |
				LoScenarioString.LoSceString.STR_LOAD_SERVER | LoScenarioString.LoSceString.STR_LOAD_CHARNAME);
		//LoadAssetBundle ();		
		mCommonUiHandle = gCommonUIHandle.GetComponent<CommonUIHandle> ();
		mCommonUiHandle.PushState (eMainMenuState.eMainMenu);

		InitUIObject ();

		Debug.Log("Play BGM ,in Start Home Menu");		
		mRefGameMain.SoundManager.PlayBGM("mainmenu_theme01");
	}

	void FixedUpdate () {
		if (mRefGameMain.LoControlScenario.Equals (LoScenario.eLoScenarioControl.eLsControl_Control)) {
			ProcessCustomScenario ();
		} 
		#if UNITY_ANDROID
		if(Input.GetKeyDown(KeyCode.Escape)){		// android
		//if(Input.GetKeyDown(KeyCode.Space)){		// unity test
			if(mCommonUiHandle.PeekState().Equals(eMainMenuState.eMainMenu)){				
				mExitGameYesNoDlg.SetActive(true);
			}
			else{				
				mCommonUiHandle.cbBtnBack();
			}
		}		
		#endif
	}

	private void LoadAssetBundle()
	{
		MgrAssetCommon.Instance.LoadAllCommonRes();
		MgrAssetMainMenu.Instance.LoadAllMainMenuRes ();
		mRefGameMain.SoundManager.InitAudioSource();	// 한씬당 거의 한번만 해주면 된다. 이외의 케이스는 시나리오대로 따로 처리.
		mRefGameMain.SoundManager.ReLoadAudioResource(eLoSoundScenario.eLoSoundSce_Common);
		mRefGameMain.SoundManager.ReLoadAudioResource(eLoSoundScenario.eLoSoundSce_MainMenu);
	}

	public void ProcessCustomScenario()
	{		
		switch (mRefGameMain.GetCurScenario ()) {

		default:
			break;
		}
	}

	#region Sound Effect
	private void PlaySFXBtnPress(int nType){
		switch(nType){
		case 1:
			mRefGameMain.SoundManager.PlaySFX ("sdBtnPress01", false, 0.7f);
			break;
		default:
			mRefGameMain.SoundManager.PlaySFX ("sdBtnPress01", false, 1);
			break;
		}
	}
	#endregion

	#region Text Multi Language
	public string GetCommonString(LoScenarioString.eLoCommonStr eCommonStrId)
	{
		return LoScenarioString.LoSceString.Instance.GetCommonString(eCommonStrId).SceString;		
	}

	private void InitUITextMultiLanguage()
	{
		Transform trCanvas 		= GameObject.Find("Canvas").transform;
		Transform trMainMenu 	= trCanvas.Find("MainMenu");		
		LoadUITextMultiLanguage_BottomMenu(trMainMenu.Find("BottomMenu"));	
		LoadUITextMultiLanguage_MgrCharacter(trCanvas.Find("MgrCharacter"));		
	}

	private void LoadUITextMultiLanguage_BottomMenu(Transform trBottomMenu)
	{
		LoScenarioString.LoSceString instLoString = LoScenarioString.LoSceString.Instance;
		Transform trTmp = null;
		trTmp = trBottomMenu.Find("BtnStore");
		trTmp.Find("txtStoreShadow").GetComponent<Text>().text 	= 
		trTmp.Find("txtStore").GetComponent<Text>().text 		= 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_Store).SceString;
		
		trTmp = trBottomMenu.Find("BtnManageCharacter");
		trTmp.Find("txtManageTeamShadow").GetComponent<Text>().text =
		trTmp.Find("txtManageTeam").GetComponent<Text>().text 		=
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_ManageTeam).SceString;

		trTmp = trBottomMenu.Find("BtnManageWeapon");
		trTmp.Find("txtManageWeaponShadow").GetComponent<Text>().text	=
		trTmp.Find("txtManageWeapon").GetComponent<Text>().text			=
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_ManageWeapon).SceString;

		trTmp = trBottomMenu.Find("BtnMakeWeapon");
		trTmp.Find("txtMakeWeapon").GetComponent<Text>().text 		=
		trTmp.Find("txtMakeWeaponShadow").GetComponent<Text>().text =
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_MakeWeapon).SceString; 

		trTmp = trBottomMenu.Find("BtnReward");
		trTmp.Find("txtRewardShadow").GetComponent<Text>().text	=
		trTmp.Find("txtReward").GetComponent<Text>().text		=
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_Reward).SceString;

		trTmp = trBottomMenu.Find("BtnSummons");
		trTmp.Find("txtSummonsShadow").GetComponent<Text>().text =
		trTmp.Find("txtSummons").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_Summons).SceString;

		trTmp = trBottomMenu.Find("BtnMgrScroll");
		trTmp.Find("txtMgrScrollShadow").GetComponent<Text>().text	=
		trTmp.Find("txtMgrScroll").GetComponent<Text>().text = instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_MgrScroll).SceString;

		trBottomMenu.Find("BtnWorldMap").Find("txtWorldMap").GetComponent<Text>().text	=
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_BottomMenu_WorldMap).SceString;			
	}

	private void LoadUITextMultiLanguage_MgrCharacter(Transform trMgrCharacter)
	{
		LoScenarioString.LoSceString instLoString = LoScenarioString.LoSceString.Instance;
		Transform trTmp2 = null;
		Transform trMgrCharacterUI = trMgrCharacter.Find("MgrCharaterUI");
		Transform trCharacterStatusUI = trMgrCharacter.Find("CharacterStatusUI");

		Transform trTmp = trMgrCharacterUI.Find("teamConstructRect");
		trTmp.Find("bgTitle").Find("Text").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_ConstructTitle).SceString;
		trTmp.Find("teamChar02").Find("btnNotyetBatch").Find("txtNotyetBatch").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_NotyetBatch).SceString;
		trTmp.Find("teamChar03").Find("btnNotyetBatch").Find("txtNotyetBatch").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_NotyetBatch).SceString;
		trTmp.Find("teamChar04").Find("btnNotyetBatch").Find("txtNotyetBatch").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_NotyetBatch).SceString;
		trTmp.Find("teamChar05").Find("btnNotyetBatch").Find("txtNotyetBatch").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_NotyetBatch).SceString;
		trTmp.Find("teamChar06").Find("btnNotyetBatch").Find("txtNotyetBatch").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_NotyetBatch).SceString;
		
		trTmp = trMgrCharacterUI.Find("PanelMyCharactersList");
		trTmp.Find("imgTitleBg").Find("txtTitle").GetComponent<Text>().text =
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_MgrChar_PossessionHeroTitle).SceString;

		// CharUpSkillRect
		trTmp = trCharacterStatusUI.Find("CharUpSkillRect");
		trTmp2 = trTmp.Find("CharUpgradePan");
		trTmp2.Find("imgStatHp").Find("labelStatHp").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_StatHpTitle).SceString;
		trTmp2.Find("imgStatAttack").Find("labelStatAttack").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_StatAttackTitle).SceString;
		trTmp2.Find("imgStatDefence").Find("labelStatDefence").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_StatDefenceTitle).SceString;
		trTmp2.Find("imgStatRecuperativePower").Find("labelStatRecuPower").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_StatRecuperativePowerTitle).SceString;
		trTmp2.Find("imgStatExt").Find("labelStatExt").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_StatExt).SceString;	
		trTmp2 = trTmp.Find("CharSkill");
		trTmp2.Find("panCharSkillTitle").Find("txtCharSkillTitle").GetComponent<Text>().text = 
			instLoString.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_MainMenu_SkillTitle).SceString;		

	}

	#endregion

#if UNITY_ANDROID
	#region for android
	private void LinkPropertyExitGameScenarioForAndroid()
	{
		Button btnExitGameYes = null, btnExitGameNo = null;
		if(mExitGameYesNoDlg == null){
			mExitGameYesNoDlg = GameObject.Find("Canvas").transform.Find("CommonTopLayerUI").Find("panExitGameYesNoDlg").gameObject;
		}
		mExitGameYesNoDlg.transform.Find("panDlg").Find("bgYesNoTitle").Find("txtYesNoTitle").GetComponent<Text>().text = 
			LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_System_ExitGameTitle).SceString;
		mExitGameYesNoDlg.transform.Find("txtYesNoMsg").GetComponent<Text>().text = 
			LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_System_ExitGameDes).SceString;
		mExitGameYesNoDlg.transform.Find("btnYes").Find("txtMsgShadow").GetComponent<Text>().text = 
		mExitGameYesNoDlg.transform.Find("btnYes").Find("txtMsg").GetComponent<Text>().text = 
			LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_Yes).SceString;
		mExitGameYesNoDlg.transform.Find("btnNo").Find("txtMsgShadow").GetComponent<Text>().text = 
		mExitGameYesNoDlg.transform.Find("btnNo").Find("txtMsg").GetComponent<Text>().text = 
			LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_No).SceString;

		btnExitGameYes = mExitGameYesNoDlg.transform.Find("btnYes").GetComponent<Button>();
		btnExitGameNo = mExitGameYesNoDlg.transform.Find("btnNo").GetComponent<Button>();
		btnExitGameYes.onClick.RemoveAllListeners();
		btnExitGameYes.onClick.AddListener(delegate { CbBtnGameExitYes(); });
		btnExitGameNo.onClick.RemoveAllListeners();
		btnExitGameNo.onClick.AddListener(delegate { CbBtnGameExitNo(); });
		mExitGameYesNoDlg.SetActive(false);
	}
	public void CbBtnGameExitYes(){		
		Application.Quit();
	}
	public void CbBtnGameExitNo(){
		mExitGameYesNoDlg.SetActive(false);
	}
	#endregion
#endif

}
