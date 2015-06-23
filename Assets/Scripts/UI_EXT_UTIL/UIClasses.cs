using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;


public delegate void DlgtVect3 (Vector3 v3);
public delegate void DlgtVec3Vec3 (Vector3 v3,Vector3 cur);
public delegate float DlgtFloatFloat (float pV);
public delegate Vector3 DlgtVectRvect(Vector3 v3);

/** CuUiOption : 한 화면의 모든 프레임에 적용되는 공통 옵션.
 *      optHoldLimit        :   선택이 될 때까지 지속해야하는 프레임 수.    단위: 프레임  범위 : 3 잠깐 ~ 90 3초.
        optSelectionDist    :   선택시 움직일 수 있는 최대 거리    단위 : 공간상 거리   범위 : 0.01 ~ 10 (셀의 크기에 비례)
        optFlyingSpdMin     :   플라잉 적용 최소 속도    단위 : 공간 거리 / 프레임    범위 : 0.01 ~ 5
        optSpdLimit         :   속도 제한 (놓치는 기능)  단위 : 공간 거리 / 프레임    범위 : 0.01 ~ 5
 * 
 **/
public class CuUiOption
{
    public uint optHoldLimit = 15;
    public float optSelectionDist = 0.5f, optFlyingSpdMin = 0.01f, optSpdLimit = 1f;

    public CuUiOption (uint HoldLimit, float SelDist, float FlyingSpdMin, float SpdLimit)
    {
        optHoldLimit = HoldLimit;
        optSelectionDist = SelDist;
        optFlyingSpdMin = FlyingSpdMin;
        optSpdLimit = SpdLimit;
    }
}

/** CuFrameOption : 프레임마다 다르게 줄 수 있는 옵션. 
 *      optVert         :   수직 스크롤이면 true
        optMoveAniSpd   :   이동 애니매이션 속도 조절  단위 : 없음      범위 : ( 10 빠름 ~ 1000 느림 )
        optSizeAniSpd   :   크기 애니매이션 속도 조절  단위 : 없음      범위 : ( 10 빠름 ~ 1000 느림 )
        optSelectSize   :   선택 되었을 때 크기.    단위 :배수   범위 : (0.5 ~ 2)
        optSwitchSize   :   스위칭 할 때 크기.     단위 :배수   범위 : (0.5 ~ 2)
        optSwitchInFrame    :   프레임 내에서 스위칭 허용할 때 true
 *
 **/
public class CuFrameOption
{
    public int optMoveAniSpd, optSizeAniSpd;
    public float optSelectSize, optSwitchSize;
    public CuUiOption optParent;
    public bool optVert, optSwitchInFrame;

    public float AutoScrollLimitRatio = 0.35f, FlyDeaccelRatio = 0.95f, LimitOfPstnAnimation = 0.05f ;
    // AutoScrollLimitRatio < 0.5, FlyDeaccelRatio ~= 0.9

    public CuFrameOption (CuUiOption UiOpt, bool Vert, int MoveAniSpd, int SizeAniSpd, float SelectSize, float SwitchSize,
                          bool SwitchInFrame = true)
    {
        optParent = UiOpt;
        optVert = Vert;
        optMoveAniSpd = MoveAniSpd;
        optSizeAniSpd = SizeAniSpd;

        optSelectSize = SelectSize;
        optSwitchSize = SwitchSize;

        optSwitchInFrame = SwitchInFrame;

        Ag.LogIntenseWord (" CuFrameOption :: Creation " + optParent.optHoldLimit.LogWith ("Parent"));
    }
}

public enum UiState
{
    NONE,
    SCROLL,
    SCROLL_OFFLIMIT,
    FLY,
    FLY_OFFLIMIT,
    FlyBackToLimit,
    SELECTED,
    SWITCH,
    AUTO_SCR,
    SPLIT,
    SPL_CLOSE,
    ALIEN_WENT,
    ALIEN_CAME,
    NEW_ELEMENT,
    SPEED_LIMIT,
}

public static class ExtUiState  // Extension Methods ..
{
    public static void Show (this UiState pObj, string cmnt = " >> ")
    {
        Ag.LogIntense (1, true);
        Ag.LogString (" UIClasses ::  ...... " + cmnt.LogWith ("=========================================== ") 
                      + pObj.LogWith ("State") );
        Ag.LogIntense (1, false);
    }
}

public class AmUiOption
{                 // Member of UI Cell Manager

    public string mMethodBtwWagu = "SWAP";// "SWAP" "INSERT"
    public int vectMoveAni = 100, vectSizeAni = 100;
    public bool? scrlVert;
    // null : Block Scroll
    public int numSelection = 30, numBeAlien = 2;
    public float accumDistLimit4Selection = 1.0f, muiDiagonalDist;

    // Basic Option ..
    public float minFlybackDistance = 0.02f;

    // Switch to Scroll State ... Limit Setting..
    public float scrlLimtWrl = 0.001f, scrlAccuDst = 0.03f;
    // Ratio to Diagonal Distance ..
    public float DiagonalDist { get; private set; }

    public AmUiOption (float pDiagDist, bool? pScrollVert)
    {
        DiagonalDist = pDiagDist;
        scrlVert = pScrollVert;
    }

    public bool OutofScrlLimit (float pDistFromStaPoint)
    {
        return pDistFromStaPoint > DiagonalDist * scrlLimtWrl;
    }

    public bool OutofAccumLimit (float pAccumDist)
    {
        return pAccumDist > DiagonalDist * scrlAccuDst;
    }
}