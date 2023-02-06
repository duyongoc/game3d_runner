using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCharacter : MonoBehaviour
{

    // method called to prepare state
    public virtual void StartState() {}

    // method called to udpate state on every frame
    public virtual void UpdateState() {}

    // method called to destroy state
    public virtual void EndState() {}
    
}
