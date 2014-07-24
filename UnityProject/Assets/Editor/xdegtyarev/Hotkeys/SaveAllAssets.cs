using UnityEngine;
using UnityEditor;

public class SaveAllAssets : MonoBehaviour {

		[MenuItem ("File/Save All Assets &#s")]
		static void SaveAssets ()
		{
			AssetDatabase.SaveAssets();
			Debug.Log("Assets Saved");
		}
}
