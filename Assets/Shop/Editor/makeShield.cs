using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class makeShield{
	[MenuItem("Assets/Create/Create Shield")]
	public static void Create()
	{
		ShieldObject asset = ScriptableObject.CreateInstance<ShieldObject> ();
		AssetDatabase.CreateAsset (asset, "Assets/Shop/Shields/NewShieldObject.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}

}
