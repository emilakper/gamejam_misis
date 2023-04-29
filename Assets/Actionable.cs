using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface Actionable
{
    public void actOn(movement player);
    public void preActOn(movement player);
    public void postActOn(movement player);
}

