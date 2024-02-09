using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimatorHelper))]
public class AnimatorHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Display a help box
        EditorGUILayout.HelpBox($"This class provides helper methods for managing NPC animation switch, and is inherited from the 'NpcAnimatorHelper' script", MessageType.Info);
    }
}
