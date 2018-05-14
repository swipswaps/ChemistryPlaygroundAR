using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class Blend2DAttribute : PropertyAttribute
{
    public readonly string xLabel;
    public readonly string yLabel;
    public readonly float multiplier;
    public readonly int hash;
    public Blend2DAttribute(string xLabel = "X", string yLabel = "Y")
    {
        hash = "Blend2D".GetHashCode();
        this.xLabel = xLabel;
        this.yLabel = yLabel;
        this.multiplier = multiplier;
    }
}