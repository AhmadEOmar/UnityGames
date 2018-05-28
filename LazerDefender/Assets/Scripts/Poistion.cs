using UnityEngine;
using System.Collections;

public class Poistion : MonoBehaviour {

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position,1);
	}
}
