using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class makeWeapon {
	
	[MenuItem("Assets/Create/Create Weapon")]
	public static void Create()
	{
		WeaponObject asset = ScriptableObject.CreateInstance<WeaponObject> ();
		AssetDatabase.CreateAsset (asset, "Assets/Shop/Weapons/NewWeaponObject.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}

}
