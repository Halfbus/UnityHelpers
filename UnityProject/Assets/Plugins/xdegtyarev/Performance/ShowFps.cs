using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShowFps : MonoBehaviour {

	GUIText gui;

	float updateInterval = 1.0f;
	float lastInterval; // Last interval end time
	int frames = 0; // Frames over current interval


	void Start () {
		lastInterval = Time.realtimeSinceStartup;
		frames = 0;
	}


	void OnDisable()
	{
		if (gui)
			DestroyImmediate (gui.gameObject);
	}


	// Update is called once per frame
	void Update () {
		++frames;
		var timeNow = Time.realtimeSinceStartup;
		if (timeNow > lastInterval + updateInterval)
		{
			if (!gui)
			{
				GameObject go = new GameObject("FPS Display", typeof(GUIText));
				go.hideFlags = HideFlags.HideAndDontSave;
				go.transform.position = new Vector3(0.2f, 0.9f, 0);
				gui = go.GetComponent<GUIText>();
				gui.pixelOffset = new Vector2(5,55);
			}

			float fps = frames / (timeNow - lastInterval);
			float ms = 1000.0f / Mathf.Max(fps, 0.00001f);
			gui.text = ms.ToString("f1") + "ms " + fps.ToString("f2") + "FPS";
			frames = 0;
			lastInterval = timeNow;
		}
	}
}
