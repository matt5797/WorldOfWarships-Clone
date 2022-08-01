using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShellData", menuName = "Scriptable Object/Shell Data", order = 0)]
public class ShellData : ScriptableObject
{
    public int W = 55; // SHELL WEIGHT
    public float D = 0.152f; // SHELL DIAMETER
    public float c_D = 0.321f;  // SHELL DRAG
    public int V_0 = 950; // SHELL MUZZLE VELOCITY
    public int K = 2216; // SHELL KRUPP
}