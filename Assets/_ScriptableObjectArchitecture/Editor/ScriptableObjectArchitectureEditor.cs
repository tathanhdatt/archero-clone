using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TabbedVariableEditorWindow : EditorWindow
{
    private const string WindowTitle = "ScriptableObject Variable Manager";

    private enum TabType
    {
        FloatVariable = 0,
        IntVariable = 1,
    }

    private TabType currentTab = TabType.FloatVariable;
    private Vector2 scrollPosition;
    private string searchFilter = string.Empty;
    private bool showModifiedOnly;
    private bool groupByFolder;

    private readonly List<ScriptableObjectVariable>
        variables = new List<ScriptableObjectVariable>();

    private readonly Dictionary<string, List<ScriptableObjectVariable>> folderGroups
        = new Dictionary<string, List<ScriptableObjectVariable>>();

    private readonly Dictionary<string, bool> folderExpandStates
        = new Dictionary<string, bool>();

    [MenuItem("Tools/ScriptableObject Variable Manager")]
    public static void ShowWindow()
    {
        GetWindow<TabbedVariableEditorWindow>(WindowTitle);
    }

    private void OnEnable()
    {
        RefreshVariablesList();
    }

    private void RefreshVariablesList()
    {
        switch (this.currentTab)
        {
            case TabType.FloatVariable:
                RefreshVariables();
                break;
            case TabType.IntVariable:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void RefreshVariables()
    {
        this.variables.Clear();
        this.folderGroups.Clear();

        string[] guids = AssetDatabase.FindAssets("t:ScriptableObjectVariable");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObjectVariable variable =
                AssetDatabase.LoadAssetAtPath<ScriptableObjectVariable>(path);
            if (variable == null) continue;
            this.variables.Add(variable);
            string pathString = Path.GetDirectoryName(path);
            if (pathString == null) continue;
            string folder = pathString.Replace("\\", "/");
            this.folderGroups.TryAdd(folder, new List<ScriptableObjectVariable>(30));
            this.folderExpandStates.TryAdd(folder, true);
            this.folderGroups[folder].Add(variable);
        }
    }

    private void OnGUI()
    {
        DrawTabBar();
        // Toolbar
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        if (GUILayout.Button("Refresh",
                EditorStyles.toolbarButton,
                GUILayout.Width(60)))
        {
            RefreshVariablesList();
        }

        this.searchFilter = EditorGUILayout.TextField(
            this.searchFilter,
            EditorStyles.toolbarSearchField);

        this.showModifiedOnly = GUILayout.Toggle(
            this.showModifiedOnly,
            "Show Modified Only",
            EditorStyles.toolbarButton);

        bool prevGroupByFolder = this.groupByFolder;
        this.groupByFolder = GUILayout.Toggle(
            this.groupByFolder,
            "Group by Folder",
            EditorStyles.toolbarButton);

        // Nếu bật "Group by Folder", đặt tất cả các folder ở trạng thái mở rộng mặc định
        if (this.groupByFolder && !prevGroupByFolder)
        {
            foreach (string folder in this.folderGroups.Keys)
            {
                this.folderExpandStates[folder] = true;
            }
        }

        EditorGUILayout.EndHorizontal();

        DrawVariablesTab();
    }

    private void DrawTabBar()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        GUI.changed = false;
        bool floatToggle = GUILayout.Toggle(
            this.currentTab == TabType.FloatVariable,
            "Float Variable",
            EditorStyles.toolbarButton);
        if (floatToggle)
        {
            this.currentTab = TabType.FloatVariable;
        }

        bool intToggle = GUILayout.Toggle(
            this.currentTab == TabType.IntVariable,
            "Int Variable",
            EditorStyles.toolbarButton);
        if (intToggle)
        {
            this.currentTab = TabType.IntVariable;
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        if (GUI.changed)
        {
            RefreshVariablesList();
        }
    }

    private void DrawVariablesTab()
    {
        // Status bar với số lượng biến
        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
        EditorGUILayout.LabelField($"Found {this.variables.Count} Variable instances",
            EditorStyles.boldLabel);

        // Buttons to expand/collapse all folders
        if (this.groupByFolder && this.folderGroups.Count > 0)
        {
            if (GUILayout.Button("Expand All", GUILayout.Width(80)))
            {
                foreach (string folder in this.folderGroups.Keys)
                {
                    this.folderExpandStates[folder] = true;
                }
            }

            if (GUILayout.Button("Collapse All", GUILayout.Width(80)))
            {
                foreach (string folder in this.folderGroups.Keys)
                {
                    this.folderExpandStates[folder] = false;
                }
            }
        }

        EditorGUILayout.EndHorizontal();

        this.scrollPosition = EditorGUILayout.BeginScrollView(this.scrollPosition);

        if (this.groupByFolder)
        {
            DrawGroupedVariables();
        }
        else
        {
            DrawFlatVariablesList();
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawFlatVariablesList()
    {
        List<ScriptableObjectVariable> filteredVariables =
            this.variables.Where(FilterVariable).ToList();
        DrawVariablesFromList(filteredVariables);
    }

    private void DrawGroupedVariables()
    {
        foreach (KeyValuePair<string, List<ScriptableObjectVariable>> folderGroup in this
                     .folderGroups)
        {
            List<ScriptableObjectVariable> filteredVariables =
                folderGroup.Value.Where(FilterVariable).ToList();
            bool hasVisibleVariables = filteredVariables.Count > 0;
            if (!hasVisibleVariables)
                continue;
            // Vẽ folder foldout
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            // Lấy trạng thái mở rộng hiện tại của folder này
            bool folderExpanded = true;
            if (this.folderExpandStates.TryGetValue(folderGroup.Key, out bool expandable))
            {
                folderExpanded = expandable;
            }

            // Vẽ foldout header với style tùy chỉnh
            EditorGUILayout.BeginHorizontal();
            // Tạo foldout với icon folder và số lượng biến
            bool newExpandState = EditorGUILayout.Foldout(
                folderExpanded,
                $"{folderGroup.Key} ({folderGroup.Value.Count})",
                true,
                EditorStyles.foldoutHeader);
            // Cập nhật trạng thái mở rộng nếu đã thay đổi
            if (newExpandState != folderExpanded)
            {
                this.folderExpandStates[folderGroup.Key] = newExpandState;
            }

            EditorGUILayout.EndHorizontal();

            // Chỉ hiển thị nội dung nếu folder được mở rộng
            if (this.folderExpandStates[folderGroup.Key])
            {
                // Thêm khoảng cách và đường ngang
                EditorGUILayout.Space(2);
                // Vẽ từng biến trong folder
                DrawVariablesFromList(filteredVariables);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }
    }

    private bool FilterVariable(ScriptableObjectVariable variable)
    {
        bool hasFilter = !string.IsNullOrEmpty(this.searchFilter);
        if (!hasFilter) return FilterVariablesByTabType(variable);
        bool isMatchFilter = variable.name.ToLower().Contains(this.searchFilter.ToLower());
        if (!isMatchFilter) return false;
        // if (!this.showModifiedOnly) return true;
        // bool isModified = !Mathf.Approximately(variable.Value, GetInitValue(variable));
        // if (isModified) return true;
        return FilterVariablesByTabType(variable);
    }

    private void DrawVariablesFromList(IEnumerable<ScriptableObjectVariable> variables)
    {
        foreach (ScriptableObjectVariable variable in variables)
        {
            DrawVariableEditor(variable);
        }
    }

    private bool FilterVariablesByTabType(ScriptableObjectVariable variable)
    {
        switch (this.currentTab)
        {
            case TabType.FloatVariable:
                return variable is FloatVariable;
            case TabType.IntVariable:
                return variable is IntVariable;
            default:
                return false;
        }
    }

    private void DrawVariableEditor(ScriptableObjectVariable variable)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        ShowHeaderOfVariable(variable);
        EditorGUILayout.Space();
        SerializedObject serializedObject = ShowFields(variable);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Reset to Initial Value", GUILayout.Width(150)))
        {
            Undo.RecordObject(variable, "Reset FloatVariable to Initial Value");
            if (serializedObject != null)
            {
                serializedObject.Update();
                // valueProperty.floatValue = initValueProperty.floatValue;
                serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        if (EditorGUI.EndChangeCheck())
        {
            // Đảm bảo các giá trị được lưu
            EditorUtility.SetDirty(variable);
        }
    }

    private void ShowHeaderOfVariable(ScriptableObjectVariable variable)
    {
        EditorGUILayout.BeginHorizontal();
        ShowVariable(variable);
        PingButton(variable);
        SelectButton(variable);
        EditorGUILayout.EndHorizontal();
    }

    private void ShowVariable(ScriptableObjectVariable variable)
    {
        EditorGUI.BeginDisabledGroup(true);
        Type type = this.currentTab switch
        {
            TabType.FloatVariable => typeof(FloatVariable),
            TabType.IntVariable => typeof(IntVariable),
            _ => null
        };
        EditorGUILayout.ObjectField(variable, type, false);
        EditorGUI.EndDisabledGroup();
    }

    private static void PingButton(ScriptableObjectVariable variable)
    {
        if (GUILayout.Button("Ping", GUILayout.Width(60)))
        {
            EditorGUIUtility.PingObject(variable);
        }
    }

    private static void SelectButton(ScriptableObjectVariable variable)
    {
        if (GUILayout.Button("Select", GUILayout.Width(60)))
        {
            Selection.activeObject = variable;
        }
    }

    private SerializedObject ShowFields(ScriptableObjectVariable variable)
    {
        SerializedObject serializedObject = new SerializedObject(variable);
        switch (this.currentTab)
        {
            case TabType.FloatVariable:
            case TabType.IntVariable:
                if (variable is IRandomVariable)
                {
                    Debug.Log("Random Variable: {variable.name}",
                        serializedObject.targetObject);
                    break;
                }

                ShowPrimitiveType(serializedObject);
                break;
        }

        serializedObject.ApplyModifiedProperties();
        return serializedObject;
    }

    private void ShowPrimitiveType(SerializedObject serializedObject)
    {
        SerializedProperty initValueProperty = serializedObject.FindProperty("initValue");
        EditorGUILayout.PropertyField(initValueProperty, new GUIContent("Initial Value"));
        SerializedProperty valueProperty = serializedObject.FindProperty("value");
        EditorGUILayout.PropertyField(valueProperty, new GUIContent("Current Value"));
        SerializedProperty logValueChangedProperty =
            serializedObject.FindProperty("logValueChanged");
        EditorGUILayout.PropertyField(logValueChangedProperty);
    }
}