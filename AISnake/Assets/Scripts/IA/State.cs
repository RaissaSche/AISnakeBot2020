using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected /*readonly*/ SnakeSystem _system;

    public State(SnakeSystem system)
    {
        _system = system;
    }

    public virtual IEnumerator Start()
    {
        _system.SetState(new StartState(_system));
        yield return null;
    }

    public virtual IEnumerator ChaseMoney()
    {
        _system.SetState(new ChaseMoneyState(_system));
        yield return null;
    }

    public virtual IEnumerator EvadeEnemies()
    {
        _system.SetState(new EvadeEnemiesState(_system));
        yield return null;
    }
}