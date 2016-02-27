using UnityEngine;
using System.Collections;
using UnityEditor;

// OBJECTMASTER 1.4.2 Unity Plugin
// Created by Ryan Miller ryan@reptoidgames.com

namespace ObjectMaster {

	public class ObjectMaster : EditorWindow {

		#region ObjectMasterWindow

		[MenuItem ("Window/ObjectMaster")]
		public static void ShowWindow() {
			//Show existing window instance. If one doesn't exist, make one.
			EditorWindow bar = EditorWindow.GetWindow(typeof(ObjectMaster));

#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
			bar.title = "ObjectMaster";
#else
			bar.titleContent = new GUIContent ("ObjectMaster");
#endif
			bar.minSize = new Vector2(200, 500);
			bar.maxSize = new Vector2(350, 2000);
		}

		#endregion

		#region ToolParameters 

		public float scatter = 5f, stickRadius = 10f;
		public bool showWarnings = true;

		enum AffectType { 
			Transform = 0, 
			Position = 1,
			Rotation = 2,
			Scale = 3
		};
		AffectType affect;
		AffectType roundAffect;
		AffectType zeroAffect;
		AffectType clipAffect;

		enum ScatterType {
			IntoCircle = 0,
			IntoSquare = 1,
			IntoSphere = 2,
			IntoCube = 3
		}
		ScatterType howScatter = ScatterType.IntoCircle;

		bool useX, useY, useZ;
		float minScale = 0.5f, maxScale = 2f;
		float turnAmount = 90;

		string localGlobal;

		public string findString, replaceString;

		private GUISkin omSkin;

		private Vector2 scroll = new Vector2(0,0);

		public bool collapseGeneral = true, 
					collapseClipboard = true, 
					collapseFindReplace = true,
					collapseRandomize = true, 
					collapseSpin = true, 
					collapseScatter = true, 
					collapseSelection = true, 
					collapseWarning = true,
					collapseLinks = true;

		#endregion

		#region GUIContent 

		void OnInspectorUpdate() {
			Repaint();
		}

		void OnGUI() {

			if (omSkin == null) {
				omSkin = Resources.Load ("ObjectMaster") as GUISkin;
			}

			scroll = GUILayout.BeginScrollView (scroll);

			GUI.skin = omSkin;

			GUILayout.Label ("ObjectMaster 1.4.2");

			#region General

			collapseGeneral = EditorGUILayout.Foldout(collapseGeneral, "General");

			if (collapseGeneral) {
				GUILayout.BeginVertical("Box");

				GUILayout.Label("Generic Tools");

				if (GUILayout.Button ("Group")) {
					Utilities.Group ();
				}

				if (GUILayout.Button ("Unparent")) {
					Utilities.Unparent ();
				}

				if (GUILayout.Button ("Ground")) {
					Utilities.Ground ();
				}

				GUILayout.BeginHorizontal();
				if (GUILayout.Button ("Join Nearby")) {
					Utilities.ChangeBuddy (stickRadius);
					Utilities.Buddy ();
				}
				stickRadius = EditorGUILayout.FloatField (stickRadius);
				GUILayout.EndHorizontal();

				if (GUILayout.Button ("Wrap in GameObject")) {
					Utilities.WrapObject ();
				}

				if (GUILayout.Button ("Bring Near / View")) {
					Utilities.BringNear ();
				}

				GUILayout.BeginHorizontal();
				if (GUILayout.Button ("Round To Int")) {
					switch (roundAffect) {
					case AffectType.Transform:
						Utilities.Snap ();
						break;
					case AffectType.Position:
						Utilities.SnapPosition ();
						break;
					case AffectType.Rotation:
						Utilities.SnapRotation ();
						break;
					case AffectType.Scale:
						Utilities.SnapScale ();
						break;
					}
				}
				roundAffect = (AffectType)EditorGUILayout.EnumPopup (roundAffect);
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				if (GUILayout.Button ("Zero Values")) {
					switch (zeroAffect) {
					case AffectType.Transform:
						Utilities.ZeroAllTransforms ();
						break;
					case AffectType.Position:
						Utilities.ZeroPosition ();
						break;
					case AffectType.Rotation:
						Utilities.ZeroRotation ();
						break;
					case AffectType.Scale:
						Utilities.ResetScale ();
						break;
					}
				}
				zeroAffect = (AffectType)EditorGUILayout.EnumPopup (zeroAffect);
				GUILayout.EndHorizontal();

				GUILayout.EndVertical ();
			}

			#endregion

			#region Clipboard

			collapseClipboard = EditorGUILayout.Foldout(collapseClipboard, "Attribute Clipboard");
			
			if (collapseClipboard) {

				// BRING CLOSER
				GUILayout.BeginVertical ("Box");

				GUILayout.Label("Copy and Paste Attributes");

				if (Utilities.localCoords) {
					localGlobal = "Use Local Coords";
				} else {
					localGlobal = "Use Global Coords";
				}
				
				if (GUILayout.Button (localGlobal)) {
					Utilities.ToggleLocalGlobal ();
				}

				clipAffect = (AffectType)EditorGUILayout.EnumPopup (clipAffect);

				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Copy")) {
					switch (clipAffect) {
					case AffectType.Transform:
						Utilities.CopyTransforms ();
						break;
					case AffectType.Position:
						Utilities.CopyPosition ();
						break;
					case AffectType.Rotation:
						Utilities.CopyRotation ();
						break;
					case AffectType.Scale:
						Utilities.CopyScale ();
						break;
					}
				}
				if (GUILayout.Button ("Paste")) {
					switch (clipAffect) {
					case AffectType.Transform:
						Utilities.PasteTransforms ();
						break;
					case AffectType.Position:
						Utilities.PastePosition ();
						break;
					case AffectType.Rotation:
						Utilities.PasteRotation ();
						break;
					case AffectType.Scale:
						Utilities.PasteScale ();
						break;
					}
				}
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
			}

			#endregion

			#region NameReplace

			collapseFindReplace = EditorGUILayout.Foldout(collapseFindReplace, "Naming and Replacing");

			if (collapseFindReplace) {
				GUILayout.BeginVertical ("Box");

				GUILayout.Label("Naming and Replacing");

				if (GUILayout.Button("Find & Replace Names")) {
					// Get existing open window or if none, make a new one:
					FindReplaceNames window = (FindReplaceNames)EditorWindow.GetWindow(typeof(FindReplaceNames));
					#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
					window.title = "Replace Names";
					#else
					window.titleContent = new GUIContent ("Replace Names");
					#endif
					window.Show();
				}

				if (GUILayout.Button("Find & Replace Objects")) {
					// Get existing open window or if none, make a new one:
					FindReplaceObjects window = (FindReplaceObjects)EditorWindow.GetWindow(typeof(FindReplaceObjects));
					#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
					window.title = "Replace Objects";
					#else
					window.titleContent = new GUIContent ("Replace Objects");
					#endif
					window.Show();
				}

				if (GUILayout.Button("Rename Master")) {
					// Get existing open window or if none, make a new one:
					RenameMaster window = (RenameMaster)EditorWindow.GetWindow(typeof(RenameMaster));
					#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
					window.title = "Rename";
					#else
					window.titleContent = new GUIContent ("Rename");
					#endif
					window.Show();
				}

				GUILayout.EndVertical();
			}

			#endregion

			#region Random

			collapseRandomize = EditorGUILayout.Foldout(collapseRandomize, "Randomize");

			if (collapseRandomize) {
				GUILayout.BeginVertical("Box");

				GUILayout.Label("Random Rotation or Scale");

				GUILayout.BeginHorizontal();
				GUILayout.Label("X");
				useX = EditorGUILayout.Toggle(useX);
				GUILayout.Label("Y");
				useY = EditorGUILayout.Toggle(useY);
				GUILayout.Label("Z");
				useZ = EditorGUILayout.Toggle(useZ);
				GUILayout.EndHorizontal();

				if (GUILayout.Button("Rotation"))
				{
					if (useX) {Utilities.RotateRandomX();}
					if (useY) {Utilities.RotateRandomY();}
					if (useZ) {Utilities.RotateRandomZ();}
				}
				if (GUILayout.Button("Scale"))
				{
					Utilities.minScale = minScale;
					Utilities.maxScale = maxScale;
					Vector3 scaleRandoms = Vector3.one;
					if (useX) { scaleRandoms.x = 1; } else { scaleRandoms.x = 0; }
					if (useY) { scaleRandoms.y = 1; } else { scaleRandoms.y = 0; }
					if (useZ) { scaleRandoms.z = 1; } else { scaleRandoms.z = 0; }
					Utilities.ScaleRandomEditor(scaleRandoms);
				}

				minScale = EditorGUILayout.FloatField("Min:", minScale);
				maxScale = EditorGUILayout.FloatField("Max:", maxScale);

				GUILayout.EndVertical();
			}

			#endregion

			#region TurnObjects

			collapseSpin = EditorGUILayout.Foldout(collapseSpin, "Turn Objects");
			
			if (collapseSpin) {

				GUILayout.BeginVertical("Box");

				GUILayout.Label("Turn Object on Axis");

				turnAmount = EditorGUILayout.FloatField("Degrees:", turnAmount);

				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("X")) {
					Utilities.Rotate(new Vector3(turnAmount,0,0));
				}
				if (GUILayout.Button ("Y")) {
					Utilities.Rotate(new Vector3(0,turnAmount,0));
				}
				if (GUILayout.Button ("Z")) {
					Utilities.Rotate(new Vector3(0,0,turnAmount));
				}
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
			}

			#endregion

			#region Scatter

			collapseScatter = EditorGUILayout.Foldout(collapseScatter, "Scatter Objects");
			
			if (collapseScatter) {

				GUILayout.BeginVertical("Box");

				GUILayout.Label("Scatter Selected Objects");

				howScatter = (ScatterType)EditorGUILayout.EnumPopup(howScatter);

				scatter = EditorGUILayout.FloatField ("Radius:", scatter);

				if (GUILayout.Button("Scatter"))
				{
					Utilities.ChangeScatter(scatter);
					
					if (howScatter == ScatterType.IntoCircle) {
						Utilities.ScatterCircle();
					}
					else if (howScatter == ScatterType.IntoCube) {
						Utilities.ScatterCube();
					}
					else if (howScatter == ScatterType.IntoSphere) {
						Utilities.ScatterSphere();
					}
					else if (howScatter == ScatterType.IntoSquare) {
						Utilities.ScatterSquare();
					}
				}

				GUILayout.EndVertical ();
			}

			#endregion

			#region Links

			collapseLinks = EditorGUILayout.Foldout(collapseLinks, "Links");

			if (collapseLinks) {
				GUILayout.BeginVertical("Box");
				GUILayout.Label("Links");

				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Store Page")) {
					Application.OpenURL("http://bit.ly/1jxh6Uo");
				}
				if (GUILayout.Button ("Website")) {
					Application.OpenURL("http://bit.ly/1W0XrrN");
				}
				GUILayout.EndHorizontal ();

				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Forum")) {
					Application.OpenURL("http://bit.ly/1NkqN27");
				}
				if (GUILayout.Button ("Intro Video")) {
					Application.OpenURL("http://bit.ly/1GP9bq9");
				}
				
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical();
			}
			#endregion

			GUILayout.EndScrollView ();
		}

		#endregion
	}
}
