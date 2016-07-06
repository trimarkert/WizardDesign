using UnityEngine;
using System.Collections;

public class EnemyHealth : SimpleHealth {


	public override void Damage(int dmgAmount)
	{
		curHealth -= dmgAmount;
	}
	protected override void Die ()
	{
		Destroy(gameObject);
	}
}
