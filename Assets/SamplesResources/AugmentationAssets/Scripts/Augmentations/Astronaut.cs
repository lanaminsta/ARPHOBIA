/*========================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

public class Astronaut : Augmentation
{
    #region PUBLIC_MEMBER_VARIABLES
    public DrillController m_Drill;
    public ParticleSystem m_DrillEffectSmoke;
    public ParticleSystem m_DrillEffectRocks;
    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PUBLIC_METHODS
    public override void OnEnter()
    {
        base.OnEnter();
        m_EvtOnEnter.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        m_Drill.m_IsDrilling = false;
        IsWaving = false;
    }

    public void StartDrilling()
    {
        IsDrilling = true;
    }

    public void AnimEvt_ScaleUpDrill()
    {
        Debug.Log("AnimEvt_ScaleUpDrill() called.");

        m_Drill.m_IsDrilling = true;
    }

    public void AnimEvt_ScaleDownDrill()
    {
        Debug.Log("AnimEvt_ScaleDownDrill() called.");

        m_Drill.m_IsDrilling = false;
    }


    public void AnimEvt_StopWaving()
    {
        IsWaving = false;
    }

    public void AnimEvt_StartWaving()
    {
        IsWaving = true;
    }

    public void HandleVirtualButtonPressed()
    {
        AnimEvt_StartWaving();
    }

    public void HandleVirtualButtonReleased()
    {

    }
    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private bool IsDrilling
    {
        get { return animator.GetBool("IsDrilling"); }
        set { animator.SetBool("IsDrilling", value); }
    }

    private bool IsWaving
    {
        get { return animator.GetBool("IsWaving"); }
        set { animator.SetBool("IsWaving", value); }
    }

    #endregion // PRIVATE_METHODS
}

