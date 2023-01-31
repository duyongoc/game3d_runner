using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTutorial : StateScene
{

    [Header("[Setting]")]
    public GameObject textScore;
    public GameObject buttonOK;
    public GameObject[] handLeftTutorial;
    public GameObject[] handRightTutorial;
    // public GameObject sliderProcess;



    public override void StartState()
    {
        base.EndState();

        // Owner.SetActivePanelScene(this.name);
        // sliderProcess.SetActive(false);
        textScore.SetActive(false);

        // set false all gameobject when load tutorial scene
        SetActiveObjectTutorial(false, false, false);

    }

    public override void UpdateState()
    {
        base.UpdateState();
        // mainCharacter.animator.SetBool("Moving", false);
    }

    public override void EndState()
    {
        base.EndState();

        textScore.SetActive(true);
        // sliderProcess.SetActive(true);
    }

    #region Events of button
    public void OnPressButtonOK()
    {
        // mainCharacter.Reset();
        // Owner.ChangeState(Owner.m_sceneInGame);
        buttonOK.SetActive(false);
    }
    #endregion


    private void SetUpTouchLeftFirst()
    {
        SetActiveObjectTutorial(true, false, false);
    }


    private void SetActiveObjectTutorial(bool handLeft, bool handRight, bool btnOK)
    {
        handLeftTutorial[0].SetActive(handLeft);
        handLeftTutorial[1].SetActive(handLeft);
        handRightTutorial[0].SetActive(handRight);
        handRightTutorial[1].SetActive(handRight);
        buttonOK.SetActive(btnOK);
    }


}
