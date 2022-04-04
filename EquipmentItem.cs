using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItem : MonoBehaviour {
	public GameObject gTxtName = null, gTxtCurLevel = null, gTxtEffCount = null, gTxtEquipped = null, gPanGrade = null;
	public GameObject gLabelStatAttack = null, gTxtCurStatAttack = null, gLabelStatDef = null, gTxtCurStatDef = null;
	public GameObject gImgWeapon = null, gEquipedCharContainer = null, gImgEquippedChar = null;

	private Text mTxtName = null, mTxtCurLevel = null, mTxtEquipped = null, mTxtEffCount = null;
	private Text mLabelStatAttack = null, mTxtCurStatAttack = null, mLabelStatDef = null, mTxtCurStatDef = null;
	private Image mImgWeapon = null, mImgEquippedChar = null, mImgGrade = null;


	private int mWeaponType = 0, mEquippedCharType = 0;
	private LoWeaponDef.LoWeaponItem mWeaponItem;

	private void LinkEquipmentItemProperty()
	{
		if (mTxtName == null)
			mTxtName = gTxtName.GetComponent<Text> ();
		if (mTxtCurLevel == null)
			mTxtCurLevel = gTxtCurLevel.GetComponent<Text> ();
		if (mTxtEffCount == null)
			mTxtEffCount = gTxtEffCount.GetComponent<Text>();
		if (mImgWeapon == null)
			mImgWeapon = gImgWeapon.GetComponent<Image> ();
		if (mImgEquippedChar == null)
			mImgEquippedChar = gImgEquippedChar.GetComponent<Image> ();
		if (mTxtEquipped == null)
			mTxtEquipped = gTxtEquipped.GetComponent<Text> ();
		if(mLabelStatAttack == null)
			mLabelStatAttack = gLabelStatAttack.GetComponent<Text>();
		if (mTxtCurStatAttack == null)
			mTxtCurStatAttack = gTxtCurStatAttack.GetComponent<Text> ();
		if(mLabelStatDef == null)
			mLabelStatDef = gLabelStatDef.GetComponent<Text>();
		if (mTxtCurStatDef == null)
			mTxtCurStatDef = gTxtCurStatDef.GetComponent<Text> ();
		if(mImgGrade == null)
			mImgGrade = gPanGrade.transform.Find("imgGrade").GetComponent<Image>();
	}

	public void Start()
	{		
		LinkEquipmentItemProperty ();
	}

	public void SetWeaponInform(int nWeaponType, LoWeaponDef.LoWeaponItem WeaponItem)
	{
		LinkEquipmentItemProperty ();

		mWeaponType = nWeaponType;
		mWeaponItem = WeaponItem;

		TextAnchor taLevel = TextAnchor.MiddleRight, taEffCount = TextAnchor.MiddleLeft;
		RectTransform rtLevel = gTxtCurLevel.GetComponent<RectTransform>();
		RectTransform rtEffCount = gTxtEffCount.GetComponent<RectTransform>();
		Vector3 posLevel = new Vector3(rtLevel.localPosition.x, rtLevel.localPosition.y, rtLevel.localPosition.z),
            	posEffCount = new Vector3(rtEffCount.localPosition.x, rtEffCount.localPosition.y, rtEffCount.localPosition.z);
		RectTransform rtGrade = gPanGrade.GetComponent<RectTransform>();
		Vector3 posGrade = new Vector3(rtGrade.localPosition.x, rtGrade.localPosition.y, rtGrade.localPosition.z);

		switch (mWeaponType)
		{
			case LoWeaponDef.LoWeaponType.Weapon_Sword:				
			case LoWeaponDef.LoWeaponType.Weapon_LongSword:
			case LoWeaponDef.LoWeaponType.Weapon_Staff:
			case LoWeaponDef.LoWeaponType.Weapon_Wand:
			case LoWeaponDef.LoWeaponType.Weapon_Arrow:
			case LoWeaponDef.LoWeaponType.Weapon_Shield:
				break;
			case LoWeaponDef.LoWeaponType.Weapon_Bow:				
				taLevel = TextAnchor.MiddleLeft;
				taEffCount = TextAnchor.MiddleRight;
				posLevel.x = posLevel.x - 146;
				posEffCount.x = posEffCount.x + 176;
				posGrade.x = posGrade.x - 216;
				break;
						
			default:
				break;
		}
		mTxtCurLevel.alignment = taLevel;
		mTxtCurLevel.text = "Lv. " + WeaponItem.WeaponLevel;
		rtLevel.localPosition = posLevel;
		rtGrade.localPosition = posGrade;

        if(mWeaponItem.WeaponAddEffect != null && mWeaponItem.WeaponAddEffect.Count > 0){
			mTxtEffCount.alignment = taEffCount;
			mTxtEffCount.text = "add-" + mWeaponItem.WeaponAddEffect.Count;			
			rtEffCount.localPosition = posEffCount;
			gTxtEffCount.SetActive(true);
        }
        else{
			gTxtEffCount.SetActive(false);
        }

		mTxtName.text = LomlWealth.LomlWealthWeapon.GetWeaponTypeTitle ((LomlWealth.eLomlWealthWeaponIndex)mWeaponItem.WeaponIndex);		
		mImgWeapon.sprite = MgrAssetMainMenu.Instance.GetAssetWeaponSprite (mWeaponType, mWeaponItem.WeaponIndex);		
		mImgGrade.sprite = LoWeaponDef.LoWeaponGrade.GetGradeImageSprite(mWeaponItem.WeaponGrade);

		mLabelStatAttack.text = LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_AttackPower).SceString;
		mTxtCurStatAttack.text = LoWeaponStat.GetWeaponAttackPower (mWeaponItem).ToString();
		mLabelStatDef.text = LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_DefencePower).SceString;
		mTxtCurStatDef.text = LoWeaponStat.GetWeaponDefensivePower (mWeaponItem).ToString ();

		if (mWeaponType.Equals (LoWeaponDef.LoWeaponType.Weapon_Acc)) {
			// acc 같이 누구나 착용 가능한 아이템은 모든 캐릭터에서 장비중인지 조사해봐야한다.
			mEquippedCharType = LomlSsd.Instance.GetEquippedCharByAllCharCheck(nWeaponType, WeaponItem);
		} 
		else {
			mEquippedCharType = LomlSsd.Instance.GetEquippedCharByWeaponTypeAndWeaponItem (nWeaponType, WeaponItem);
		}

		if (mEquippedCharType.Equals (0)) {
			mTxtEquipped.text = LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_NotUsed).SceString;//"사용안됨";
			mTxtEquipped.color = new Color (1.0f, 0.294f, 0.294f, 1.0f);
			gEquipedCharContainer.SetActive (false);
		} else {
			mTxtEquipped.text = LoScenarioString.LoSceString.Instance.GetCommonString(LoScenarioString.eLoCommonStr.eLoCommStr_Equipped).SceString;//"착용중";
			mTxtEquipped.color = new Color (0.0f, 0.992f, 0.212f, 1.0f);
			gEquipedCharContainer.SetActive (true);
			//mImgEquippedChar.sprite = Resources.Load<Sprite> (LoModel.LoModelUtil.GetCharImgPathByCharType (mEquippedCharType)) as Sprite;
			mImgEquippedChar.sprite = MgrAssetMainMenu.Instance.GetCharSpriteRefByCharType(mEquippedCharType);
		}
	}
}
