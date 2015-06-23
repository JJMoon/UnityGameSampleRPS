//----------------------------------------------
//            Appsgraphy : PsykickBattle
// Copyright © 2012-2013 Developer MOON, LJK 
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class MenuManager : AmSceneBase
{
	public GameObject mBtnKick, mBtnKeeper, mBtnShirts, mBtnSocks, mBtnPants, mPlayerKicker, mPlayerKeeper, mSlider, mShirtItem, mAg_Player, mSelectLineBar, mKBall, mTrappingBall, mBackBtn, mMenuTeamEdit, mLineEditMenu;
	public bool mSelectedKeeper = false, mPartPants, mPartsShirts, mPartsGlove, mPartsSocks;
	Texture2D mKickerShirts, mKickerPants, mKickerSocks, mKeeperShirts, mKeeperPants, mKeeperSocks, mKeeperGlove, mItemTex;
	AmTexture mShirt, mPants, mSocks, mGkShirt, mGkPants, mGkSocks;
	public AmTexture mGlShirt, mGlPants, mGlShoes, mGlSocks, mGlGlove;
	public ProceduralMaterial mProcedureMat;
	ProceduralMaterial[] matKickerShirts, matPants, matSocks, matKeeperShirts;
	Material MyShirts, MyPants, MySocks;
	Object[] subKickerShirts, subKeeperShirts, subPants, subSocks;
	Object[] EnemysubKickerShirts, EnemysubKeeperShirts, EnemysubPants, EnemysubSocks;
	Texture2D Tex, Tex2;
	Color mco;
	bool mkeeperMode;
	//object[] subKeeperShirts, subKickerShirts, subPants, subSocks;
	
	void Btn_Fun_ShirsType1 ()
	{
        UniformPoint = 0;
        //UniformMainPopupSetting ("4%","0%","0%");
		if (mkeeperMode) {

			mProcedureMat = (ProceduralMaterial)subKeeperShirts [0];
			dicMenuList ["Btn_Fun_Kp_ShirsType1"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_Kp_ShirsType1"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = 1;
			uniformTypeid = "KeeperUniformTop1";
			TextureBuyButtonClose (uniformTypeid);
			
			
		} else {

			dicMenuList ["Btn_Fun_ShirsType1"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_ShirsType1"].GetComponent<UICheckbox> ().Set (true);
			mProcedureMat = (ProceduralMaterial)subKickerShirts [0];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = 1;
			uniformTypeid = "KickerUniformTop1";
			TextureBuyButtonClose (uniformTypeid);
		}
		//        Debug.Log ("2_1");
		ShirtsSetColor ();
	}
	
	void Btn_Fun_ShirsType2 ()
	{
        UniformPoint = 4;
		//UniformMainPopupSetting ("4%","0%","0%");
		if (mkeeperMode) {

			mProcedureMat = (ProceduralMaterial)subKeeperShirts [1];
			dicMenuList ["Btn_Fun_Kp_ShirsType2"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_Kp_ShirsType2"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = 2;
			uniformTypeid = "KeeperUniformTop2";
			TextureBuyButtonClose (uniformTypeid);
			
		} else {

			mProcedureMat = (ProceduralMaterial)subKickerShirts [1];
			dicMenuList ["Btn_Fun_ShirsType2"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_ShirsType2"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = 2;
			uniformTypeid = "KickerUniformTop2";
			TextureBuyButtonClose (uniformTypeid);
			
		}
		ShirtsSetColor ();
		
		
	}
	
	void Btn_Fun_ShirsType3 ()
	{
        UniformPoint = 4;
		//UniformMainPopupSetting ("4%","0%","0%");
		if (mkeeperMode) {

			uniformTypeid = "KeeperUniformTop3";
			mProcedureMat = (ProceduralMaterial)subKeeperShirts [2];
			dicMenuList ["Btn_Fun_Kp_ShirsType3"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_Kp_ShirsType3"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = 3;
			TextureBuyButtonClose (uniformTypeid);
			
		} else {

			uniformTypeid = "KickerUniformTop3";
			mProcedureMat = (ProceduralMaterial)subKickerShirts [2];
			dicMenuList ["Btn_Fun_ShirsType3"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_ShirsType3"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = 3;
			TextureBuyButtonClose (uniformTypeid);
		}
		ShirtsSetColor ();
	}
	
	void Btn_Fun_ShirsType4 ()
	{
        UniformPoint = 4;
		//UniformMainPopupSetting ("4%","0%","0%");
		if (mkeeperMode) {

			uniformTypeid = "KeeperUniformTop4";
			mProcedureMat = (ProceduralMaterial)subKeeperShirts [3];
			dicMenuList ["Btn_Fun_Kp_ShirsType4"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_Kp_ShirsType4"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = 4;
			TextureBuyButtonClose (uniformTypeid);
			
		} else {
			uniformTypeid = "KickerUniformTop4";
			mProcedureMat = (ProceduralMaterial)subKickerShirts [3];
			dicMenuList ["Btn_Fun_ShirsType4"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_ShirsType4"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = 4;
			TextureBuyButtonClose (uniformTypeid);
		}
		
		ShirtsSetColor ();
		
	}
	
	void Btn_Fun_ShirsType5 ()
	{
        UniformPoint = 4;
		//UniformMainPopupSetting ("4%","0%","0%");
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformTop5";
			mProcedureMat = (ProceduralMaterial)subKeeperShirts [4];
			dicMenuList ["Btn_Fun_Kp_ShirsType5"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_Kp_ShirsType5"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = 5;
			TextureBuyButtonClose (uniformTypeid);
			
		} else {
			uniformTypeid = "KickerUniformTop5";
			mProcedureMat = (ProceduralMaterial)subKickerShirts [4];
			dicMenuList ["Btn_Fun_ShirsType5"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_ShirsType5"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = 5;
			TextureBuyButtonClose (uniformTypeid);
		}
		
		ShirtsSetColor ();
		
	}
	
	void Btn_Fun_ShirsType6 ()
	{
        UniformPoint = 4;
		//UniformMainPopupSetting ("4%","0%","0%");
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformTop6";
			mProcedureMat = (ProceduralMaterial)subKeeperShirts [5];
			dicMenuList ["Btn_Fun_Kp_ShirsType6"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_Kp_ShirsType6"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = 6;
			TextureBuyButtonClose (uniformTypeid);
			
		} else {
			uniformTypeid = "KickerUniformTop6";
			mProcedureMat = (ProceduralMaterial)subKickerShirts [5];
			dicMenuList ["Btn_Fun_ShirsType6"].GetComponent<UICheckbox> ().isChecked = true;
			dicMenuList ["Btn_Fun_ShirsType6"].GetComponent<UICheckbox> ().Set (true);
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = mProcedureMat.mainTexture;
			Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = 6;
			TextureBuyButtonClose (uniformTypeid);
		}
		
		ShirtsSetColor ();
	}
	//---------------------------------------------------------- Pants
	void Btn_Fun_PantsType1 ()
	{
        PantPoint = 0;
		//UniformMainPopupSetting ("3%","0%","0%");
		dicMenuList ["Btn_Fun_PantsType1"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_PantsType1"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformPants1";
			Ag.mySelf.arrUniform [0].Keep.Pants.Texture = 1;
			mProcedureMat = (ProceduralMaterial)EnemysubPants [0];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformPants1";
			Ag.mySelf.arrUniform [0].Kick.Pants.Texture = 1;
			mProcedureMat = (ProceduralMaterial)subPants [0];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		PantsSetColor ();
	}
	
	void Btn_Fun_PantsType2 ()
	{
        PantPoint = 3;
		//UniformMainPopupSetting ("3%","0%","0%");
		dicMenuList ["Btn_Fun_PantsType2"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_PantsType2"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformPants2";
			Ag.mySelf.arrUniform [0].Keep.Pants.Texture = 2;
			mProcedureMat = (ProceduralMaterial)EnemysubPants [1];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformPants2";
			Ag.mySelf.arrUniform [0].Kick.Pants.Texture = 2;
			mProcedureMat = (ProceduralMaterial)subPants [1];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		PantsSetColor ();
	}
	
	void Btn_Fun_PantsType3 ()
	{
        PantPoint = 3;
		//UniformMainPopupSetting ("3%","0%","0%");
		dicMenuList ["Btn_Fun_PantsType3"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_PantsType3"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformPants3";
			Ag.mySelf.arrUniform [0].Keep.Pants.Texture = 3;
			mProcedureMat = (ProceduralMaterial)EnemysubPants [2];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformPants3";
			Ag.mySelf.arrUniform [0].Kick.Pants.Texture = 3;
			mProcedureMat = (ProceduralMaterial)subPants [2];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		PantsSetColor ();
	}
	
	void Btn_Fun_PantsType4 ()
	{
        PantPoint = 3;
		//UniformMainPopupSetting ("3%","0%","0%");
		dicMenuList ["Btn_Fun_PantsType4"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_PantsType4"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformPants4";
			Ag.mySelf.arrUniform [0].Keep.Pants.Texture = 4;
			mProcedureMat = (ProceduralMaterial)EnemysubPants [3];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformPants4";
			Ag.mySelf.arrUniform [0].Kick.Pants.Texture = 4;
			mProcedureMat = (ProceduralMaterial)subPants [3];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		PantsSetColor ();
	}
	
	void Btn_Fun_PantsType5 ()
	{
        PantPoint = 3;
		//UniformMainPopupSetting ("3%","0%","0%");
		dicMenuList ["Btn_Fun_PantsType5"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_PantsType5"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformPants5";
			Ag.mySelf.arrUniform [0].Keep.Pants.Texture = 5;
			mProcedureMat = (ProceduralMaterial)EnemysubPants [4];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformPants5";
			Ag.mySelf.arrUniform [0].Kick.Pants.Texture = 5;
			mProcedureMat = (ProceduralMaterial)subPants [4];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		PantsSetColor ();
	}
	
	void Btn_Fun_PantsType6 ()
	{
        PantPoint = 3;
		//UniformMainPopupSetting ("3%","0%","0%");
		dicMenuList ["Btn_Fun_PantsType6"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_PantsType6"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformPants6";
			Ag.mySelf.arrUniform [0].Keep.Pants.Texture = 6;
			mProcedureMat = (ProceduralMaterial)EnemysubPants [5];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformPants6";
			Ag.mySelf.arrUniform [0].Kick.Pants.Texture = 6;
			mProcedureMat = (ProceduralMaterial)subPants [5];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		PantsSetColor ();
	}
	//---------------------------------------------------------- Socks
	void Btn_Fun_SocksType1 ()
	{
        SockPoint = 0;
		//UniformMainPopupSetting ("2%","0%","0%");
		dicMenuList ["Btn_Fun_SocksType1"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_SocksType1"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformSocks1";
			Ag.mySelf.arrUniform [0].Keep.Socks.Texture = 1;
			mProcedureMat = (ProceduralMaterial)EnemysubSocks [0];
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformSocks1";
			Ag.mySelf.arrUniform [0].Kick.Socks.Texture = 1;
			mProcedureMat = (ProceduralMaterial)subSocks [0];
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		SocksSetColor ();
		
	}
	
	void Btn_Fun_SocksType2 ()
	{   
        SockPoint = 2;
		//UniformMainPopupSetting ("2%","0%","0%");
		dicMenuList ["Btn_Fun_SocksType2"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_SocksType2"].GetComponent<UICheckbox> ().Set (true);
		
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformSocks2";
			mProcedureMat = (ProceduralMaterial)EnemysubSocks [1];
			Ag.mySelf.arrUniform [0].Keep.Socks.Texture = 2;
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformSocks2";
			mProcedureMat = (ProceduralMaterial)subSocks [1];
			Ag.mySelf.arrUniform [0].Kick.Socks.Texture = 2;
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		SocksSetColor ();
		
	}
	
	void Btn_Fun_SocksType3 ()
	{
        SockPoint = 2;
		//UniformMainPopupSetting ("2%","0%","0%");
		dicMenuList ["Btn_Fun_SocksType3"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_SocksType3"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformSocks3";
			mProcedureMat = (ProceduralMaterial)EnemysubSocks [2];
			Ag.mySelf.arrUniform [0].Keep.Socks.Texture = 3;
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformSocks3";
			mProcedureMat = (ProceduralMaterial)subSocks [2];
			Ag.mySelf.arrUniform [0].Kick.Socks.Texture = 3;
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		SocksSetColor ();
		
	}
	
	void Btn_Fun_SocksType4 ()
	{
        SockPoint = 2;
		//UniformMainPopupSetting ("2%","0%","0%");
		dicMenuList ["Btn_Fun_SocksType4"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_SocksType4"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformSocks4";
			mProcedureMat = (ProceduralMaterial)EnemysubSocks [3];
			Ag.mySelf.arrUniform [0].Keep.Socks.Texture = 4;
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformSocks4";
			mProcedureMat = (ProceduralMaterial)subSocks [3];
			Ag.mySelf.arrUniform [0].Kick.Socks.Texture = 4;
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		
		SocksSetColor ();
	}
	
	void Btn_Fun_SocksType5 ()
	{
        SockPoint = 2;
		//UniformMainPopupSetting ("2%","0%","0%");
		dicMenuList ["Btn_Fun_SocksType5"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_SocksType5"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformSocks5";
			mProcedureMat = (ProceduralMaterial)EnemysubSocks [4];
			Ag.mySelf.arrUniform [0].Keep.Socks.Texture = 5;
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformSocks5";
			mProcedureMat = (ProceduralMaterial)subSocks [4];
			Ag.mySelf.arrUniform [0].Kick.Socks.Texture = 5;
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		SocksSetColor ();
		
	}
	
	void Btn_Fun_SocksType6 ()
	{
        SockPoint = 2;
		//UniformMainPopupSetting ("2%","0%","0%");
		dicMenuList ["Btn_Fun_SocksType6"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_SocksType6"].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			uniformTypeid = "KeeperUniformSocks6";
			mProcedureMat = (ProceduralMaterial)EnemysubSocks [5];
			Ag.mySelf.arrUniform [0].Keep.Socks.Texture = 6;
			mPlayerKeeper.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		} else {
			uniformTypeid = "KickerUniformSocks6";
			mProcedureMat = (ProceduralMaterial)subSocks [5];
			Ag.mySelf.arrUniform [0].Kick.Socks.Texture = 6;
			mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture = mProcedureMat.mainTexture;
			TextureBuyButtonClose (uniformTypeid);
		}
		SocksSetColor ();
		
		
	}
	
	void ReadmyTexture ()
	{
		try {
			//**------------------------------------Test /07 - 03
			MyShirts = (Material)Resources.Load ("Materials/MyShirts");
			MyPants = (Material)Resources.Load ("Materials/MyPants");
			MySocks = (Material)Resources.Load ("Materials/MySocks");
			
			subKeeperShirts = Resources.LoadAll ("Textures/Substance/Keepershirts", typeof(ProceduralMaterial));
			subKickerShirts = Resources.LoadAll ("Textures/Substance/Kickershirts", typeof(ProceduralMaterial));
			subPants = Resources.LoadAll ("Textures/Substance/Pants", typeof(ProceduralMaterial));
			subSocks = Resources.LoadAll ("Textures/Substance/Socks", typeof(ProceduralMaterial));
			
			EnemysubKeeperShirts = Resources.LoadAll ("Textures/EnemySubstance/Keepershirts", typeof(ProceduralMaterial));
			EnemysubKickerShirts = Resources.LoadAll ("Textures/EnemySubstance/Kickershirts", typeof(ProceduralMaterial));
			EnemysubPants = Resources.LoadAll ("Textures/EnemySubstance/Pants", typeof(ProceduralMaterial));
			EnemysubSocks = Resources.LoadAll ("Textures/EnemySubstance/Socks", typeof(ProceduralMaterial));
		} catch {
			Debug.Log ("Uniform Error");
		}
		
	}
	//----------------------------------------------------------
	void PantsChange ()
	{ 
		UniformType = 2;
		AllShirtsPantsSocksSubmenuClose ();
		dicMenuList ["scroll_pants"].SetActive (true);
		int PantsNum = 0, PantMainColor = 0, PantsSubColor = 0;
		if (mkeeperMode) {
			PantsNum = Ag.mySelf.arrUniform [0].Keep.Pants.Texture;
			PantMainColor = Ag.mySelf.arrUniform [0].Keep.Pants.ColMain;
			PantsSubColor = Ag.mySelf.arrUniform [0].Keep.Pants.ColSub;
		} else {
			PantsNum = Ag.mySelf.arrUniform [0].Kick.Pants.Texture;
			PantMainColor = Ag.mySelf.arrUniform [0].Kick.Pants.ColMain;
			PantsSubColor = Ag.mySelf.arrUniform [0].Kick.Pants.ColSub;
		}
		
		switch (PantsNum) {
		case 1:
			Btn_Fun_PantsType1 ();
			break;
		case 2:
			Btn_Fun_PantsType2 ();
			break;
		case 3:
			Btn_Fun_PantsType3 ();
			break;
		case 4:
			Btn_Fun_PantsType4 ();
			break;
		case 5:
			Btn_Fun_PantsType5 ();
			break;
		case 6:
			Btn_Fun_PantsType6 ();
			break;
		}
		
		switch (PantMainColor) {
		case 0:
			Btn_Fun_MainColor0 ();
			break;
		case 1:
			Btn_Fun_MainColor1 ();
			break;
		case 2:
			Btn_Fun_MainColor2 ();
			break;
		case 3:
			Btn_Fun_MainColor3 ();
			break;
		case 4:
			Btn_Fun_MainColor4 ();
			break;
		case 5:
			Btn_Fun_MainColor5 ();
			break;
		case 6:
			Btn_Fun_MainColor6 ();
			break;
		case 7:
			Btn_Fun_MainColor7 ();
			break;
		case 8:
			Btn_Fun_MainColor8 ();
			break;
		case 9:
			Btn_Fun_MainColor9 ();
			break;
		case 10:
			Btn_Fun_MainColor10 ();
			break;
		case 11:
			Btn_Fun_MainColor11 ();
			break;
		}
		
		switch (PantsSubColor) {
		case 0:
			Btn_Fun_LineColor0 ();
			break;
		case 1:
			Btn_Fun_LineColor1 ();
			break;
		case 2:
			Btn_Fun_LineColor2 ();
			break;
		case 3:
			Btn_Fun_LineColor3 ();
			break;
		case 4:
			Btn_Fun_LineColor4 ();
			break;
		case 5:
			Btn_Fun_LineColor5 ();
			break;
		case 6:
			Btn_Fun_LineColor6 ();
			break;
		case 7:
			Btn_Fun_LineColor7 ();
			break;
		case 8:
			Btn_Fun_LineColor8 ();
			break;
		case 9:
			Btn_Fun_LineColor9 ();
			break;
		case 10:
			Btn_Fun_LineColor10 ();
			break;
		case 11:
			Btn_Fun_LineColor11 ();
			break;
		}
		
		
		
	}
	
	void ShirtsChange ()
	{
		UniformType = 1;
		//        Debug.Log ("Shirt" + Ag.mySelf.arrUniform [0].Kick.Shirt.Texture);
		//        Debug.Log ("Shirt" + Ag.mySelf.arrUniform [0].Keep.Shirt.Texture);
		//        Debug.Log ("Pants" + Ag.mySelf.arrUniform [0].Kick.Pants.Texture);
		//        Debug.Log ("Pants" + Ag.mySelf.arrUniform [0].Keep.Pants.Texture);
		//        Debug.Log ("Socks" + Ag.mySelf.arrUniform [0].Kick.Socks.Texture);
		//        Debug.Log ("Socks" + Ag.mySelf.arrUniform [0].Keep.Socks.Texture);
		
		AllShirtsPantsSocksSubmenuClose ();
		int shirtsNum = 0, shirtMainColor = 0, shirtSubColor = 0;
		if (mkeeperMode) {
			shirtsNum = Ag.mySelf.arrUniform [0].Keep.Shirt.Texture;
			shirtMainColor = Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain;
			shirtSubColor = Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub;
			dicMenuList ["scroll_topgk"].SetActive (true);
			
		} else {
			shirtsNum = Ag.mySelf.arrUniform [0].Kick.Shirt.Texture;
			shirtMainColor = Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain;
			shirtSubColor = Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub;
			dicMenuList ["scroll_top"].SetActive (true);
		}
		
		switch (shirtsNum) {
		case 1:
			Btn_Fun_ShirsType1 ();
			break;
		case 2:
			Btn_Fun_ShirsType2 ();
			break;
		case 3:
			Btn_Fun_ShirsType3 ();
			break;
		case 4:
			Btn_Fun_ShirsType4 ();
			break;
		case 5:
			Btn_Fun_ShirsType5 ();
			break;
		case 6:
			Btn_Fun_ShirsType6 ();
			break;
		}
		
		switch (shirtMainColor) {
		case 0:
			Btn_Fun_MainColor0 ();
			break;
		case 1:
			Btn_Fun_MainColor1 ();
			break;
		case 2:
			Btn_Fun_MainColor2 ();
			break;
		case 3:
			Btn_Fun_MainColor3 ();
			break;
		case 4:
			Btn_Fun_MainColor4 ();
			break;
		case 5:
			Btn_Fun_MainColor5 ();
			break;
		case 6:
			Btn_Fun_MainColor6 ();
			break;
		case 7:
			Btn_Fun_MainColor7 ();
			break;
		case 8:
			Btn_Fun_MainColor8 ();
			break;
		case 9:
			Btn_Fun_MainColor9 ();
			break;
		case 10:
			Btn_Fun_MainColor10 ();
			break;
		case 11:
			Btn_Fun_MainColor11 ();
			break;
		}
		
		switch (shirtSubColor) {
		case 0:
			Btn_Fun_LineColor0 ();
			break;
		case 1:
			Btn_Fun_LineColor1 ();
			break;
		case 2:
			Btn_Fun_LineColor2 ();
			break;
		case 3:
			Btn_Fun_LineColor3 ();
			break;
		case 4:
			Btn_Fun_LineColor4 ();
			break;
		case 5:
			Btn_Fun_LineColor5 ();
			break;
		case 6:
			Btn_Fun_LineColor6 ();
			break;
		case 7:
			Btn_Fun_LineColor7 ();
			break;
		case 8:
			Btn_Fun_LineColor8 ();
			break;
		case 9:
			Btn_Fun_LineColor9 ();
			break;
		case 10:
			Btn_Fun_LineColor10 ();
			break;
		case 11:
			Btn_Fun_LineColor11 ();
			break;
		}
		
	}
	
	void ShirtsSetColor ()
	{
		//        Debug.Log (Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain + "");
		//        Debug.Log (Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain + "");
		//        Debug.Log (Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain + "");
		//        Debug.Log (mkeeperMode + "KeeperMode");
		int shirtMainColor = 0, shirtSubColor = 0;
		if (mkeeperMode) {
			shirtMainColor = Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain;
			shirtSubColor = Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub;
		} else {
			shirtMainColor = Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain;
			shirtSubColor = Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub;
			
		}
		switch (shirtMainColor) {
		case 0:
			Btn_Fun_MainColor0 ();
			break;
		case 1:
			Btn_Fun_MainColor1 ();
			break;
		case 2:
			Btn_Fun_MainColor2 ();
			break;
		case 3:
			Btn_Fun_MainColor3 ();
			break;
		case 4:
			Btn_Fun_MainColor4 ();
			break;
		case 5:
			Btn_Fun_MainColor5 ();
			break;
		case 6:
			Btn_Fun_MainColor6 ();
			break;
		case 7:
			Btn_Fun_MainColor7 ();
			break;
		case 8:
			Btn_Fun_MainColor8 ();
			break;
		case 9:
			Btn_Fun_MainColor9 ();
			break;
		case 10:
			Btn_Fun_MainColor10 ();
			break;
		case 11:
			Btn_Fun_MainColor11 ();
			break;
		}
		
		switch (shirtSubColor) {
		case 0:
			Btn_Fun_LineColor0 ();
			break;
		case 1:
			Btn_Fun_LineColor1 ();
			break;
		case 2:
			Btn_Fun_LineColor2 ();
			break;
		case 3:
			Btn_Fun_LineColor3 ();
			break;
		case 4:
			Btn_Fun_LineColor4 ();
			break;
		case 5:
			Btn_Fun_LineColor5 ();
			break;
		case 6:
			Btn_Fun_LineColor6 ();
			break;
		case 7:
			Btn_Fun_LineColor7 ();
			break;
		case 8:
			Btn_Fun_LineColor8 ();
			break;
		case 9:
			Btn_Fun_LineColor9 ();
			break;
		case 10:
			Btn_Fun_LineColor10 ();
			break;
		case 11:
			Btn_Fun_LineColor11 ();
			break;
		}
		
	}
	
	void PantsSetColor ()
	{
		int PantsMainColor = 0, PantsSubColor = 0;
		if (mkeeperMode) {
			PantsMainColor = Ag.mySelf.arrUniform [0].Keep.Pants.ColMain;
			PantsSubColor = Ag.mySelf.arrUniform [0].Keep.Pants.ColSub;
			
			
		} else {
			PantsMainColor = Ag.mySelf.arrUniform [0].Kick.Pants.ColMain;
			PantsSubColor = Ag.mySelf.arrUniform [0].Kick.Pants.ColSub;
			
		}
		switch (PantsMainColor) {
		case 0:
			Btn_Fun_MainColor0 ();
			break;
		case 1:
			Btn_Fun_MainColor1 ();
			break;
		case 2:
			Btn_Fun_MainColor2 ();
			break;
		case 3:
			Btn_Fun_MainColor3 ();
			break;
		case 4:
			Btn_Fun_MainColor4 ();
			break;
		case 5:
			Btn_Fun_MainColor5 ();
			break;
		case 6:
			Btn_Fun_MainColor6 ();
			break;
		case 7:
			Btn_Fun_MainColor7 ();
			break;
		case 8:
			Btn_Fun_MainColor8 ();
			break;
		case 9:
			Btn_Fun_MainColor9 ();
			break;
		case 10:
			Btn_Fun_MainColor10 ();
			break;
		case 11:
			Btn_Fun_MainColor11 ();
			break;
		}
		
		switch (PantsSubColor) {
		case 0:
			Btn_Fun_LineColor0 ();
			break;
		case 1:
			Btn_Fun_LineColor1 ();
			break;
		case 2:
			Btn_Fun_LineColor2 ();
			break;
		case 3:
			Btn_Fun_LineColor3 ();
			break;
		case 4:
			Btn_Fun_LineColor4 ();
			break;
		case 5:
			Btn_Fun_LineColor5 ();
			break;
		case 6:
			Btn_Fun_LineColor6 ();
			break;
		case 7:
			Btn_Fun_LineColor7 ();
			break;
		case 8:
			Btn_Fun_LineColor8 ();
			break;
		case 9:
			Btn_Fun_LineColor9 ();
			break;
		case 10:
			Btn_Fun_LineColor10 ();
			break;
		case 11:
			Btn_Fun_LineColor11 ();
			break;
		}
		
	}
	
	void SocksSetColor ()
	{
		
		int socksMainColor = 0, socksSubColor = 0;
		if (mkeeperMode) {
			
			socksMainColor = Ag.mySelf.arrUniform [0].Keep.Socks.ColMain;
			socksSubColor = Ag.mySelf.arrUniform [0].Keep.Socks.ColSub;
		} else {
			
			socksMainColor = Ag.mySelf.arrUniform [0].Kick.Socks.ColMain;
			socksSubColor = Ag.mySelf.arrUniform [0].Kick.Socks.ColSub;
		}
		switch (socksMainColor) {
		case 0:
			Btn_Fun_MainColor0 ();
			break;
		case 1:
			Btn_Fun_MainColor1 ();
			break;
		case 2:
			Btn_Fun_MainColor2 ();
			break;
		case 3:
			Btn_Fun_MainColor3 ();
			break;
		case 4:
			Btn_Fun_MainColor4 ();
			break;
		case 5:
			Btn_Fun_MainColor5 ();
			break;
		case 6:
			Btn_Fun_MainColor6 ();
			break;
		case 7:
			Btn_Fun_MainColor7 ();
			break;
		case 8:
			Btn_Fun_MainColor8 ();
			break;
		case 9:
			Btn_Fun_MainColor9 ();
			break;
		case 10:
			Btn_Fun_MainColor10 ();
			break;
		case 11:
			Btn_Fun_MainColor11 ();
			break;
		}
		
		switch (socksSubColor) {
		case 0:
			Btn_Fun_LineColor0 ();
			break;
		case 1:
			Btn_Fun_LineColor1 ();
			break;
		case 2:
			Btn_Fun_LineColor2 ();
			break;
		case 3:
			Btn_Fun_LineColor3 ();
			break;
		case 4:
			Btn_Fun_LineColor4 ();
			break;
		case 5:
			Btn_Fun_LineColor5 ();
			break;
		case 6:
			Btn_Fun_LineColor6 ();
			break;
		case 7:
			Btn_Fun_LineColor7 ();
			break;
		case 8:
			Btn_Fun_LineColor8 ();
			break;
		case 9:
			Btn_Fun_LineColor9 ();
			break;
		case 10:
			Btn_Fun_LineColor10 ();
			break;
		case 11:
			Btn_Fun_LineColor11 ();
			break;
		}
	}
	
	void SocksChange ()
	{
		UniformType = 3;
		int socksNum = 0, socksMainColor = 0, socksSubColor = 0;
		
		AllShirtsPantsSocksSubmenuClose ();
		dicMenuList ["scroll_socks"].SetActive (true);
		
		if (mkeeperMode) {
			socksNum = Ag.mySelf.arrUniform [0].Keep.Socks.Texture;
			socksMainColor = Ag.mySelf.arrUniform [0].Keep.Socks.ColMain;
			socksSubColor = Ag.mySelf.arrUniform [0].Keep.Socks.ColSub;
		} else {
			socksNum = Ag.mySelf.arrUniform [0].Kick.Socks.Texture;
			socksMainColor = Ag.mySelf.arrUniform [0].Kick.Socks.ColMain;
			socksSubColor = Ag.mySelf.arrUniform [0].Kick.Socks.ColSub;
		}
		
		switch (socksNum) {
		case 1:
			Btn_Fun_SocksType1 ();
			break;
		case 2:
			Btn_Fun_SocksType2 ();
			break;
		case 3:
			Btn_Fun_SocksType3 ();
			break;
		case 4:
			Btn_Fun_SocksType4 ();
			break;
		case 5:
			Btn_Fun_SocksType5 ();
			break;
		case 6:
			Btn_Fun_SocksType6 ();
			break;
		}
		
		switch (socksMainColor) {
		case 0:
			Btn_Fun_MainColor0 ();
			break;
		case 1:
			Btn_Fun_MainColor1 ();
			break;
		case 2:
			Btn_Fun_MainColor2 ();
			break;
		case 3:
			Btn_Fun_MainColor3 ();
			break;
		case 4:
			Btn_Fun_MainColor4 ();
			break;
		case 5:
			Btn_Fun_MainColor5 ();
			break;
		case 6:
			Btn_Fun_MainColor6 ();
			break;
		case 7:
			Btn_Fun_MainColor7 ();
			break;
		case 8:
			Btn_Fun_MainColor8 ();
			break;
		case 9:
			Btn_Fun_MainColor9 ();
			break;
		case 10:
			Btn_Fun_MainColor10 ();
			break;
		case 11:
			Btn_Fun_MainColor11 ();
			break;
		}
		
		switch (socksSubColor) {
		case 0:
			Btn_Fun_LineColor0 ();
			break;
		case 1:
			Btn_Fun_LineColor1 ();
			break;
		case 2:
			Btn_Fun_LineColor2 ();
			break;
		case 3:
			Btn_Fun_LineColor3 ();
			break;
		case 4:
			Btn_Fun_LineColor4 ();
			break;
		case 5:
			Btn_Fun_LineColor5 ();
			break;
		case 6:
			Btn_Fun_LineColor6 ();
			break;
		case 7:
			Btn_Fun_LineColor7 ();
			break;
		case 8:
			Btn_Fun_LineColor8 ();
			break;
		case 9:
			Btn_Fun_LineColor9 ();
			break;
		case 10:
			Btn_Fun_LineColor10 ();
			break;
		case 11:
			Btn_Fun_LineColor11 ();
			break;
		}
		
		//Btn_Fun_SocksType1 ();
	}
	
	bool TopFlag, PantsFlag, SocksFlag;
	int UniformType;
	string uniformTypeid = "";
	
	void Btn_Fun_Bundle_Top ()
	{
		//        Debug.Log ("1-1");
		UniformType = 1;
		
		
		ShirtsChange ();
		dicMenuList ["btn_00top"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["btn_00top"].GetComponent<UICheckbox> ().Set (true);
	}
	
	void Btn_Fun_Bundle_Pants ()
	{
		
		
		UniformType = 2;
		PantsChange ();
		dicMenuList ["btn_01pants"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["btn_01pants"].GetComponent<UICheckbox> ().Set (true);
	}
	
	void Btn_Fun_Bundle_Socks ()
	{
		
		UniformType = 3;
		SocksChange ();
		dicMenuList ["btn_02socks"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["btn_02socks"].GetComponent<UICheckbox> ().Set (true);
		
	}
	//---------------------------------------------------------- KickerMode
	List<AmUniform> BeforeUniform = new List<AmUniform> ();
	AmUniform BeforeSetUniform = new AmUniform ();
	//AmUniform BeforeUniform = new AmUniform ();
	bool mIskicker;
	
	void Btn_Fun_KickerMode ()
	{
		UniformEventCheckKicker();
		PutonNotbuyUniform ();
		KickerUniformPrice ();
		//BeforeUniform = Ag.mySelf.arrUniform [0];
		mIskicker = true;
		mkeeperMode = false;
		DestroyObject (mPlayerKeeper);
		if (mPlayerKicker == null) {
			mPlayerKicker = (GameObject)Instantiate ((GameObject)Resources.Load ("CeremonyCharacter/CereMonyKicker"));
			mPlayerKicker.AddComponent<SpinWithMouse> ().target = mPlayerKicker.transform;
			mPlayerKicker.AddComponent<CapsuleCollider> ().center = new Vector3 (0, 0.3f, 0);
			mPlayerKicker.GetComponent<CapsuleCollider> ().radius = 0.22f;
			mPlayerKicker.GetComponent<CapsuleCollider> ().height = 0.69f;
		}
		
		mPlayerKicker.transform.parent = dicMenuList ["LPanel_uniform"].transform;
		/*
        mPlayerKicker.transform.localPosition = new Vector3 (-276.6582f, -188.2901f, 76f);
        mPlayerKicker.transform.eulerAngles = new Vector3 (0, 182.368f, 0);
        mPlayerKicker.transform.localScale = new Vector3 (566f, 566f, 566f);
        
        */
		
		mPlayerKicker.transform.localPosition = new Vector3 (-331.7963f, -143.0316f, -57.6532f);
		mPlayerKicker.transform.eulerAngles = new Vector3 (0, 178.8739f, 0);
		mPlayerKicker.transform.localScale = new Vector3 (500f, 500f, 500f);
		mPlayerKicker.animation.Play ();
		
		
		//mProcedureMat = (ProceduralMaterial)subKickerShirts [0];
		dicMenuList ["Btn_Fun_KickerMode"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_KickerMode"].GetComponent<UICheckbox> ().Set (true);
		
		
		TextureSet (mIskicker);
		SocksChange ();
		PantsChange ();
		StartCoroutine (ShirtUpdate ());
		//Btn_Fun_Bundle_Top ();
		
		//mPlayerKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture = (ProceduralMaterial) subKickerShirts[0];
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_itemstat/Label_intext", true).gameObject.GetComponent<UILabel>().text = WWW.UnEscapeURL("%EA%B0%80%20%20%20%20%EC%82%B0%20%20%20%20%EC%A0%90%3A%0A%ED%8C%8C%20%EC%9D%B4%20%EC%96%B4%20%20%ED%82%A5%3A%0A%EB%B8%94%EB%A0%88%EC%9D%B4%EC%A6%88%ED%82%A5%3A");

	}
	
	IEnumerator ShirtUpdate ()
	{
		yield return new WaitForSeconds (0.1f);
		//        Debug.Log ("ShirtUpdate");
		Btn_Fun_Bundle_Top ();
	}
	//---------------------------------------------------------- KeeperMode
	void Btn_Fun_KeeperMode ()
	{

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_uniform/bundle_itemstat/Label_intext", true).gameObject.GetComponent<UILabel>().text = WWW.UnEscapeURL("%EA%B0%80%EC%82%B0%EC%A0%90%3A%0A%ED%94%8C%EB%9E%98%EC%89%AC%EC%A0%90%ED%94%84%3A%0A%EB%9D%BC%EC%9D%B4%ED%8A%B8%EB%8B%9D%EC%A0%90%ED%94%84%3A");
		UniformEventCheckKeeper();
		PutonNotbuyUniform ();
		KeeperUniformPrice ();
		//BeforeUniform = Ag.mySelf.arrUniform [0];
		mIskicker = false;
		Debug.Log ("1");
		mkeeperMode = true;
		DestroyObject (mPlayerKicker);
		if (mPlayerKeeper == null) {
			mPlayerKeeper = (GameObject)Instantiate ((GameObject)Resources.Load ("CeremonyCharacter/CereMonyKeeper"));
			mPlayerKeeper.AddComponent<SpinWithMouse> ().target = mPlayerKeeper.transform;
			mPlayerKeeper.AddComponent<CapsuleCollider> ().center = new Vector3 (0, 0.3f, 0);
			mPlayerKeeper.GetComponent<CapsuleCollider> ().radius = 0.22f;
			mPlayerKeeper.GetComponent<CapsuleCollider> ().height = 0.69f;
		}
		
		mPlayerKeeper.transform.parent = dicMenuList ["LPanel_uniform"].transform;
		mPlayerKeeper.transform.localPosition = new Vector3 (-331.7963f, -143.0316f, -57.6532f);
		mPlayerKeeper.transform.eulerAngles = new Vector3 (0, 178.8739f, 0);
		mPlayerKeeper.transform.localScale = new Vector3 (504.557f, 504.557f, 504.557f);
		mPlayerKeeper.animation.Play ();
		
		
		
		
		dicMenuList ["Btn_Fun_KeeperMode"].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_KeeperMode"].GetComponent<UICheckbox> ().Set (true);
		
		TextureSet (mIskicker);
		SocksChange ();
		PantsChange ();
		//Btn_Fun_Bundle_Top ();
		StartCoroutine (ShirtUpdate ());
		
		
	}
	
	bool UniformFlag;
	
	void PlayerInfo (string good, string perpeft, string Score) {
		dicMenuList ["Label_score"].GetComponent<UILabel>().text = good+" %";
		dicMenuList ["Label_good"].GetComponent<UILabel>().text = perpeft+" %";
		dicMenuList ["Label_great"].GetComponent<UILabel>().text = "";
	}
	
	
	bool PutonNotbuyUniform ()
	{
		
		int UniformNumber = 0;
		for (int i = 0; i < Ag.mySelf.arrUniform.Count; i++) {
			if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == "KickerUniformTop" + Ag.mySelf.arrUniform [0].Kick.Shirt.Texture)
				UniformNumber++;
			if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == "KeeperUniformTop" + Ag.mySelf.arrUniform [0].Keep.Shirt.Texture)
				UniformNumber++;
			if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == "KickerUniformPants" + Ag.mySelf.arrUniform [0].Kick.Pants.Texture)
				UniformNumber++;
			if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == "KeeperUniformPants" + Ag.mySelf.arrUniform [0].Keep.Pants.Texture)
				UniformNumber++;
			if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == "KickerUniformSocks" + Ag.mySelf.arrUniform [0].Kick.Socks.Texture)
				UniformNumber++;
			if (Ag.mySelf.arrUniform [i].WAS.itemTypeId == "KeeperUniformSocks" + Ag.mySelf.arrUniform [0].Keep.Pants.Texture)
				UniformNumber++;
		}
		if (UniformNumber < 6) {
			MenuCommonOpen ("popup_uniformalert", "Ui_popup", true);
			UniformFlag = false;
		} 
		if (UniformNumber == 6) { 
			
			UNiformUpdate ();
			//BeforeUniform =  Ag.mySelf.arrUniform [0];
			BeforeSetUniform.Kick.Shirt.Texture = Ag.mySelf.arrUniform [0].Kick.Shirt.Texture;
			BeforeSetUniform.Keep.Shirt.Texture = Ag.mySelf.arrUniform [0].Keep.Shirt.Texture;
			BeforeSetUniform.Kick.Pants.Texture = Ag.mySelf.arrUniform [0].Kick.Pants.Texture;
			BeforeSetUniform.Keep.Pants.Texture = Ag.mySelf.arrUniform [0].Keep.Pants.Texture;
			BeforeSetUniform.Kick.Socks.Texture = Ag.mySelf.arrUniform [0].Kick.Socks.Texture;
			BeforeSetUniform.Keep.Socks.Texture = Ag.mySelf.arrUniform [0].Keep.Socks.Texture;
			UniformFlag = true;
		}
		return UniformFlag;
		
	}
	
	void ReturnBeforeUniform ()
	{
		Ag.mySelf.arrUniform [0].Kick.Shirt.Texture = BeforeSetUniform.Kick.Shirt.Texture;
		Ag.mySelf.arrUniform [0].Keep.Shirt.Texture = BeforeSetUniform.Keep.Shirt.Texture;
		Ag.mySelf.arrUniform [0].Kick.Pants.Texture = BeforeSetUniform.Kick.Pants.Texture;
		Ag.mySelf.arrUniform [0].Keep.Pants.Texture = BeforeSetUniform.Keep.Pants.Texture;
		Ag.mySelf.arrUniform [0].Kick.Socks.Texture = BeforeSetUniform.Kick.Socks.Texture;
		Ag.mySelf.arrUniform [0].Keep.Socks.Texture = BeforeSetUniform.Keep.Socks.Texture;
	}
	
	void UniformIgnore ()
	{
		if (!Ag.Uniform) {
			ReturnBeforeUniform ();
			
			if (mkeeperMode) {
				//ReturnBeforeUniform ();
				Btn_Fun_KeeperMode ();
				
			} else {
				//ReturnBeforeUniform ();
				Btn_Fun_KickerMode ();
			}
		}
		
		if (Ag.Uniform) {
			if (mkeeperMode) {
				ReturnBeforeUniform ();
				Btn_Fun_KickerMode ();
				
			} else {
				ReturnBeforeUniform ();
				Btn_Fun_KeeperMode ();
			}
		}
		Ag.Uniform = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox3_uniform", true).GetComponent<UICheckbox>().isChecked = true;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "Panel_teamback/bundle_tap/checkbox3_uniform", true).GetComponent<UICheckbox>().Set(true);
		MenuCommonOpen ("popup_uniformalert", "Ui_popup", false);
	}

    void SortBtnInitSet () {
        Sortbyflag = false;
        SortBygrade = false;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_leftbtn_lineup/btn_sortgrade", true).GetComponent<UICheckbox>().isChecked = false;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_leftbtn_lineup/btn_sortgrade", true).GetComponent<UICheckbox>().isChecked = false;

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_leftbtn_lineup/btn_sortstat", true).GetComponent<UICheckbox>().isChecked = false;
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_leftbtn_lineup/btn_sortstat", true).GetComponent<UICheckbox>().isChecked = false;

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_leftbtn_lineup/btn_sortgrade", true).GetComponent<UICheckbox>().Set(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_leftbtn_lineup/btn_sortgrade", true).GetComponent<UICheckbox>().Set(false);

        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_lineup/bundle_leftbtn_lineup/btn_sortstat", true).GetComponent<UICheckbox>().Set(false);
        mRscrcMan.FindChild (dicMenuList ["Ui_team"], "LPanel_cardmix/bundle_leftbtn_lineup/btn_sortstat", true).GetComponent<UICheckbox>().Set(false);

    }

	
	void UniformCancel ()
	{
		ReturnBeforeUniform ();
		if (!Ag.Uniform) {
			switch (mMenuName) {
			case "Btn_Fun_MatchRequire":
				Btn_Fun_MatchRequire ();
				break;
			case "Btn_Fun_GotoLineup":
				Btn_Fun_GotoLineup ();
				break;
			case "Btn_Fun_LineupClose":
				Btn_Fun_LineupClose ();
				break;
			case "Btn_Fun_DirectorModeOpen":
				Btn_Fun_DirectorModeOpen ();
				break;
			case "Btn_Fun_CardMixOpen":
				Btn_Fun_CardMixOpen ();
				break;
			case "Btn_Fun_BuyCard":
				Btn_Fun_BuyCard ();
				break;
			}
			
		}
		
		
		if (mkeeperMode) {
			//ReturnBeforeUniform ();
			Btn_Fun_KeeperMode ();
			
		} else {
			//ReturnBeforeUniform ();
			Btn_Fun_KickerMode ();
		}
		
		KickerUniformSetting ();
		
		MenuCommonOpen ("popup_uniformalert", "Ui_popup", false);
	}
	//---------------------------------------------------------- Kicker Shirt
	//---------------------------------------------------------- ColorChange MainColor
	void MainColorSet (int pNum)
	{
		dicMenuList ["btn_Main_color" + pNum].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["btn_Main_color" + pNum].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			switch (UniformType) {
			case 1: 
				Ag.mySelf.arrUniform [0].Keep.Shirt.ColMain = pNum;
				break;
			case 2: 
				Ag.mySelf.arrUniform [0].Keep.Pants.ColMain = pNum;
				break;
			case 3: 
				Ag.mySelf.arrUniform [0].Keep.Socks.ColMain = pNum;
				break;
			}
		} else {
			switch (UniformType) {
			case 1: 
				Ag.mySelf.arrUniform [0].Kick.Shirt.ColMain = pNum;
				break;
			case 2: 
				Ag.mySelf.arrUniform [0].Kick.Pants.ColMain = pNum;
				break;
			case 3: 
				Ag.mySelf.arrUniform [0].Kick.Socks.ColMain = pNum;
				break;
				
			}
		}
	}
	
	void SubColorSet (int pNum)
	{
		dicMenuList ["Btn_Fun_LineColor" + pNum].GetComponent<UICheckbox> ().isChecked = true;
		dicMenuList ["Btn_Fun_LineColor" + pNum].GetComponent<UICheckbox> ().Set (true);
		if (mkeeperMode) {
			switch (UniformType) {
			case 1: 
				Ag.mySelf.arrUniform [0].Keep.Shirt.ColSub = pNum;
				break;
			case 2: 
				Ag.mySelf.arrUniform [0].Keep.Pants.ColSub = pNum;
				break;
			case 3: 
				Ag.mySelf.arrUniform [0].Keep.Socks.ColSub = pNum;
				break;
			}
		} else {
			switch (UniformType) {
			case 1: 
				Ag.mySelf.arrUniform [0].Kick.Shirt.ColSub = pNum;
				break;
			case 2: 
				Ag.mySelf.arrUniform [0].Kick.Pants.ColSub = pNum;
				break;
			case 3: 
				Ag.mySelf.arrUniform [0].Kick.Socks.ColSub = pNum;
				break;
				
			}
		}
	}
	
	void Btn_Fun_MainColor0 ()
	{
		MainColorSet (0);
        Uniform_SetColor ("outputcolor", new Color (0.11f, 0.11f, 0.11f));
		
	}
	
	void Btn_Fun_MainColor1 ()
	{
		MainColorSet (1);
        Uniform_SetColor ("outputcolor", new Color (0.25f, 0.25f, 0.25f));
		
	}
	
	void Btn_Fun_MainColor2 ()
	{
		MainColorSet (2);
        Uniform_SetColor ("outputcolor", new Color (0.7f, 0.7f, 0.7f));
	}
	
	void Btn_Fun_MainColor3 ()
	{
		MainColorSet (3);
        Uniform_SetColor ("outputcolor", new Color (0.5f, 0.07f, 0.05f));
	}
	
	void Btn_Fun_MainColor4 ()
	{
		MainColorSet (4);
        Uniform_SetColor ("outputcolor", new Color (0.63f, 0.18f, 0.05f));
	}
	
	void Btn_Fun_MainColor5 ()
	{
		MainColorSet (5);
        Uniform_SetColor ("outputcolor", new Color (0.68f, 0.35f, 0));
	}
	
	void Btn_Fun_MainColor6 ()
	{
		MainColorSet (6);
        Uniform_SetColor ("outputcolor", new Color (0.66f, 0.6f, 0));
	}
	
	void Btn_Fun_MainColor7 ()
	{
		MainColorSet (7);
        Uniform_SetColor ("outputcolor", new Color (0.15f, 0.39f, 0.07f));
	}
	
	void Btn_Fun_MainColor8 ()
	{
		MainColorSet (8);
        Uniform_SetColor ("outputcolor", new Color (0.15f, 0.62f, 0.7f));
	}
	
	void Btn_Fun_MainColor9 ()
	{
		MainColorSet (9);
        Uniform_SetColor ("outputcolor", new Color (0.07f, 0.27f, 0.58f));
	}
	
	void Btn_Fun_MainColor10 ()
	{
		MainColorSet (10);
        Uniform_SetColor ("outputcolor", new Color (0.078f, 0.125f, 0.53f));
	}
	
	void Btn_Fun_MainColor11 ()
	{
		MainColorSet (11);
        Uniform_SetColor ("outputcolor", new Color (0.25f, 0.03f, 0.35f));
	}
	//---------------------------------------------------------- ColorChange SubColor
	void Btn_Fun_LineColor0 ()
	{
		
		SubColorSet (0);
        Uniform_SetColor ("outputcolor_1", new Color (0.11f, 0.11f, 0.11f));
	}
	
	void Btn_Fun_LineColor1 ()
	{
		
		SubColorSet (1);
        Uniform_SetColor ("outputcolor_1", new Color (0.25f, 0.25f, 0.25f));
	}
	
	void Btn_Fun_LineColor2 ()
	{
		
		SubColorSet (2);
        Uniform_SetColor ("outputcolor_1", new Color (0.7f, 0.7f, 0.7f));
	}
	
	void Btn_Fun_LineColor3 ()
	{
		
		SubColorSet (3);
        Uniform_SetColor ("outputcolor_1", new Color (0.5f, 0.07f, 0.05f));
	}
	
	void Btn_Fun_LineColor4 ()
	{
		
		SubColorSet (4);
        Uniform_SetColor ("outputcolor_1", new Color (0.63f, 0.18f, 0.05f));
	}
	
	void Btn_Fun_LineColor5 ()
	{
		
		SubColorSet (5);
        Uniform_SetColor ("outputcolor_1", new Color (0.68f, 0.35f, 0));
	}
	
	void Btn_Fun_LineColor6 ()
	{
		
		SubColorSet (6);
        Uniform_SetColor ("outputcolor_1", new Color (0.66f, 0.6f, 0));
	}
	
	void Btn_Fun_LineColor7 ()
	{
		
		SubColorSet (7);
        Uniform_SetColor ("outputcolor_1", new Color (0.15f, 0.39f, 0.07f));
	}
	
	void Btn_Fun_LineColor8 ()
	{
		
		SubColorSet (8);
        Uniform_SetColor ("outputcolor_1", new Color (0.15f, 0.62f, 0.7f));
		
	}
	
	void Btn_Fun_LineColor9 ()
	{
		
		SubColorSet (9);
        Uniform_SetColor ("outputcolor_1", new Color (0.07f, 0.27f, 0.58f));
	}
	
	void Btn_Fun_LineColor10 ()
	{
		
		SubColorSet (10);
		
        Uniform_SetColor ("outputcolor_1", new Color (0.078f, 0.125f, 0.53f));
	}
	
	void Btn_Fun_LineColor11 ()
	{
		
		SubColorSet (11);
        Uniform_SetColor ("outputcolor_1", new Color (0.25f, 0.03f, 0.35f));
	}
	
	void Uniform_SetColor (string pLine, Color pColor)
	{
		
		mProcedureMat.SetProceduralColor (pLine, pColor);
		mProcedureMat.RebuildTextures ();
	}
	
	void MyUniformSetting ()
	{
		MyShirts.mainTexture = mKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [0].mainTexture;
		MyPants.mainTexture = mKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [1].mainTexture;
		MySocks.mainTexture = mKicker.transform.FindChild ("Clothes").renderer.sharedMaterials [2].mainTexture;
	}
	
	void Uniform_SetColor2 (string pLine, Color pColor)
	{
		mProcedureMat.SetProceduralColor (pLine, pColor);
		if (pLine == "outputcolor_1")
			mProcedureMat.RebuildTextures ();
	}
	
	void Uniform_Setting ()
	{
		
		//MyUniformSetting ();
	}
	
	void UNiformSetColorColor2 (string pStr, int pInt)
	{
        switch (pInt) {
        case 0:
            Uniform_SetColor2 (pStr, new Color (0.11f, 0.11f, 0.11f));
            break;
        case 1:
            Uniform_SetColor2 (pStr, new Color (0.25f, 0.25f, 0.25f));
            break;
        case 2:
            Uniform_SetColor2 (pStr, new Color (0.7f, 0.7f, 0.7f));
            break;
        case 3:
            Uniform_SetColor2 (pStr, new Color (0.5f, 0.07f, 0.05f));
            break;
        case 4:
            Uniform_SetColor2 (pStr, new Color (0.63f, 0.18f, 0.05f));
            break;
        case 5:
            Uniform_SetColor2 (pStr, new Color (0.68f, 0.35f, 0));
            break;
        case 6:
            Uniform_SetColor2 (pStr, new Color (0.66f, 0.6f, 0));
            break;
        case 7:
            Uniform_SetColor2 (pStr, new Color (0.15f, 0.39f, 0.07f));
            break;
        case 8:
            Uniform_SetColor2 (pStr, new Color (0.15f, 0.62f, 0.7f));
            break;
        case 9:
            Uniform_SetColor2 (pStr, new Color (0.07f, 0.27f, 0.58f));
            break;
        case 10:
            Uniform_SetColor2 (pStr, new Color (0.078f, 0.125f, 0.53f));
            break;
        case 11:
            Uniform_SetColor2 (pStr, new Color (0.25f, 0.03f, 0.35f));
            break;
        }
	}






	//---------------------------------------------------------- SubmenuClose
	void AllShirtsPantsSocksSubmenuClose ()
	{
		dicMenuList ["scroll_top"].SetActive (false);
		dicMenuList ["scroll_pants"].SetActive (false);
		dicMenuList ["scroll_socks"].SetActive (false);
		dicMenuList ["scroll_topgk"].SetActive (false);
		
	}
}



