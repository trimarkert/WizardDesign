using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

// OBJECTMASTER 1.4.2 Unity Plugin
// Created by Ryan Miller ryan@reptoidgames.com

namespace ObjectMaster
{
    [ExecuteInEditMode]
    public class Utilities : Editor
    {
		#region CopyAndPaste
		[MenuItem("Edit/ObjectMaster/Copy/",  false, 1)]
		public static void MenuCopy() {}

		[MenuItem("Edit/ObjectMaster/Copy/Copy Position")]
        public static void CopyPosition()
        {
            if (Selection.transforms.Length > 0)
            {
                if (Selection.transforms.Length != 1)
                {
                    LogWarning("Copy Position requires only one transform be selected");
                }
                else
                {
                    if (localCoords)
                    {
                        PlayerPrefs.SetFloat("CGTools_SavedPosX", Selection.activeTransform.localPosition.x);
                        PlayerPrefs.SetFloat("CGTools_SavedPosY", Selection.activeTransform.localPosition.y);
                        PlayerPrefs.SetFloat("CGTools_SavedPosZ", Selection.activeTransform.localPosition.z);
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("CGTools_SavedPosX", Selection.activeTransform.position.x);
                        PlayerPrefs.SetFloat("CGTools_SavedPosY", Selection.activeTransform.position.y);
                        PlayerPrefs.SetFloat("CGTools_SavedPosZ", Selection.activeTransform.position.z);
                    }
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

		[MenuItem("Edit/ObjectMaster/Paste/",  false, 2)]
		public static void Paste() {}

        [MenuItem("Edit/ObjectMaster/Paste/Paste Position")]
        public static void PastePosition()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Paste Position");

                Vector3 savedPosition = new Vector3(PlayerPrefs.GetFloat("CGTools_SavedPosX"), PlayerPrefs.GetFloat("CGTools_SavedPosY"), PlayerPrefs.GetFloat("CGTools_SavedPosZ"));

                foreach (Transform aT in Selection.transforms)
                {
                    if (localCoords)
                    {
                        aT.transform.localPosition = savedPosition;
                    }
                    else
                    {
                        aT.transform.position = savedPosition;
                    }
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

        [MenuItem("Edit/ObjectMaster/Copy/Copy Rotation")]
        public static void CopyRotation()
        {
            if (Selection.transforms.Length > 0)
            {
                if (Selection.transforms.Length != 1)
                {
                    LogWarning("Copy Rotation requires only one transform be selected");
                }
                else
                {
                    if (localCoords)
                    {
                        PlayerPrefs.SetFloat("CGTools_SavedRotX", Selection.activeTransform.localEulerAngles.x);
                        PlayerPrefs.SetFloat("CGTools_SavedRotY", Selection.activeTransform.localEulerAngles.y);
                        PlayerPrefs.SetFloat("CGTools_SavedRotZ", Selection.activeTransform.localEulerAngles.z);
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("CGTools_SavedRotX", Selection.activeTransform.eulerAngles.x);
                        PlayerPrefs.SetFloat("CGTools_SavedRotY", Selection.activeTransform.eulerAngles.y);
                        PlayerPrefs.SetFloat("CGTools_SavedRotZ", Selection.activeTransform.eulerAngles.z);
                    }
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

        [MenuItem("Edit/ObjectMaster/Paste/Paste Rotation")]
        public static void PasteRotation()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Paste Rotation");

                Vector3 savedRotation = new Vector3(PlayerPrefs.GetFloat("CGTools_SavedRotX"), PlayerPrefs.GetFloat("CGTools_SavedRotY"), PlayerPrefs.GetFloat("CGTools_SavedRotZ"));

                foreach (Transform aT in Selection.transforms)
                {
                    if (localCoords)
                    {
                        aT.transform.localRotation = Quaternion.Euler(savedRotation);
                    }
                    else
                    {
                        aT.transform.rotation = Quaternion.Euler(savedRotation);
                    }
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

        [MenuItem("Edit/ObjectMaster/Copy/Copy Scale")]
        public static void CopyScale()
        {
            if (Selection.transforms.Length > 0)
            {
                if (Selection.transforms.Length != 1)
                {
                    LogWarning("Copy Scale requires only one transform be selected");
                }
                else
                {
                    PlayerPrefs.SetFloat("CGTools_SavedScaleX", Selection.activeTransform.localScale.x);
                    PlayerPrefs.SetFloat("CGTools_SavedScaleY", Selection.activeTransform.localScale.y);
                    PlayerPrefs.SetFloat("CGTools_SavedScaleZ", Selection.activeTransform.localScale.z);
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

        [MenuItem("Edit/ObjectMaster/Paste/Paste Scale")]
        public static void PasteScale()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Paste Scale");

                Vector3 savedScale = new Vector3(PlayerPrefs.GetFloat("CGTools_SavedScaleX"), PlayerPrefs.GetFloat("CGTools_SavedScaleY"), PlayerPrefs.GetFloat("CGTools_SavedScaleZ"));

                foreach (Transform aT in Selection.transforms)
                {
                    aT.transform.localScale = savedScale;
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

		[MenuItem("Edit/ObjectMaster/Copy/Copy Transforms")]
		public static void CopyTransforms()
		{
			if (Selection.transforms.Length > 0)
			{
				CopyPosition();
				CopyRotation();
				CopyScale();
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Paste/Paste Transforms")]
		public static void PasteTransforms()
		{
			if (Selection.transforms.Length > 0)
			{
				PastePosition();
				PasteRotation();
				PasteScale();
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}

		#endregion

		#region FindReplace

		[MenuItem("Edit/ObjectMaster/Find and Replace/",  false, 3)]
		public static void MenuFindReplace() {}


        public static void FindReplaceNames(string find, string replace, bool withinSelection)
        {

            if (withinSelection)
            {
                if (Selection.gameObjects.Length > 0)
                {
                    Undo.RecordObjects(Selection.gameObjects, "Find Replace Names");
                    foreach (GameObject gO in Selection.gameObjects)
                    {
                        gO.name = gO.name.Replace(find, replace);
                    }
                }
                else
                {
                    LogWarning("You must select one or more objects.");
                }
            }
            else
            {
                //Undo.RecordObjects(Selection.gameObjects, "Find Replace Names");
                foreach (GameObject gO in FindObjectsOfType<GameObject>())
                {
                    gO.name = gO.name.Replace(find, replace);
                }
            }
        }

        public static void FindReplaceObjects(string find, GameObject replace, bool withinSelection, bool byTag)
        {

            if (withinSelection)
            {
                if (Selection.gameObjects.Length > 0)
                {
                    Undo.RecordObjects(Selection.gameObjects, "Find Replace Objects");
                    foreach (GameObject gO in Selection.gameObjects)
                    {
                        if (!byTag && gO.name.Contains(find))
                        {
                            GameObject newObj = Instantiate(replace, gO.transform.position, gO.transform.rotation) as GameObject;
                            newObj.transform.parent = gO.transform.parent;
                            newObj.name = newObj.name.Replace("(Clone)", "");
                            DestroyImmediate(gO);
                        }
                        else if (byTag && gO.tag.Contains(find))
                        {
                            GameObject newObj = Instantiate(replace, gO.transform.position, gO.transform.rotation) as GameObject;
                            newObj.transform.parent = gO.transform.parent;
                            newObj.name = newObj.name.Replace("(Clone)", "");
                            DestroyImmediate(gO);
                        }
                    }
                }
                else
                {
                    LogWarning("You must select one or more objects.");
                }
            }
            else
            {
                //Undo.RecordObjects(Selection.gameObjects, "Find Replace Objects");
                foreach (GameObject gO in FindObjectsOfType<GameObject>())
                {
                    if (!byTag && gO.name.Contains(find))
                    {
                        GameObject newObj = Instantiate(replace, gO.transform.position, gO.transform.rotation) as GameObject;
                        newObj.transform.parent = gO.transform.parent;
                        newObj.name = newObj.name.Replace("(Clone)", "");
                        DestroyImmediate(gO);
                    }
                    else if (byTag && gO.tag.Contains(find))
                    {
                        GameObject newObj = Instantiate(replace, gO.transform.position, gO.transform.rotation) as GameObject;
                        newObj.transform.parent = gO.transform.parent;
                        newObj.name = newObj.name.Replace("(Clone)", "");
                        DestroyImmediate(gO);
                    }
                }
            }
        }

		#endregion

		#region RenameMaster
		
		public static void RenameNames(string newName, bool isSuffix, bool isPrefix, bool numberItems, bool sortInHierarchy, int padding, int startNum, int incrementBy)
		{
			int indexCount = startNum;
			string padFormat = "";
			for (int padNum = 0; padNum < padding; padNum++) {
				padFormat += "0";
			}

			for (int itemIndex = 0; itemIndex < Selection.transforms.Length; itemIndex ++) {

				string thisItemName = Selection.transforms[itemIndex].name;

				// add name 
				if (!isPrefix && !isSuffix) {
					Debug.Log("Rename without suffix or prefix");
					Selection.transforms[itemIndex].name = newName;
				}
				else {
					if (isPrefix) {
						Debug.Log("rename with Prefix");
						Selection.transforms[itemIndex].name = newName + thisItemName;
					}
					if (isSuffix) {
						Debug.Log("rename with Suffix");
						Selection.transforms[itemIndex].name = thisItemName + newName;
					}
				}

				if (numberItems) {
					// add trailing number
					Selection.transforms[itemIndex].name += indexCount.ToString(padFormat);
					indexCount += incrementBy;
				}
			}

			if (sortInHierarchy) {
				for (int itemIndex = 0; itemIndex < Selection.transforms.Length; itemIndex ++) {
					Selection.transforms[itemIndex].SetSiblingIndex(itemIndex);
				}
			}
		}
		#endregion

		#region ZeroTransforms

		[MenuItem("Edit/ObjectMaster/Zero Transforms/",  false, 4)]
		public static void MenuZeroTransforms() {}


		[MenuItem("Edit/ObjectMaster/Zero Transforms/Zero All Transforms")]
		public static void ZeroAllTransforms()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Zero Transforms");
				
				foreach (Transform aT in Selection.transforms)
				{
					aT.localPosition = Vector3.zero;
					aT.rotation = Quaternion.Euler(0, 0, 0);
					aT.localScale = Vector3.one;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Zero Transforms/Zero Position")]
		public static void ZeroPosition()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Zero Positions");
				
				foreach (Transform aT in Selection.transforms)
				{
					aT.localPosition = Vector3.zero;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Zero Transforms/Zero Rotation")]
		public static void ZeroRotation()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Zero Rotations");
				
				foreach (Transform aT in Selection.transforms)
				{
					aT.rotation = Quaternion.Euler(0, 0, 0);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Zero Transforms/Reset Scale")]
		public static void ResetScale()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Reset Scales");
				
				foreach (Transform aT in Selection.transforms)
				{
					aT.localScale = Vector3.one;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		#endregion

		#region Snapping

		[MenuItem("Edit/ObjectMaster/Snap/",  false, 5)]
		public static void MenuSnap() {}


		[MenuItem("Edit/ObjectMaster/Snap/Snap All")]
		public static void Snap()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Snap All");
				
				SnapPosition();
				SnapRotation();
				SnapScale();
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Snap/Snap Position")]
		public static void SnapPosition()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Snap Position");
				
				foreach (Transform s in Selection.transforms)
				{
					Vector3 snapValue = s.position;
					snapValue.x = Mathf.RoundToInt(snapValue.x);
					snapValue.y = Mathf.RoundToInt(snapValue.y);
					snapValue.z = Mathf.RoundToInt(snapValue.z);
					s.position = snapValue;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Snap/Snap Rotation")]
		public static void SnapRotation()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Snap Rotation");
				
				foreach (Transform s in Selection.transforms)
				{
					Vector3 snapValue = s.eulerAngles;
					snapValue.x = Mathf.RoundToInt(snapValue.x / 45f) * 45f;
					snapValue.y = Mathf.RoundToInt(snapValue.y / 45f) * 45f;
					snapValue.z = Mathf.RoundToInt(snapValue.z / 45f) * 45f;
					
					s.eulerAngles = snapValue;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Snap/Snap Scale")]
		public static void SnapScale()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Snap Scale");
				
				foreach (Transform s in Selection.transforms)
				{
					Vector3 snapValue = s.localScale;
					// don't let scale go below 1
					snapValue.x = Mathf.Clamp(Mathf.RoundToInt(snapValue.x), 1, Mathf.Infinity);
					snapValue.y = Mathf.Clamp(Mathf.RoundToInt(snapValue.y), 1, Mathf.Infinity);
					snapValue.z = Mathf.Clamp(Mathf.RoundToInt(snapValue.z), 1, Mathf.Infinity);
					
					s.localScale = snapValue;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		#endregion

		#region Scatter

		[MenuItem("Edit/ObjectMaster/Scatter/",  false, 6)]
		public static void MenuScatter() {}

		// distance to randomize within
		public static float scatterDistance = 5f;
		
		public static void ChangeScatter(float newDist)
		{
			scatterDistance = newDist;
		}
		
		[MenuItem("Edit/ObjectMaster/Scatter/Into Circle")]
		public static void ScatterCircle()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Scatter");
				
				Vector3 pivotPosition = Vector3.zero;
				Ray camRay = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
				RaycastHit camHit;
				if (Physics.Raycast(camRay, out camHit))
				{
					pivotPosition = camHit.point;
				}
				
				foreach (Transform aT in Selection.transforms)
				{
					Quaternion originalRotation = aT.transform.rotation;
					aT.transform.position = pivotPosition;
					aT.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
					aT.transform.Translate(aT.transform.forward * UnityEngine.Random.Range(0, scatterDistance), Space.World);
					aT.transform.rotation = originalRotation;
					GroundTransform(aT);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Scatter/Into Sphere")]
		public static void ScatterSphere()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Scatter");
				
				Vector3 pivotPosition = Vector3.zero;
				Ray camRay = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
				RaycastHit camHit;
				if (Physics.Raycast(camRay, out camHit))
				{
					pivotPosition = camHit.point;
				}
				
				foreach (Transform aT in Selection.transforms)
				{
					Quaternion originalRotation = aT.transform.rotation;
					aT.transform.position = pivotPosition;
					aT.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
					aT.transform.Translate(aT.transform.forward * UnityEngine.Random.Range(0, scatterDistance), Space.World);
					aT.transform.rotation = originalRotation;
					GroundTransform(aT);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Scatter/Into Square")]
		public static void ScatterSquare()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Scatter");
				
				Vector3 pivotPosition = Vector3.zero;
				Ray camRay = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
				RaycastHit camHit;
				if (Physics.Raycast(camRay, out camHit))
				{
					pivotPosition = camHit.point;
				}
				
				foreach (Transform aT in Selection.transforms)
				{
					aT.transform.position = pivotPosition;
					aT.transform.Translate(UnityEngine.Random.Range(-scatterDistance, scatterDistance), 0, UnityEngine.Random.Range(-scatterDistance, scatterDistance), Space.World);
					GroundTransform(aT);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Scatter/Into Cube")]
		public static void ScatterCube()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Scatter");
				
				Vector3 pivotPosition = Vector3.zero;
				Ray camRay = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
				RaycastHit camHit;
				if (Physics.Raycast(camRay, out camHit))
				{
					pivotPosition = camHit.point;
				}
				
				foreach (Transform aT in Selection.transforms)
				{
					aT.transform.position = pivotPosition;
					aT.transform.Translate(UnityEngine.Random.Range(-scatterDistance, scatterDistance), UnityEngine.Random.Range(-scatterDistance, scatterDistance), UnityEngine.Random.Range(-scatterDistance, scatterDistance), Space.World);
					GroundTransform(aT);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		#endregion

		#region Randomize

		[MenuItem("Edit/ObjectMaster/Randomize/",  false, 7)]
		public static void MenuRandomize() {}

		public static void Rotate(Vector3 rot)
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Rotate");
				foreach (Transform s in Selection.transforms)
				{
					s.transform.Rotate(rot, Space.World);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}

		[MenuItem("Edit/ObjectMaster/Randomize/Rotations")]
		public static void RotateRandom()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Random Rotate All");
				RotateRandomX();
				RotateRandomY();
				RotateRandomZ();
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Randomize/Rotate X")]
		public static void RotateRandomX()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Random Rotate X");
				
				foreach (GameObject s in Selection.gameObjects)
				{
					s.transform.Rotate(UnityEngine.Random.Range(-180, 180), 0, 0);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Randomize/Rotate Y")]
		public static void RotateRandomY()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Random Rotate Y");
				
				foreach (GameObject s in Selection.gameObjects)
				{
					s.transform.Rotate(0, UnityEngine.Random.Range(-180, 180), 0);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Randomize/Rotate Z")]
		public static void RotateRandomZ()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Random Rotate Z");
				
				foreach (GameObject s in Selection.gameObjects)
				{
					s.transform.Rotate(0, 0, UnityEngine.Random.Range(-180, 180));
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		public static float minScale = 0.5f, maxScale = 2f;
		
		[MenuItem("Edit/ObjectMaster/Randomize/Scale")]
		public static void ScaleRandomAll()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Random Scale All");
				ScaleRandom(new Vector3(1, 1, 1));
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		public static void ScaleRandomEditor(Vector3 rand)
		{
			if (Selection.transforms.Length > 0)
			{
				ScaleRandom(rand);
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Randomize/Scale X")]
		public static void ScaleRandomX()
		{
			if (Selection.transforms.Length > 0)
			{
				ScaleRandom(new Vector3(1, 0, 0));
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Randomize/Scale Y")]
		public static void ScaleRandomY()
		{
			if (Selection.transforms.Length > 0)
			{
				ScaleRandom(new Vector3(0, 1, 0));
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		[MenuItem("Edit/ObjectMaster/Randomize/Scale Z")]
		public static void ScaleRandomZ()
		{
			if (Selection.transforms.Length > 0)
			{
				ScaleRandom(new Vector3(0, 0, 1));
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		public static void ScaleRandom(Vector3 scaleAxes)
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Random Scale");
				
				foreach (GameObject s in Selection.gameObjects)
				{
					Vector3 scaleAmount = s.transform.localScale;
					float rand = UnityEngine.Random.Range(minScale, maxScale);
					if (scaleAxes.x != 0) { scaleAmount.x = rand; }
					if (scaleAxes.y != 0) { scaleAmount.y = rand; }
					if (scaleAxes.z != 0) { scaleAmount.z = rand; }
					s.transform.localScale = scaleAmount;
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		#endregion
			
		#region Config
		
		public static bool localCoords = true;

		[MenuItem("Edit/ObjectMaster/Config/",  false, 8)]
		public static void MenuConfig() {}

		[MenuItem("Edit/ObjectMaster/Config/Use Local Coords")]
		public static void UseLocalCoords()
		{
			localCoords = true;
		}
		[MenuItem("Edit/ObjectMaster/Config/Use Global Coords")]
		public static void UseGlobalCoords()
		{
			localCoords = false;
		}
		
		public static void ToggleLocalGlobal()
		{
			localCoords = !localCoords;
		}
		
		public static bool showWarnings = false;

		[MenuItem("Edit/ObjectMaster/Config/Disable Warnings")]
		public static void MenuDisableWarnings()
		{
			Debug.Log ("ObjectMaster warnings disabled.");
			SetWarnings (false);
		}
		
		[MenuItem("Edit/ObjectMaster/Config/Enable Warnings")]
		public static void MenuEnableWarnings()
		{
			Debug.LogWarning ("ObjectMaster warnings enabled.");
			SetWarnings (true);
		}
		
		public static void SetWarnings(bool val)
		{
			showWarnings = val;
		}
		
		public static void LogWarning(string warning)
		{
			if (showWarnings)
			{
				Debug.LogWarning(warning);
			}
			
		}
		
		#endregion

		#region Parenting

		[MenuItem("Edit/ObjectMaster/Parent",  false, 9)]
		public static void Parent()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Parent");

                foreach (Transform aT in Selection.transforms)
                {
                    if (aT != Selection.transforms[Selection.transforms.Length - 1])
                    {
						Undo.SetTransformParent(aT.transform, Selection.transforms[Selection.transforms.Length - 1], "Parent");
                    }
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

        [MenuItem("Edit/ObjectMaster/Unparent", false, 9)]
        public static void Unparent()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Unparent");

                foreach (Transform aT in Selection.transforms)
                {
					Undo.SetTransformParent(aT.transform, null, "Unparent");
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

		#endregion

		#region Group
	
		[MenuItem("Edit/ObjectMaster/Group %g",  false, 10)]
		public static void Group()
        {
            if (Selection.transforms.Length > 0)
            {
                GameObject group = new GameObject("Group");

                Vector3 pivotPosition = Vector3.zero;
                Ray camRay = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                RaycastHit camHit;
                if (Physics.Raycast(camRay, out camHit))
                {
                    pivotPosition = camHit.point;
                }

                group.transform.position = pivotPosition;

                Undo.RegisterCreatedObjectUndo(group, "Group");

                foreach (GameObject s in Selection.gameObjects)
                {
                    Undo.SetTransformParent(s.transform, group.transform, "Group");
                }

                Selection.activeGameObject = group;
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

		#endregion

		#region BringNear

        [MenuItem("Edit/ObjectMaster/Bring Near %&F", false, 11)]
        public static void BringNear()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Bring Near");

                foreach (Transform aT in Selection.transforms)
                {
                    aT.transform.transform.position = SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward * 5f;
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

		#endregion

		#region Buddy

		// distance to randomize within
        public static float stickDistance = 5f;

        public static void ChangeBuddy(float newDist)
        {
            stickDistance = newDist;
        }

        [MenuItem("Edit/ObjectMaster/Buddy", false, 12)]
        public static void Buddy()
        {
            if (Selection.transforms.Length > 0)
            {
                Undo.RecordObjects(Selection.transforms, "Buddy");

                // for each object
                foreach (Transform aT in Selection.transforms)
                {

                    if (aT.GetComponent<Collider>())
                    {

                        // grab all nearby objects within range
                        Collider[] nearby = Physics.OverlapSphere(aT.position, stickDistance);

                        // find the closest one
                        Transform closest = null;

                        closest = null;

                        foreach (Collider c in nearby)
                        {
                            if (c.transform != aT)
                            {
                                if (closest == null)
                                {
                                    closest = c.transform;
                                }
                                if (Vector3.Distance(c.transform.position, aT.position) < Vector3.Distance(closest.position, aT.position))
                                {
                                    closest = c.transform;
                                }
                            }
                        }

                        if (closest)
                        {
                            // raycast to find the point to stick to
                            RaycastHit awayHit;

                            aT.GetComponent<Collider>().enabled = false;

                            if (Physics.Raycast(aT.position, closest.position - aT.position, out awayHit))
                            {

                                aT.GetComponent<Collider>().enabled = true;

                                // second raycasthit to store the target object pointing back at this object
                                RaycastHit backHit;

                                closest.GetComponent<Collider>().enabled = false;

                                // do the raycast
                                if (Physics.Raycast(new Ray(awayHit.point, aT.position - awayHit.point), out backHit))
                                {

                                    closest.GetComponent<Collider>().enabled = true;

                                    // create an offset--the distance between aT's center and edge of collider
                                    Vector3 offset = backHit.point - aT.position;

                                    // set position to awayHit plus the offset
                                    aT.transform.position = awayHit.point - offset;
                                }
                                else
                                {
                                    LogWarning("Buddy: Return ray did not hit anything.");
                                }

                            }
                            else
                            {
                                LogWarning("Buddy: Couldn't draw a ray to other object.");
                            }

                            // just to be safe, turn both colliders back on
                            aT.GetComponent<Collider>().enabled = true;
                            closest.GetComponent<Collider>().enabled = true;
                        }
                        else
                        {
                            LogWarning("Buddy: found no nearby objects to stick to.");
                        }
                    }
                }
            }
            else
            {
                LogWarning("You must select one or more objects.");
            }
        }

		#endregion
		
		#region Ground
		
		[MenuItem("Edit/ObjectMaster/Ground", false, 13)]
		public static void Ground()
		{
			if (Selection.transforms.Length > 0)
			{
				Undo.RecordObjects(Selection.transforms, "Ground");
				
				foreach (Transform aT in Selection.transforms)
				{
					GroundTransform(aT);
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		public static void GroundTransform(Transform aT)
		{
			if (Selection.transforms.Length > 0)
			{
				Vector3 rayStart;
				RaycastHit hit;
				
				if (aT.GetComponent<Collider>())
				{
					rayStart = aT.GetComponent<Collider>().bounds.min;
				}
				else
				{
					rayStart = aT.position;
				}
				
				if (Physics.Raycast(rayStart, Vector3.down, out hit))
				{
					if (hit.distance > 0.01f)
					{
						aT.Translate(Vector3.down * (rayStart.y - hit.point.y), Space.World);
					}
				}
				else
				{
					LogWarning("No ground detected below for object(s) to Drop to.");
				}
			}
			else
			{
				LogWarning("You must select one or more objects.");
			}
		}
		
		#endregion

		#region Wrap

		[MenuItem ("Edit/ObjectMaster/Wrap Object", false, 14)]
		public static void WrapObject() {
			
			foreach (Transform selected in Selection.transforms) {
				
				Transform wrapThis = selected;
				
				GameObject group = new GameObject ();
				Undo.RegisterCreatedObjectUndo (group, "Wrap Object");
				
				group.transform.parent = wrapThis.parent;
				group.transform.SetSiblingIndex (wrapThis.GetSiblingIndex ());
				group.transform.localPosition = wrapThis.localPosition;
				group.transform.localRotation = wrapThis.localRotation;
				group.transform.localScale = wrapThis.localScale;
				group.name = wrapThis.name;
				
				Undo.SetTransformParent (wrapThis, group.transform, "Wrap Object");
				Undo.RecordObject(wrapThis.gameObject, "Wrap Object");
				wrapThis.transform.localPosition = Vector3.zero;
				wrapThis.transform.localRotation = Quaternion.Euler (0, 0, 0);
				wrapThis.transform.localScale = Vector3.one;
				
				Selection.activeTransform = group.transform;
			}
		}

		#endregion

    }

	#region FindReplaceNames

    public class FindReplaceNames : EditorWindow
    {
        [MenuItem("Edit/ObjectMaster/Find and Replace/Names")]
        public static void Init()
        {
            // Get existing open window or if none, make a new one:
            FindReplaceNames window = (FindReplaceNames)EditorWindow.GetWindow(typeof(FindReplaceNames));
#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
			window.title = "Replace Names";
#else
			window.titleContent = new GUIContent ("Replace Names");
#endif
            window.Show();
        }

        string find = "";
        string replace = "";
        bool selected = true;

        void OnGUI()
        {
            GUILayout.Label("Find and Replace GameObject Names", EditorStyles.boldLabel);
            find = EditorGUILayout.TextField("Search for: ", find);
            replace = EditorGUILayout.TextField("Replace with: ", replace);
            selected = EditorGUILayout.Toggle("Search within selection: ", selected);

			if (GUILayout.Button("Find and Replace Names"))
			{
				Utilities.FindReplaceNames(find, replace, selected);
			}

			GUILayout.Space (10);

			if (Selection.transforms.Length > 0 && find != "") {
				GUILayout.BeginHorizontal ();
				GUILayout.BeginVertical ();

				GUILayout.Label ("Before", EditorStyles.boldLabel);

				foreach (Transform t in Selection.transforms) {
					GUILayout.Label (t.gameObject.name);
				}
				GUILayout.EndVertical ();
				GUILayout.BeginVertical ();

				GUILayout.Label ("After", EditorStyles.boldLabel);
				foreach (Transform t in Selection.transforms) {
					GUILayout.Label (t.gameObject.name.Replace(find, replace));
				}
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
			}
        }
    }

	#endregion

	#region FindReplaceObjects 
	
	public class FindReplaceObjects : EditorWindow
	{
		
		public enum SearchType
		{
			SearchByName = 0,
			SearchByTag = 1
		}
		public SearchType searchType;
		
		[MenuItem("Edit/ObjectMaster/Find and Replace/GameObjects")]
		public static void Init()
		{
			// Get existing open window or if none, make a new one:
			FindReplaceObjects window = (FindReplaceObjects)EditorWindow.GetWindow(typeof(FindReplaceObjects));
			
			#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
			window.title = "Replace Objects";
			#else
			window.titleContent = new GUIContent ("Replace Objects");
			#endif
			window.Show();
		}
		
		string find = "";
		GameObject replace;
		bool selected = true;
		bool byTag = false;
		
		void OnGUI()
		{
			GUILayout.Label("Find and Replace with GameObjects", EditorStyles.boldLabel);
			GUILayout.Label("Note: Undo is not currently supported. Save your scene first!");
			find = EditorGUILayout.TextField("Search for: ", find);
			searchType = (SearchType)EditorGUILayout.EnumPopup("Search by: ", searchType);
			replace = EditorGUILayout.ObjectField("Replace with: ", replace, typeof(GameObject), true) as GameObject;
			selected = EditorGUILayout.Toggle("Search within selection: ", selected);
			
			if (GUILayout.Button("Find and Replace GameObjects"))
			{
				byTag = searchType == SearchType.SearchByTag;
				Utilities.FindReplaceObjects(find, replace, selected, byTag);
			}
			
			GUILayout.Space (10);
			
			if (Selection.transforms.Length > 0 && find != "") {
				GUILayout.BeginVertical ();
				
				string replaceText = "<select a prefab>";
				if (replace != null) {
					replaceText = replace.name;
				}
				
				GUILayout.Label ("Replace these with " + replaceText, EditorStyles.boldLabel);
				
				foreach (Transform t in Selection.transforms) {
					GUILayout.Label (t.gameObject.name);
				}
				GUILayout.EndVertical ();
			}
		}
	}
	
	#endregion

	#region RenameMaster 
	
	public class RenameMaster : EditorWindow
	{
		public enum NameOperation
		{
			NewName,
			AddPrefix,
			AddSuffix
		}
		public NameOperation nameOperation;
		
		[MenuItem("Edit/ObjectMaster/Rename Master")]
		public static void Init()
		{
			// Get existing open window or if none, make a new one:
			RenameMaster window = (RenameMaster)EditorWindow.GetWindow(typeof(RenameMaster));
			
			#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_5_0
			window.title = "Rename";
			#else
			window.titleContent = new GUIContent ("Rename");
			#endif
			window.Show();
		}
		string rename = "";
		int numberPadding = 4;
		bool numberItems = true;
		bool sortInHierarchy = true;
		int startNumber = 0;
		int incrementBy = 1;
		
		void OnGUI()
		{
			GUILayout.Label("Rename Multiple Files", EditorStyles.boldLabel);
			GUILayout.Label("Note: Undo is not currently supported. Save your scene first!");
			rename = EditorGUILayout.TextField("New Name: ", rename);

			nameOperation = (NameOperation)EditorGUILayout.EnumPopup("Operation: ", nameOperation);

			numberItems = EditorGUILayout.Toggle("Number Items: ", numberItems);

			if (numberItems) {
				numberPadding = Mathf.Clamp(EditorGUILayout.IntField("Number Padding: ", numberPadding), 0, 10000);
				startNumber = EditorGUILayout.IntField("Start Number: ", startNumber);
				incrementBy = Mathf.Clamp(EditorGUILayout.IntField("Increment by (step): ", incrementBy), 1, 100000);
				sortInHierarchy = EditorGUILayout.Toggle("Sort in Hierarchy: ", sortInHierarchy);
			}

			if (Selection.transforms.Length > 0) {
				if (GUILayout.Button("Rename " + Selection.transforms.Length + " Item(s)"))
				{
					Utilities.RenameNames(rename, nameOperation == NameOperation.AddSuffix, nameOperation == NameOperation.AddPrefix, numberItems, sortInHierarchy, numberPadding, startNumber, incrementBy);
				}
			}

			string sample = "";

			if (nameOperation == NameOperation.AddPrefix) {
				sample = rename + "[name]";
			}
			else if (nameOperation == NameOperation.AddSuffix) {
				sample = "[name]" + rename;
			}
			else if (nameOperation == NameOperation.NewName) {
				sample = rename;
			}

			if (numberItems) {
				string padFormat = "";
				for (int padNum = 0; padNum < numberPadding; padNum++) {
					padFormat += "0";
				}
				sample += startNumber.ToString(padFormat);
			}

			GUILayout.Space (10);

			GUILayout.Label("Preview: " + sample);
		}
	}
	
	#endregion
}
