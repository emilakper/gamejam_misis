using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface Actionable
{
    public void preActOn(movement player);
    public void actOn(movement player, KeyCode action_key);
    public void postActOn(movement player);
}

