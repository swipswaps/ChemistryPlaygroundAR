using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Atom", menuName = "Chemistry/Atom")]
public class AtomScriptableObject : ScriptableObject
{
    public string code;
    public string atomName;
    public int upperLevelCount;
    public int protons;
    public int neutrons;
}
