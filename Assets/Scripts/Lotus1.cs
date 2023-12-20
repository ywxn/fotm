using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lotus1 : Enemy
{

    public override float Health { get => base.Health; set => base.Health = 5f; }

    public override float XP
    {
        get { return base.XP * 2f; }
        set { base.XP = value; }
    }

}
