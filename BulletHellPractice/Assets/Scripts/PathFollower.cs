﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour {
	//Waypoints to be used for pathing
	public GameObject[] wayPntObjects;
	private List<Transform> wayPntsList;
	private Transform[] wayPnts;

	//Speed that the character will move
	public float speed;
	//Buffer zone for how close the character has to be to the waypoint to be considered "there"
	public float reachDist = 1.0f;
	//Current target waypoint for the character
	private int currentPoint = 0;

	// Use this for initialization
	void Start () {
		foreach(GameObject curObj in wayPntObjects)
		{
			Transform tempTrans = curObj.GetComponent<Transform>();
			wayPntsList.Add(tempTrans);
		}
		wayPnts = wayPntsList.ToArray();
	
	}
	
	// Update is called once per frame
	void Update () {
		if(currentPoint < wayPnts.Length)
		{
			//
			float dist = Vector3.Distance(wayPnts[currentPoint].position, transform.position);
			transform.position = Vector3.MoveTowards(transform.position, wayPnts[currentPoint].position,speed * Time.deltaTime);
			transform.LookAt(wayPnts[currentPoint].position);

			//Logic to change waypoints
			if(dist <= reachDist)
			{	
				currentPoint++;
			}
		}
		else
		{
			currentPoint = 0;
		}
	}

	//Used to visualize the Waypoints while editing
	void OnDrawGizmos()
	{

		if(wayPntObjects.Length > 0)
		{
			for(int a = 0; a < wayPntObjects.Length; a++)
			{
				if(wayPntObjects[a] !=null)
				{
					Gizmos.DrawSphere(wayPntObjects[a].transform.position, reachDist);
				}
			}
		}
	}
}
