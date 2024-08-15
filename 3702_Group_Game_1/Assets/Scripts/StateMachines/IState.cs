using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public interface IState
{
    public void Tick();
    public void SetTransitions(params IState[] transitionsTo);
}
