using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MaterialByShaderSelectionUtil : EditorWindow
{
	[MenuItem("Halfbus/Select materials")]
	static void Init()
	{
		MaterialByShaderSelectionUtil window = (MaterialByShaderSelectionUtil)EditorWindow.GetWindow(typeof(MaterialByShaderSelectionUtil));
		window.Show();
	}

	public Object target;
	List<Object> selections = new List<Object>();

	void OnGUI()
	{
		if (Selection.activeObject is Shader) {
			target = Selection.activeObject as Shader;
		}
		
		target = EditorGUILayout.ObjectField(target, typeof(Shader), false);
		
		if (target) {
			if (GUILayout.Button("Select materials by shader")) {
				
				selections.Clear();
				var currentShader = target as Shader;
				foreach (var o in AssetDatabase.GetAllAssetPaths()) {
					if (o.Contains(".mat")) {
						Material mat = AssetDatabase.LoadAssetAtPath(o, typeof(Material)) as Material;
						if (mat != null) {
							if (mat.shader == currentShader) {
								selections.Add(mat);		
							}
						}
					}
				}
				Debug.Log("Materials selected: " + selections.Count);
				Selection.objects = selections.ToArray();
			}
		}
				
		if (selections.Count > 0) {
			foreach (var o in selections) {
				EditorGUILayout.ObjectField(o, typeof(Material), false);
			}
		} else {
			EditorGUILayout.LabelField("There is no materials that use current shader");
		}
	}
}
