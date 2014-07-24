using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BuildMachine : MonoBehaviour{
	static string[] enabledScenes = FindEnabledEditorScenes();
	static string[] scenes;
	static string targetDir;
	static BuildTarget currentBuildTarget;
	static BuildOptions options;

	[MenuItem("File/CI/Build IOS Player")]		
	static void PerformIOSBuild(){
		EditorUserBuildSettings.selectedBuildTargetGroup = BuildTargetGroup.iPhone;
		scenes = enabledScenes;
		targetDir = "IOSBuild";

		System.IO.Directory.CreateDirectory(Application.dataPath.Replace("Assets", targetDir));
		Debug.Log("Creating path:" + (Application.dataPath.Replace("Assets", targetDir)));

		currentBuildTarget = BuildTarget.iPhone;
		options = BuildOptions.Development;
		EditorUserBuildSettings.appendProject = false;

		print("Active Build Target:" + EditorUserBuildSettings.activeBuildTarget);
		
		PerformPlatformCheck();
	}

	[MenuItem("File/CI/Build IOS Player Profiled")]
	static void PerformIOSBuildProfiled(){
		Debug.Log("Enabling Profiling");
		EditorUserBuildSettings.development = true;
		EditorUserBuildSettings.connectProfiler = true;
		PerformIOSBuild();
	}

	static void PerformPlatformCheck(){
		if(EditorUserBuildSettings.activeBuildTarget != currentBuildTarget){
			Debug.Log("Switching BuildTarget");
			EditorUserBuildSettings.activeBuildTargetChanged += startBuildingPlayer;
			EditorUserBuildSettings.SwitchActiveBuildTarget(currentBuildTarget);			
		} else{
			Build();
		}
	}

	static void Build(){
		print("Start building player!");
		EditorUserBuildSettings.allowDebugging = true;
		EditorUserBuildSettings.symlinkLibraries = true;
		EditorUserBuildSettings.development = true;
		EditorUserBuildSettings.connectProfiler = true;
		EditorUserBuildSettings.symlinkLibraries = true;

		string res = BuildPipeline.BuildPlayer(scenes, targetDir, BuildTarget.iPhone, options);
		if(res.Length > 0){
			throw new System.Exception("BuildPlayer failed:" + res);
		}	
	}

	static string[] FindEnabledEditorScenes(){
		var EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes){
			if(!scene.enabled)
				continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}

	static void SwitchPlatform(){
		Debug.Log("Switching BuildTarget");
		EditorUserBuildSettings.activeBuildTargetChanged += startBuildingPlayer;
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iPhone);	
	}

	static void startBuildingPlayer(){
		EditorUserBuildSettings.activeBuildTargetChanged -= startBuildingPlayer;	
		Build();
	}
}