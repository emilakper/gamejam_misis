using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Pickable : MonoBehaviour 
{
    public string PrefabToSpawnName;
    abstract public void onPick(movement obj);
}
