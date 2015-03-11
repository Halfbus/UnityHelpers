//
// Screener.cs
//
// Author:
//        Dmitry Minsky <dmitry.minsky@halfbusstudio.com>
//
// Copyright (c) 2014 HalfBus Studio
//

using UnityEngine;
using System.Collections;


public class Screener : MonoBehaviour {

	// Capture camera and save it as PNG file.
	public void SaveCameraToPNG() {

		// To minimize aliasing render in double resolution
		int sqrw = Screen.width * 2;
		int sqrh = Screen.height * 2;

		// The "24" can be 0,16,24 or formats like RenderTextureFormat.Default, ARGB32 etc.
		RenderTexture tempRT = new RenderTexture( sqrw, sqrh, 24, RenderTextureFormat.ARGB32 ); 
		tempRT.filterMode = FilterMode.Trilinear;
		GetComponent<Camera>().targetTexture = tempRT;
		GetComponent<Camera>().Render();
		RenderTexture.active = tempRT;

		Texture2D virtualPhoto = new Texture2D( sqrw,sqrh, TextureFormat.ARGB32, false ); // false, meaning no need for mipmaps
		virtualPhoto.ReadPixels( new Rect( 0, 0, sqrw,sqrh ), 0, 0 ); // you get the center section
		
		RenderTexture.active = null; // "just in case" 
		GetComponent<Camera>().targetTexture = null;
		//Destroy(tempRT); - tricky on android and other platforms, take care

		// Write to file
		byte[] bytes;
		bytes = virtualPhoto.EncodeToPNG();
		System.IO.File.WriteAllBytes( GetScreenshotFileName(), bytes );
	}

	// You can write your own policy, for example datetime as filename
	private string GetScreenshotFileName() {
		return Application.persistentDataPath + "/screenshot.png";
	}

}
