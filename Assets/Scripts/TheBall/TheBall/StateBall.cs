using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBall : MonoBehaviour
{
    protected TheBall owner;
    public TheBall Owner { get => owner; set => owner = value; }

    //Method called to prepare state
    public virtual void StartState()
    {

    }

    //Method called to udpate state on every frame
    public virtual void UpdateState()
    {

    }

    //Method called to destroy state
    public virtual void EndState()
    {

    }
}
