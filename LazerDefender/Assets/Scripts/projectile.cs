using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

		public float damage = 100f;
		
		
		
		
		public float getDamage()
		{
			return damage;
		}
		
		
		public void Hit()
		{
			Destroy (gameObject);
		}
}
