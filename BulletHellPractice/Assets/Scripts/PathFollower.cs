using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollower : MonoBehaviour {
	//
	public GameObject[] wayPntObjects;
	public List<Transform> wayPntsList;
	public Transform[] wayPnts;
	public float speed;
	public float reachDist = 1.0f;
	public int currentPoint = 0;
	public float extraDeathTime = 1.0f;
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
			GetComponent<SimpleHealth>().Invoke("Die", extraDeathTime);
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
