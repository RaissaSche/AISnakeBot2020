using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{
    public StartState(SnakeSystem system) : base(system)
    {
        _system = system;
    }

    public override IEnumerator Start()
    {
        //comportamento inicial/setup vai aqui
        Debug.Log("Start state");
        yield return null;
    }
}