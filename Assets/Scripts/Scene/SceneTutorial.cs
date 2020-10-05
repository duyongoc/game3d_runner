using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTutorial : StateScene
{
    // [Header("Position original")]
    // [SerializeField] private int m_originX = default;
    // [SerializeField] private int m_originY = default;

    // [Tooltip("Speed time duration when change scene")]
    // [SerializeField] private float m_speedDuration = 0.5f;
    [Header("Slider process")]
    public GameObject textScore;
    // public GameObject sliderProcess;
    
    public GameObject[] handLeftTutorial;
    public GameObject[] handRightTutorial;
    public GameObject buttonOK;

    [Header("The Ball")]
    [SerializeField] private MainCharacter mainCharacter = default;

    private float timer = 1.5f;
    private float timerProcess = 0f;

    // isTouch will check 
    public enum Touch {Move, Left, Right, None};
    private Touch touchScene = Touch.Move;

    public override void StartState()
    {
        base.EndState();

        Owner.SetActivePanelScene(this.name);

        textScore.SetActive(false);
        // sliderProcess.SetActive(false);
        
        // set false all gameobject when load tutorial scene
        SetActiveObjectTutorial(false, false, false);
        Invoke("SetUpTouchLeftFirst", timer);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        switch(touchScene)
        { 
            case Touch.Move:
            {
                mainCharacter.animator.SetBool("Moving", true);
                mainCharacter.transform.Translate(Vector3.forward * mainCharacter.moveSpeed * Time.deltaTime); 

                break;
            }
            case Touch.Left:
            {
                mainCharacter.animator.SetBool("Moving", false);
                float moveTurn = Input.mousePosition.x;
                if(Input.GetMouseButton(0) && (moveTurn < Screen.width / 2 && moveTurn > 0))
                {
                    mainCharacter.animator.SetBool("Moving", true);
                    mainCharacter.transform.Translate(Vector3.forward * mainCharacter.moveSpeed * Time.deltaTime); 
                    mainCharacter.transform.Rotate(-Vector3.up, mainCharacter.angleSpeed * Time.deltaTime);

                    timerProcess += Time.deltaTime;
                    if(timerProcess > timer)
                    {
                        mainCharacter.animator.SetBool("Moving", false);
                        SetActiveObjectTutorial(false, true, false);
                        touchScene = Touch.Right;

                        timerProcess = 0;
                    }
                }
                break;
            }
            case Touch.Right: 
            {
                mainCharacter.animator.SetBool("Moving", false);
                float moveTurn = Input.mousePosition.x;
                if(Input.GetMouseButton(0) && (moveTurn > Screen.width / 2 && moveTurn < Screen.width))
                {
                    mainCharacter.animator.SetBool("Moving", true);
                    mainCharacter.transform.Translate(Vector3.forward * mainCharacter.moveSpeed * Time.deltaTime); 
                    mainCharacter.transform.Rotate(Vector3.up, mainCharacter.angleSpeed * Time.deltaTime);

                    timerProcess += Time.deltaTime;
                    if(timerProcess > timer)
                    {
                        mainCharacter.animator.SetBool("Moving", false);
                        // set active true button OK to load ingame scene
                        SetActiveObjectTutorial(false, false, true);
                        touchScene = Touch.None;

                        timerProcess = 0;
                    }
                }
                break;
            }
            case Touch.None:
            {
                break;
            }
            
            // default:
            //     mainCharacter.animator.SetBool("Moving", false);
        }

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
        mainCharacter.Reset();
        Owner.ChangeState(Owner.m_sceneInGame);

        buttonOK.SetActive(false);
        
        //
        //owner.m_sceneShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(m_originX, m_originY), m_speedDuration);
        //owner.m_sceneMenu.GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, m_speedDuration);
    }
    #endregion

    private void SetUpTouchLeftFirst()
    {
        SetActiveObjectTutorial(true, false, false);
        touchScene = Touch.Left;
    }

    private void SetActiveObjectTutorial( bool handLeft, bool handRight, bool btnOK)
    {
        handLeftTutorial[0].SetActive(handLeft);
        handLeftTutorial[1].SetActive(handLeft);
        handRightTutorial[0].SetActive(handRight);
        handRightTutorial[1].SetActive(handRight);
        buttonOK.SetActive(btnOK);
    }

    
}
