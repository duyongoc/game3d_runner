using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ConfigScene)), CanEditMultipleObjects]
public class SceneConfigEditor : Editor
{
    public void ShowArrayProperty(UnityEditor.SerializedProperty list)
    {
        //list.arraySize = EditorGUILayout.IntField("Size", list.arraySize);
        UnityEditor.EditorGUI.indentLevel += 1;
        for (int i = 0; i < list.arraySize; i++)
        {
            UnityEditor.EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new UnityEngine.GUIContent("Set up Phase " + (i + 1).ToString()));
        }
        UnityEditor.EditorGUI.indentLevel -= 1;
    }

    public override void OnInspectorGUI()
    {
        ConfigScene configScene = (ConfigScene)target;

        GUIStyle title = new GUIStyle ();
        title.richText = true;
        GUILayout.Label("<size=15><color=yellow>This is a Label in a Custom Editor</color></size>",title);

        GUIStyle head = new GUIStyle ();
        head.richText = true;
        GUILayout.Label("<size=15><color=green>Set up Game's Phase</color></size>",head);

        //ShowArrayProperty(serializedObject.FindProperty("phases"));
        //EditorUtility.SetDirty(configScene);
        DrawDefaultInspector();
    }
}
#endif

[CreateAssetMenu(fileName = "Scene", menuName = "CONFIG/Scene/SceneDefault")]
public class ConfigScene : ScriptableObject
{
    public Phase[] phases;
}

[System.Serializable]
public class Phase
{
    public float timePhase;

    [Header("All of Enemies in Phase:")]
    public EnemyInPhase[] enemies;

}

[System.Serializable]
public class EnemyInPhase
{
    public enum E_Enemy { Default, Seek, Jump, Elastic }
    public E_Enemy enemyType;
    public float speedIncrease;
    public float timeSpawn;


}