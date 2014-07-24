//  Vector3Extension.cs
//  
//  Author:
//       Dmitry Minsky <dmitry.minsky@halfbusstudio.com>
// 
//  Copyright (c) 2013 HalfBus Studio

using UnityEngine;


public static class Vector3Extension {

	public static Vector3 SetX( this Vector3 vector3, float val ) {
		vector3.x = val;
		return vector3;
	}

	public static Vector3 SetY( this Vector3 vector3, float val ) {
		vector3.y = val;
		return vector3;
	}

	public static Vector3 SetZ( this Vector3 vector3, float val ) {
		vector3.z = val;
		return vector3;
	}

}
