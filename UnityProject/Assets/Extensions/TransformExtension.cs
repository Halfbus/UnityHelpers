//  TransformExtension.cs
//  
//  Author:
//       Dmitry Minsky <dmitry.minsky@halfbusstudio.com>
// 
//  Copyright (c) 2013 HalfBus Studio

using UnityEngine;


public static class TransformExtension {

	public static void SetX( this Transform t, float val ) {
		t.position = new Vector3( val, t.position.y, t.position.z );
	}

	public static void SetY( this Transform t, float val ) {
		t.position = new Vector3( t.position.x, val, t.position.z );
	}

	public static void SetZ( this Transform t, float val ) {
		t.position = new Vector3( t.position.x, t.position.y, val );
	}

	public static void DrawGizm( this Transform t, float size = 1f ) {
		Gizmos.color = Color.red;
		Gizmos.DrawLine( t.position, t.position + t.right * size );
 
		Gizmos.color = Color.green;
		Gizmos.DrawLine( t.position, t.position + t.up * size );
 
		Gizmos.color = Color.blue;
		Gizmos.DrawLine( t.position, t.position + t.forward * size ); 
	}
	
}
