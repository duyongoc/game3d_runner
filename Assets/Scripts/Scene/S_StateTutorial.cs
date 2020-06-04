using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_StateTutorial : S_StateBase 
{


    public override void StartState()
    {
        base.EndState();
        owner.SetActivePanelScene(this.name);
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void EndState()
    {
        base.EndState();

    }


    

}
