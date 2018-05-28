using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 15.0f;
	public float padding = 1f;
	public float health = 250f;
	public GameObject projectile;
	public float beamSpeed;
	public float firingRate = 0.2f;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	float xMin;
	float xMax;
	float yMin = -4.4f;
	float yMax = 6.4f;
	
	
	
	// Use this for initialization
	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerControler();
	}
	
	void PlayerControler()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000001f, firingRate);
			
			
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}
		
		else if(Input.GetKey (KeyCode.LeftArrow))
		{
			//transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.RightArrow))
		{	
			//transform.position += new Vector3(speed * Time.deltaTime,0,0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else if (Input.GetKey (KeyCode.UpArrow))
		{	
			//transform.position += new Vector3(0,speed * Time.deltaTime,0);
			transform.position += Vector3.up * speed * Time.deltaTime;
		} 
		else if (Input.GetKey (KeyCode.DownArrow))
		{	
			//transform.position += new Vector3(0, -speed * Time.deltaTime,0);
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		
		//restricts the player on the x the game space
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
		//restricts the player on the y the game space
		float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);
	}
	
	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0,1,0);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,beamSpeed,0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("Player has been hit");
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
	
	void Die()
	{
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy (gameObject);
	}
}
