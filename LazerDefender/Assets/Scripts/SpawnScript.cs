using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float enemySpeed  = 5f;
	public float spawnDelay = 0.5f;
	
	private bool movementRight = true;
	private float xMax;
	private float xMin;
	
	// Use this for initialization
	void Start () 
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xMax = rightBoundary.x;
		xMin = leftBoundary.x;
		
		SpawnUntilFull();
		
	}
	
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3 (width, height));
	}
	
	
	void Spawn()
	{
		
		foreach (Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	
	
	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if(freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition())
		{
			Invoke("SpawnUntilFull" , spawnDelay);
		}
	}
	
	
	
	void Update()
	{
		if (movementRight)
		{
			transform.position += Vector3.right * enemySpeed * Time.deltaTime;
			
		}
		else 
		{
			transform.position += Vector3.left * enemySpeed * Time.deltaTime;
		}	
		
		Edge();
		
		
	}
	
	void Edge()
	{
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		if(rightEdgeOfFormation > xMax)
		{
			movementRight = false;
			
			
		}
		else if(leftEdgeOfFormation < xMin )
		{
			movementRight = true;
		}
		
		if(AllMembersDead())
		{
			Debug.Log ("Empty Formation");
			Spawn();
		}
		
		
	}
	
	Transform NextFreePosition()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if(childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead()
	{	
		
		foreach (Transform childPositionGameObject in transform)
		{
			if(childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
		
		
	}
}