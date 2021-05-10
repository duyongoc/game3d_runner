using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTutorial : StateCharacter
{


    // isTouch will check 
    public enum Touch { Move, Left, Right, None };
    private Touch touchScene = Touch.Move;

    //
    //= private
    private MainCharacter character;
    private float timerProcess = 0f;
    private float timer = 1.5f;


    #region UNTIY
    private void Start()
    {

    }

    // private void Update()
    // {
    // }
    #endregion


    #region STATE
    public override void StartState()
    {
        base.StartState();
        Invoke("SetUpTouchLeftFirst", timer);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        switch (touchScene)
        {
            case Touch.Move:
                {
                    character.GetAnimator.SetBool("Moving", true);
                    character.transform.Translate(Vector3.forward * character.GetMoveSpeed * Time.deltaTime);

                    break;
                }
            case Touch.Left:
                {
                    character.GetAnimator.SetBool("Moving", false);
                    float moveTurn = Input.mousePosition.x;
                    if (Input.GetMouseButton(0) && (moveTurn < Screen.width / 2 && moveTurn > 0))
                    {
                        character.GetAnimator.SetBool("Moving", true);
                        character.transform.Translate(Vector3.forward * character.GetMoveSpeed * Time.deltaTime);
                        character.transform.Rotate(-Vector3.up, character.GetAngleSpeed * Time.deltaTime);

                        timerProcess += Time.deltaTime;
                        if (timerProcess > timer)
                        {
                            character.GetAnimator.SetBool("Moving", false);
                            // SetActiveObjectTutorial(false, true, false);
                            touchScene = Touch.Right;

                            timerProcess = 0;
                        }
                    }
                    break;
                }
            case Touch.Right:
                {
                    character.GetAnimator.SetBool("Moving", false);
                    float moveTurn = Input.mousePosition.x;
                    if (Input.GetMouseButton(0) && (moveTurn > Screen.width / 2 && moveTurn < Screen.width))
                    {
                        character.GetAnimator.SetBool("Moving", true);
                        character.transform.Translate(Vector3.forward * character.GetMoveSpeed * Time.deltaTime);
                        character.transform.Rotate(Vector3.up, character.GetAngleSpeed * Time.deltaTime);

                        timerProcess += Time.deltaTime;
                        if (timerProcess > timer)
                        {
                            character.GetAnimator.SetBool("Moving", false);

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
        }
    }
    #endregion


    public override void EndState()
    {
        base.EndState();
    }

    private void SetUpTouchLeftFirst()
    {
        // SetActiveObjectTutorial(true, false, false);
        touchScene = Touch.Left;
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }

}
