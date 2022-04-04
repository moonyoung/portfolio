using UnityEngine;
using LoWeaponDef;

public class SpellFire01 : LoRecycleData {
	private static float mSpellFire01Speed = 30f;
	private eLoWeaponMagicStatus meCurMagicStatus = eLoWeaponMagicStatus.eLoWeaponMagicStatus_None;
	public RunData refGameMainRunData = null;
	private ParticleSystem mSpellFire01Eff = null;
	private ParticleSystem mSpellFire01HitEffect = null;
	private Vector3 mVHitFire01MagicPos;
	private Vector3 mVStartPos, mVEndPos, mVCurPos;
	private bool mbShot = false, mbHitFire01Magic = false;
	private float mMoveFloat = 0.0f;
	private float mGameSpeed = 1.0f;
	private AttackData mAttData = null;

	void FixedUpdate () {
		if (IsEndUsed.Equals(false)){
			if(mbShot.Equals(true))
			{
				float dist = Vector3.Distance (transform.position, mVEndPos);

				if (dist < 1.5f) {				
					mbShot = false;
					mSpellFire01Eff.Stop(true);
					mSpellFire01Eff.gameObject.SetActive (false);

					refGameMainRunData.RemoveAllyAttackDataByAttData (mAttData);

					mbHitFire01Magic = true;
					mVHitFire01MagicPos = transform.position;
					mSpellFire01HitEffect.transform.position = mVHitFire01MagicPos;
					mSpellFire01HitEffect.Simulate(0.0f, true, true);
					mSpellFire01HitEffect.Play (true);

				} else {		
					mMoveFloat = Time.deltaTime * mSpellFire01Speed * mGameSpeed;
					transform.position = Vector3.MoveTowards(transform.position, mVEndPos, mMoveFloat);
					transform.LookAt (mVEndPos);
				}
			}
			if (mbHitFire01Magic.Equals (true)) {			
				if (!mSpellFire01HitEffect.isPlaying) {
					mSpellFire01HitEffect.Stop(true);					
					gameObject.SetActive(false);
					IsEndUsed = true;
				}
				mSpellFire01HitEffect.transform.position = mVHitFire01MagicPos;
			}
		}
	}

	public void ShotMagic(LoModel.eLoModelCharType eCharType, float fEndPosX, float fEndPosY, float fEndPosZ, int nAttPower)
	{
		mVStartPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		mVEndPos = new Vector3 (fEndPosX, fEndPosY, fEndPosZ);	

		mbShot = true;
		meCurMagicStatus = eLoWeaponMagicStatus.eLoWeaponMagicStatus_Shoted;
		mAttData = refGameMainRunData.AddAllyAttackData (transform.GetComponent<SphereCollider>().GetInstanceID(), eCharType, nAttPower);

		mSpellFire01Eff.Play(true);			
	}

	public bool IsShoted()
	{
		if (meCurMagicStatus.Equals (eLoWeaponMagicStatus.eLoWeaponMagicStatus_Shoted))
			return true;
		return false;
	}

	// 재활용되는 경우, 모든 변수 초기화 
    private void InitForRecycleModel()
    {
		mSpellFire01Speed = 30f;
		meCurMagicStatus = eLoWeaponMagicStatus.eLoWeaponMagicStatus_None;
		mbShot = false;
		mbHitFire01Magic = false;
		mMoveFloat = 0.0f;
		mGameSpeed = 1.0f;
    }

	public void SetInitData(ref RunData gameMainRunData, float fGameSpeed)
	{
		InitForRecycleModel();

		mSpellFire01Eff = transform.Find("Blast01_fire").GetComponent<ParticleSystem> ();
		mSpellFire01HitEffect = transform.Find("Hit01_fire").GetComponent<ParticleSystem> ();

		refGameMainRunData = gameMainRunData;
		mGameSpeed = fGameSpeed;

		ParticleSystem.MainModule psMain = mSpellFire01Eff.main;
		psMain.simulationSpeed = mGameSpeed;
		mSpellFire01Eff.gameObject.SetActive(true);
		mSpellFire01Eff.Simulate(0.0f, true, false);

		psMain = mSpellFire01HitEffect.main;
		psMain.simulationSpeed = mGameSpeed;
		mSpellFire01HitEffect.Simulate(0.0f, true, false);

		gameObject.SetActive(true);	// LoRecycleMode 을 사용한 변경으로 인해 재사용의 경우 마지막이 SetActivie(false) 이다.
	}
}
