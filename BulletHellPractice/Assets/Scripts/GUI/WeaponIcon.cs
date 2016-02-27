using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour {
	public Color disableOverlayClr;
	public Image myImg;
	
	public void DisableWeapon()
	{
		myImg.color = disableOverlayClr;
	}

	public void EnableWeapon()
	{
		myImg.color = Color.white;
	}
}
