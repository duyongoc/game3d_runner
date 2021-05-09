using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScene : MonoBehaviour
{

    //Method called to prepare state
    public virtual void StartState() { }

    //Method called to update state on every frame
    public virtual void UpdateState() { }

    //Method called to destroy state
    public virtual void EndState() { }

}
