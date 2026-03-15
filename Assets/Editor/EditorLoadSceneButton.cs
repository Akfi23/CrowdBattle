using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

namespace Editor
{

    [InitializeOnLoad]
    public class EditorLoadSceneButton
    {
        static class ToolbarStyles
        {
            public static readonly GUIStyle CommandButtonStyle;

            static ToolbarStyles()
            {
                CommandButtonStyle = new GUIStyle("Command")
                {
                    fontSize = 15,
                    alignment = TextAnchor.MiddleCenter,
                    imagePosition = ImagePosition.TextOnly,
                    fontStyle = FontStyle.Bold,
                    fixedWidth =100f
                };
            }
        }

        static EditorLoadSceneButton()
        {
            //ToolbarExtender.LeftToolbarGUI.Add(LeftToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(LeftToolbarGUI);
        }

        private static void LeftToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            GUI.enabled = !EditorApplication.isPlaying;

            GUI.backgroundColor = Color.cyan;

            if (GUILayout.Button(new GUIContent("SCENE", "Select scene"), ToolbarStyles.CommandButtonStyle, GUILayout.Width(600f)))
            {
                SelectSceneEditorWindow.OpenWindow();
            }

            GUI.backgroundColor = Color.white;
            GUI.enabled = true;
        }
    }
}