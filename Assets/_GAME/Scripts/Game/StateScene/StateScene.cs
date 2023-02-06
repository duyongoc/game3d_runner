using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScene : MonoBehaviour
{

    // method called to prepare state
    public virtual void StartState() { }

    // method called to update state on every frame
    public virtual void UpdateState() { }

    // method called to destroy state
    public virtual void EndState() { }

}
