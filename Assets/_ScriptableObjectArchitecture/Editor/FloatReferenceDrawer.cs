using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatReferenceDrawer : PropertyDrawer
{
    private readonly string[] popupOptions = { "Use Constant", "Use Variable" };
    private GUIStyle popupStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        this.popupStyle ??= new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
        {
            imagePosition = ImagePosition.ImageOnly
        };

        SerializedProperty useConstant = property.FindPropertyRelative("useConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("value");
        SerializedProperty variable = property.FindPropertyRelative("variable");

        Rect buttonRect = new Rect(position);
        buttonRect.yMin += this.popupStyle.margin.top;
        buttonRect.width = this.popupStyle.fixedWidth + this.popupStyle.margin.right;
        buttonRect.height = EditorGUIUtility.singleLineHeight;
        
        position.xMin = buttonRect.xMax;
        
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, this.popupOptions, this.popupStyle);
        useConstant.boolValue = result == 0;
        EditorGUI.indentLevel = indent;

        EditorGUI.PropertyField(
            position,
            useConstant.boolValue ? constantValue : variable,
            label,
            true
        );
    }
}