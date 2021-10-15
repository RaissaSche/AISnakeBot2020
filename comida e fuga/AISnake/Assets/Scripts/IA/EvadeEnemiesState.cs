using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeEnemiesState : State
{
    public EvadeEnemiesState(SnakeSystem system) : base(system)
    {
        _system = system;
    }
    
    public override IEnumerator EvadeEnemies()
    {
        //comportamento de desviar dos inimigos com raytracing vai aqui
        Debug.Log("Evade Enemies state");
        yield return null;
    }
    
}