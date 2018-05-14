using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Molecula", menuName = "Chemistry/Molecula")]
public class MoleculaScriptableObject : ScriptableObject
{
    public List<SerializableEdge> edges;
    public MoleculaInfo info;

    [Serializable]
    public class SerializableEdge
    {
        public BasicAtom first;
        public BasicAtom second;
        public EdgePayload payload;
    }
}
[Serializable]
public class EdgePayload
{
    public int electronsFirst;
    public int electronsSecond;
}
[Serializable]
public class MoleculaInfo
{
    public string title;
    [Multiline]
    public string info;
    public Sprite sprite;

    public override bool Equals(object obj)
    {
        var other = obj as MoleculaInfo;
        return other != null && other.title == this.title && other.info == this.info;
    }

    public override int GetHashCode()
    {
        return info.GetHashCode() + 31 * title.GetHashCode(); 
    }
}