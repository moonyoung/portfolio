using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusUI : MonoBehaviour {
	public GameObject gCommonUIHandle = null, gMgrCharacterUI = null, gCharStatusWorld = null;
	private CommonUIHandle mCommonUiHandle = null;
	private MgrCharacterUI mMgrCharacterUI = null;
	private CharStatusWorld mCharStatusWorld = null;

	public int mCurCharType = 0;		// 처리되고 있는 캐릭터 type.

	public GameObject gCharUpSkillRect = null, gCharEquipmentUIRect = null, gContainerEquip = null, gDlgEquipmentSet = null;
	public GameObject gObjRightEquippedItem = null, gObjLeftEquippedItem = null, gObjAcc01Item = null, gObjAcc02Item = null, gObjAcc03Item = null, gImgPreviousChar = null, gImgNextChar = null;
	public GameObject gObjCannotEquipmentCommonPopup = null;

	private Image mImgRightEquippedItem = null, mImgLeftEquippedItem = null, mImgAcc01Item = null, mImgAcc02Item = null, mImgAcc03Item = null, mImgPreviousChar = null, mImgNextChar = null;

	private LoPlayfabLomlUnit mCurUnitData = null;
	private LomlSsdUnitEquipment mCurUnitEquip = null;
	private LoWeaponDef.eWeaponMainHand mEWeaponMainHand = LoWeaponDef.eWeaponMainHand.eMainHand_BothAnother;


	#region for container equipment
	private ContainerEquip mContainerEquipment = null;
	#endregion

	private void LinkCharacterStatusUIProperty()
	{
		if(mImgRightEquippedItem == null)
			mImgRightEquippedItem = gObjRightEquippedItem.GetComponent<Image> ();
		if(mImgLeftEquippedItem == null)
			mImgLeftEquippedItem = gObjLeftEquippedItem.GetComponent<Image> ();
		if(mImgAcc01Item == null)
			mImgAcc01Item = gObjAcc01Item.GetComponent<Image> ();
		if(mImgAcc02Item == null)
			mImgAcc02Item = gObjAcc02Item.GetComponent<Image> ();
		if(mImgAcc03Item == null)
			mImgAcc03Item = gObjAcc03Item.GetComponent<Image> ();

		if (mImgPreviousChar == null)
			mImgPreviousChar = gImgPreviousChar.GetComponent<Image> ();

		if (mImgNextChar == null)
			mImgNextChar = gImgNextChar.GetComponent<Image> ();

		if(mCommonUiHandle == null)
			mCommonUiHandle = gCommonUIHandle.GetComponent<CommonUIHandle> ();

		if (mMgrCharacterUI == null)
			mMgrCharacterUI = gMgrCharacterUI.GetComponent<MgrCharacterUI> ();

		if (mCharStatusWorld == null)
			mCharStatusWorld = gCharStatusWorld.GetComponent<CharStatusWorld> ();
		
		if (gContainerEquip && mContainerEquipment == null) {			
			mContainerEquipment = gContainerEquip.GetComponent<ContainerEquip> ();
			gContainerEquip.SetActive (false);
		}
	}

	public void Start()
	{		
		LinkCharacterStatusUIProperty ();

		ReloadEditData (mCurCharType);

		SetEquippedItemImage (mCurCharType);
	}

	public void DeleCheckCommonPopup()
	{		
		gObjCannotEquipmentCommonPopup.SetActive (false);
	}

	public bool CheckAvailableWeapon(int nCharType, int nWeaponIndex)
	{		
		if (nWeaponIndex <= 0) {
			//gObjCannotEquipmentCommonPopup.SetActive (true);
			//gObjCannotEquipmentCommonPopup.GetComponent<popupCommon> ().SetPopupCommon ("Test", "Test Bsg", "확인", DeleCheckCommonPopup);
			return false;
		}
		return true;
	}

	public void CbBtnRightHandEquip()
	{		
		int nRightWeapon = mCurUnitEquip.RightWeapon;

		if ((LoWeaponDef.LoWeaponInfo.GetWeaponMainHandByCharType (mCurCharType).Equals (LoWeaponDef.eWeaponMainHand.eMainHand_BothSame) && nRightWeapon <= 0)) {			
			nRightWeapon = mCurUnitEquip.LeftWeapon;
		}

		if (CheckAvailableWeapon (mCurCharType, nRightWeapon).Equals (true)) {
			// 오른손인지 왼손인지에 따라, 셋팅되는 무기가 다른 캐릭이 있으므로 알맞은 종류로 셋팅해서 무리 리스트를 열게한다.
			Enter_ContainerEquip (mCurCharType, LoWeaponDef.eLoWeaponSlot.eLoWeaponSlot_Right, 
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharTypeAndIndex (mCurCharType, nRightWeapon));
		}
	}

	public void CbBtnLeftHandEquip()
	{
		int nLeftWeapon = mCurUnitEquip.LeftWeapon;

		if ((LoWeaponDef.LoWeaponInfo.GetWeaponMainHandByCharType (mCurCharType).Equals (LoWeaponDef.eWeaponMainHand.eMainHand_BothSame) &&
			nLeftWeapon <= 0))
			nLeftWeapon = mCurUnitEquip.RightWeapon;	

		if (CheckAvailableWeapon (mCurCharType, nLeftWeapon).Equals (true)) {
			Enter_ContainerEquip (mCurCharType, LoWeaponDef.eLoWeaponSlot.eLoWeaponSlot_Left,
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharTypeAndIndex (mCurCharType, nLeftWeapon));
		}
	}

	public void CbBtnAcc01()
	{
		Enter_ContainerEquip (mCurCharType, LoWeaponDef.eLoWeaponSlot.eLoWeaponSlot_Acc01, LoWeaponDef.LoWeaponType.Weapon_Acc);
	}

	public void CbBtnAcc02()
	{
		Enter_ContainerEquip (mCurCharType, LoWeaponDef.eLoWeaponSlot.eLoWeaponSlot_Acc02, LoWeaponDef.LoWeaponType.Weapon_Acc);
	}

	public void CbBtnAcc03()
	{
		Enter_ContainerEquip (mCurCharType, LoWeaponDef.eLoWeaponSlot.eLoWeaponSlot_Acc03, LoWeaponDef.LoWeaponType.Weapon_Acc);
	}

	public void CbBtnPreviousChar()
	{
		SetEditCharType (mMgrCharacterUI.GetPreviousCharTypeBaseOnCharType (mCurCharType));
		mCharStatusWorld.ReloadEditChar (mCurCharType);
	}

	public void CbBtnNextChar()
	{
		SetEditCharType (mMgrCharacterUI.GetNextCharTypeBaseOnCharType (mCurCharType));
		mCharStatusWorld.ReloadEditChar (mCurCharType);
	}

	public void ReloadEditData(int nCharType)
	{		
		if (nCharType != 0) {
			mCurUnitData = LoPlayfab.Instance.GetLomlUnitByCharType (nCharType);
			mCurUnitEquip = LomlSsd.Instance.GetUnitEquipmentByCharType (nCharType);
		}
	}

	public int SetEquippedItemImage(int nCharType)
	{
		if (mImgRightEquippedItem == null || nCharType <= 0) {			
			return -1;
		}
		
		mEWeaponMainHand = LoWeaponDef.LoWeaponInfo.GetWeaponMainHandByCharType (nCharType);

		switch (mEWeaponMainHand) {		
		case LoWeaponDef.eWeaponMainHand.eMainHand_BothAnother:		
			/*
			mImgRightEquippedItem.sprite = Resources.Load<Sprite> (LoWeaponDef.LoWeaponInfo.GetImgPathByWeaponType (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), mCurUnitEquip.RightWeapon)) as Sprite;			

			mImgLeftEquippedItem.sprite = Resources.Load<Sprite> (LoWeaponDef.LoWeaponInfo.GetImgPathByWeaponType (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Left), mCurUnitEquip.LeftWeapon)) as Sprite;
				*/
			mImgRightEquippedItem.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), mCurUnitEquip.RightWeapon);

			mImgLeftEquippedItem.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Left), mCurUnitEquip.LeftWeapon);
			break;
		case LoWeaponDef.eWeaponMainHand.eMainHand_BothSame:
			// ssd eupip 에는 양손무기여도 무기 보유갯수 처리를 위해 한쪽에만 무기가 설정되어 있다.
			// areminnok 과 darkknight 가 다름.
			// areminnok 같이 양손으로 들지만, 한쪽 손에만 무기 셋팅이 있는경우, 메인 무기 이미지 처리를 해준다.
			int nMainHandWeapon = mCurUnitEquip.RightWeapon;
			if (nMainHandWeapon <= 0)
				nMainHandWeapon = mCurUnitEquip.LeftWeapon;
			/*
			mImgRightEquippedItem.sprite = Resources.Load<Sprite>(LoWeaponDef.LoWeaponInfo.GetImgPathByWeaponType(
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType(mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), nMainHandWeapon)) as Sprite;
			mImgLeftEquippedItem.sprite = Resources.Load<Sprite>(LoWeaponDef.LoWeaponInfo.GetImgPathByWeaponType(
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType(mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), nMainHandWeapon)) as Sprite;
				*/
			mImgRightEquippedItem.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), nMainHandWeapon);
			mImgLeftEquippedItem.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), nMainHandWeapon);
			break;		
		case LoWeaponDef.eWeaponMainHand.eMainHand_RightOnly:
			/*
			mImgRightEquippedItem.sprite = Resources.Load<Sprite>(LoWeaponDef.LoWeaponInfo.GetImgPathByWeaponType(
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType(mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), mCurUnitEquip.RightWeapon)) as Sprite;
				*/
			mImgRightEquippedItem.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Right), mCurUnitEquip.RightWeapon);
			mImgLeftEquippedItem.sprite = MgrAssetMainMenu.Instance.GetMainMenuUI(208);
			break;	
		case LoWeaponDef.eWeaponMainHand.eMainHand_LeftOnly:
			mImgRightEquippedItem.sprite = MgrAssetMainMenu.Instance.GetMainMenuUI(208);
			/*
			mImgLeftEquippedItem.sprite = Resources.Load<Sprite>(LoWeaponDef.LoWeaponInfo.GetImgPathByWeaponType(
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType(mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Left), mCurUnitEquip.LeftWeapon)) as Sprite;
				*/
			mImgLeftEquippedItem.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (
				LoWeaponDef.LoWeaponInfo.GetWeaponTypeByCharType (mCurCharType, LoWeaponDef.eWeaponHand.eWeaponHand_Left), mCurUnitEquip.LeftWeapon);
			break;
		default:
			return -1;			
		}

		if (mCurUnitEquip.Acc01 > 0) {			
			gObjAcc01Item.SetActive (true);
			mImgAcc01Item.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (LoWeaponDef.LoWeaponType.Weapon_Acc, mCurUnitEquip.Acc01);				
		}
		else {
			gObjAcc01Item.SetActive (false);
		}
			

		if (mCurUnitEquip.Acc02 > 0) {			
			gObjAcc02Item.SetActive (true);
			mImgAcc02Item.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (LoWeaponDef.LoWeaponType.Weapon_Acc, mCurUnitEquip.Acc02);
		}
		else {
			gObjAcc02Item.SetActive (false);
		}

		if (mCurUnitEquip.Acc03 > 0) {			
			gObjAcc03Item.SetActive (true);
			mImgAcc03Item.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (LoWeaponDef.LoWeaponType.Weapon_Acc, mCurUnitEquip.Acc03);
		}
		else {
			gObjAcc03Item.SetActive (false);
		}

		// previous & next char image
		mImgPreviousChar.sprite = MgrAssetMainMenu.Instance.GetCharSpriteRefByCharType(mMgrCharacterUI.GetPreviousCharTypeBaseOnCharType(mCurCharType));
		mImgNextChar.sprite = MgrAssetMainMenu.Instance.GetCharSpriteRefByCharType(mMgrCharacterUI.GetNextCharTypeBaseOnCharType(mCurCharType));

		return 1;
	}

	public void SetEditCharType(int charType)
	{		
		LinkCharacterStatusUIProperty ();

		mCurCharType = charType;

		ReloadEditData (mCurCharType);
		SetEquippedItemImage (mCurCharType);

		gCharUpSkillRect.GetComponent<CharUpSkill> ().SetEditCharType (charType, gMgrCharacterUI.GetComponent<MgrCharacterUI>());
	}

	public int GetCurrentEditCharType()
	{				
		return mCurCharType;
	}

	/*
	 *Character Status UI 로 들어올때 UI activation 처리
	 */
	public void SelfActivationUIOnEnterCharacterStatusUI()
	{
		gCharUpSkillRect.SetActive (true);
		gCharEquipmentUIRect.SetActive (true);
		gContainerEquip.SetActive (false);
		gDlgEquipmentSet.SetActive (false);
	}

	#region for ContainerEquip
	public void Enter_ContainerEquip(int nCharType, LoWeaponDef.eLoWeaponSlot eWeaponSlot, int nWeaponType)
	{		
		if (mContainerEquipment) {
			gContainerEquip.SetActive (true);
			mContainerEquipment.LoadEquipListByCharType(nCharType, eWeaponSlot, nWeaponType);
			mCommonUiHandle.PushState (eMainMenuState.eContainerEquip);
		}
	}

	public void Leave_ContainerEquip()
	{
		if (mContainerEquipment) {
			gContainerEquip.SetActive (false);
		}
	}
	#endregion
}
