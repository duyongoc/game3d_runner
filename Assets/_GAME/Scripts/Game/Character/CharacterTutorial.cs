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
    private MainCharacter mainCharacter = default;
    private float timerProcess = 0f;
    private float timer = 1.5f;


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
                    mainCharacter.animator.SetBool("Moving", true);
                    mainCharacter.transform.Translate(Vector3.forward * mainCharacter.moveSpeed * Time.deltaTime);

                    break;
                }
            case Touch.Left:
                {
                    mainCharacter.animator.SetBool("Moving", false);
                    float moveTurn = Input.mousePosition.x;
                    if (Input.GetMouseButton(0) && (moveTurn < Screen.width / 2 && moveTurn > 0))
                    {
                        mainCharacter.animator.SetBool("Moving", true);
                        mainCharacter.transform.Translate(Vector3.forward * mainCharacter.moveSpeed * Time.deltaTime);
                        mainCharacter.transform.Rotate(-Vector3.up, mainCharacter.angleSpeed * Time.deltaTime);

                        timerProcess += Time.deltaTime;
                        if (timerProcess > timer)
                        {
                            mainCharacter.animator.SetBool("Moving", false);
                            // SetActiveObjectTutorial(false, true, false);
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
                    if (Input.GetMouseButton(0) && (moveTurn > Screen.width / 2 && moveTurn < Screen.width))
                    {
                        mainCharacter.animator.SetBool("Moving", true);
                        mainCharacter.transform.Translate(Vector3.forward * mainCharacter.moveSpeed * Time.deltaTime);
                        mainCharacter.transform.Rotate(Vector3.up, mainCharacter.angleSpeed * Time.deltaTime);

                        timerProcess += Time.deltaTime;
                        if (timerProcess > timer)
                        {
                            mainCharacter.animator.SetBool("Moving", false);

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

    public override void EndState()
    {
        base.EndState();
    }

    private void SetUpTouchLeftFirst()
    {
        // SetActiveObjectTutorial(true, false, false);
        touchScene = Touch.Left;
    }

}
