using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMoneyState : State
{
    public ChaseMoneyState(SnakeSystem system) : base(system)
    {
        _system = system;
    }

    public override IEnumerator ChaseMoney()
    {
        //comportamento de procurar dinheiro com raytracing vai aqui
        Debug.Log("Chase Money state");
        yield return null;
    }
}