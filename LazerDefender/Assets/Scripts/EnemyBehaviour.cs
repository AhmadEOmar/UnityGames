using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public GameObject projectile;
	public float health = 150;
	public float enemyBeamSpeed = 10f;
	public float enemyFiringRate = 0.5f;
	public int scoreValue = 150;
	public AudioClip enemyFire;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;

	void Start()
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		projectile missile = col.gameObject.GetComponent<projectile>();
		if (missile)
		{
			health -= missile.getDamage();
			missile.Hit ();
			if (health <= 0)
			{
				Die ();
			}
			
			
		}
	}
	
	void Update()
	{
		float probability = Time.deltaTime * enemyFiringRate;
		if (Random.value < probability)
		{
			EnemyFire();
		}
		
	}
	
	/*void EnemyShots()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("EnemyFire", 0.000001f, enemyFiringRate);
			
			
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("EnemyFire");
		}
	
	}*/
	
	void EnemyFire()
	{
		Vector3 startPosition = transform.position + new Vector3(0,-1,0);
		GameObject enemyBeam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		enemyBeam.rigidbody2D.velocity = new Vector3(0,-enemyBeamSpeed,0);
		AudioSource.PlayClipAtPoint(enemyFire, transform.position);
	}
	
	void Die()
	{
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}
	
}
