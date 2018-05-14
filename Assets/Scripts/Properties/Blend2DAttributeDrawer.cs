#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEngine.Rendering;
[CustomPropertyDrawer(typeof(Blend2DAttribute))]
public class Blend2DAttributeDrawer : PropertyDrawer
{
    private const float blendBoxSize = 150f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Blend2DAttribute nP = attribute as Blend2DAttribute;
        Do2DBlend(position, property, nP);
    }

    private int hotControl = -1;

    private void Do2DBlend(Rect r, SerializedProperty sP, Blend2DAttribute nP)
    {
        using (new GUI.GroupScope(r))
        {
            Rect blendRect = new Rect(Vector2.zero, Vector2.one * blendBoxSize);

            Event e = Event.current;
            if (e.isMouse && e.button == 0 && (e.rawType == EventType.MouseDown || e.rawType == EventType.MouseDrag))
            {
                if (blendRect.Contains(e.mousePosition))
                {
                    sP.vector2Value = new Vector2(Mathf.Clamp(e.mousePosition.x, 0, blendBoxSize),
                                          Mathf.Clamp(blendBoxSize - e.mousePosition.y, 0, blendBoxSize)) / blendBoxSize;
                    e.Use();
                }
            }
            Vector2 v = sP.vector2Value;
            using (EditorGUI.ChangeCheckScope cC = new EditorGUI.ChangeCheckScope())
            {
                Rect labelRect = new Rect(blendBoxSize + 5, blendBoxSize / 2f - 50, Screen.width - blendBoxSize - 5, 15);
                EditorGUI.LabelField(labelRect, sP.displayName, EditorStyles.boldLabel);
                labelRect.y += 20;
                EditorGUI.LabelField(labelRect, nP.xLabel);
                labelRect.y += 15;
                v.x = EditorGUI.FloatField(labelRect, v.x);
                labelRect.y += 15;
                EditorGUI.LabelField(labelRect, nP.yLabel);
                labelRect.y += 15;
                v.y = EditorGUI.FloatField(labelRect, v.y);
                if (cC.changed)
                    sP.vector2Value = new Vector2(Mathf.Clamp01(v.x), Mathf.Clamp01(v.y));
            }

            using (new GUI.GroupScope(blendRect))
            {
                GL.Begin(Application.platform == RuntimePlatform.WindowsEditor ? GL.QUADS : GL.LINES);
                ApplyWireMaterial.Invoke(null, new object[] { CompareFunction.Always });
                const float quarter = 0.25f * blendBoxSize;
                const float half = quarter * 2;
                const float threeQuarters = half + quarter;
                float lowGreyVal = EditorGUIUtility.isProSkin ? 0.25f : 0.75f;
                Color lowGrey = new Color(lowGreyVal, lowGreyVal, lowGreyVal);
                DrawLineFast(new Vector2(quarter, 0), new Vector2(quarter, blendBoxSize), lowGrey);
                DrawLineFast(new Vector2(quarter, 0), new Vector2(quarter, blendBoxSize), lowGrey);
                DrawLineFast(new Vector2(threeQuarters, 0), new Vector2(threeQuarters, blendBoxSize), lowGrey);
                DrawLineFast(new Vector2(0, quarter), new Vector2(blendBoxSize, quarter), lowGrey);
                DrawLineFast(new Vector2(0, threeQuarters), new Vector2(blendBoxSize, threeQuarters), lowGrey);
                DrawLineFast(new Vector2(half, 0), new Vector2(half, blendBoxSize), Color.grey);
                DrawLineFast(new Vector2(0, half), new Vector2(blendBoxSize, half), Color.grey);
                GL.End();

                GL.Begin(Application.platform == RuntimePlatform.WindowsEditor ? GL.QUADS : GL.LINES);
                ApplyWireMaterial.Invoke(null, new object[] { CompareFunction.Always });
                Vector2 circlePos = sP.vector2Value;
                circlePos.y = 1 - circlePos.y;
                circlePos *= blendBoxSize;
                DrawCircleFast(circlePos, blendBoxSize * 0.04f, 2, new Color(1, 0.5f, 0));
                GL.End();
            }

            
        }
    }

    private static void DrawLineFast(Vector2 from, Vector2 to, Color color)
    {
        GL.Color(color);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector2 tangent = (to - from).normalized;
            Vector2 mult = new Vector2(tangent.y > tangent.x ? -1 : 1, tangent.y > tangent.x ? 1 : -1);
            tangent = (new Vector2(mult.x * tangent.y, mult.y * tangent.x)) * 0.5f;
            GL.Vertex(new Vector3(from.x + tangent.x, from.y + tangent.y, 0f));
            GL.Vertex(new Vector3(from.x - tangent.x, from.y - tangent.y, 0f));
            GL.Vertex(new Vector3(to.x + tangent.x, to.y + tangent.y, 0f));
            GL.Vertex(new Vector3(to.x - tangent.x, to.y - tangent.y, 0f));
        }
        else
        {
            GL.Vertex(new Vector3(from.x, from.y, 0f));
            GL.Vertex(new Vector3(to.x, to.y, 0f));
        }
    }

    private const int circleDivisions = 18;
    private static void DrawCircleFast(Vector2 position, float radius, float thickness, Color color)
    {
        GL.Color(color);
        for (int i = 1; i <= circleDivisions; i++)
        {
            float vC = Mathf.PI * 2 * (i / (float)circleDivisions - 1 / (float)circleDivisions);
            float vP = Mathf.PI * 2 * (i / (float)circleDivisions);
            Vector2 from = position + new Vector2(Mathf.Sin(vP) * radius, Mathf.Cos(vP) * radius);
            Vector2 to = position + new Vector2(Mathf.Sin(vC) * radius, Mathf.Cos(vC) * radius);
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Vector2 tangent = (to - from).normalized;
                Vector2 mult = new Vector2(tangent.y > tangent.x ? -1 : 1, tangent.y > tangent.x ? 1 : -1);
                tangent = (new Vector2(mult.x * tangent.y, mult.y * tangent.x)) * 0.5f * thickness;
                GL.Vertex(new Vector3(from.x + tangent.x, from.y + tangent.y, 0f));

                GL.Vertex(new Vector3(to.x + tangent.x, to.y + tangent.y, 0f));
                GL.Vertex(new Vector3(to.x - tangent.x, to.y - tangent.y, 0f));
                GL.Vertex(new Vector3(from.x - tangent.x, from.y - tangent.y, 0f));
            }
            else
            {
                GL.Vertex(new Vector3(from.x, from.y, 0f));
                GL.Vertex(new Vector3(to.x, to.y, 0f));
            }
        }
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return blendBoxSize;
    }

    private MethodInfo _ApplyWireMaterial;
    private MethodInfo ApplyWireMaterial
    {
        get
        {
            return _ApplyWireMaterial ?? (_ApplyWireMaterial =
                       typeof(HandleUtility).GetMethod("ApplyWireMaterial", BindingFlags.NonPublic | BindingFlags.Static, null, new[] { typeof(CompareFunction) }, null));
        }
    }
}
#endif