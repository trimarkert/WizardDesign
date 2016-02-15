using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {
	public GameObject playerParent;
	public GameObject guiParent;
	 
	//This is intended to set up the player so that controllers don't control
	// both players
	void Start () 
	{
		if(isLocalPlayer)
		{
			playerParent.GetComponent<PlayerController>().enabled = true;
			guiParent.SetActive(true);

//			//Just turn important scripts on that make wizard work
//			GetComponent<PlayerController>().enabled = true;
//			GetComponent<SimpleHealth>().enabled = true;
//			//Not sure how many weapon icons there will be so made it a list
//			WeaponIcon[] weaponIcons;
//			weaponIcons = GetComponents<WeaponIcon>();
//			foreach(WeaponIcon wpnIcon in weaponIcons)
//			{
//				wpnIcon.enabled = true;
//			}

		}
	}
}
