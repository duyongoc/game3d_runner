using UnityEngine;

public abstract class S_StateBase: MonoBehaviour
{
    //reference to our state machine
    public SceneManger owner;
    //public  S_SceneState CurrentState { get => currentState; set => currentState = value; }

    //Method called to prepare state
    public virtual void StartState()
    {
    }

    //Method called to update state on every frame
    public virtual void UpdateState()
    {
    }

    //Method called to destroy state
    public virtual void EndState()
    {
    }

}
