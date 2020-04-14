using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FontAwesome
{
	public static class FontAwesomeLoader
	{
		private const string Path = "Packages/com.fontawesome.upm/Styles.uss";
		
		public static void ApplySheetTo(VisualElement element)
		{
			var sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(Path);
			element.styleSheets.Add(sheet);
		}
	}
}