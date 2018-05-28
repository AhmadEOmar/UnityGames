using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour {

	public AudioClip crack;	
	public Sprite [] hitSprites;
	public static int breakableCount = 0;
	public GameObject smoke;
	
	private bool isBreakable;
	private int timesHits;
	private LevelManager levelmanager;
	
	// Use this for initialization
	void Start () 
	{
		isBreakable = (this.tag == "Breakable");
		//Will keep track of breakable bricks
		
		if (isBreakable)
		{
			breakableCount++;
			
		}
		timesHits = 0;
		levelmanager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	void OnCollisionEnter2D (Collision2D col)
	{
		//Will play crack sound in the position of the brick
		AudioSource.PlayClipAtPoint (crack, transform.position);
		
		if (isBreakable)
		{
			handleHits();
		}
	}
	
	
	
	void handleHits ()
	{
		timesHits++;
		int maxHits = hitSprites.Length +1;
		if (timesHits >= maxHits)
		{
			breakableCount--;
			levelmanager.BrickDestroyed();
			PuffSmoke();
			Destroy(gameObject);
		}
		else 
		{
			LoadSprites();
		}
	}
	
	void PuffSmoke()
	{
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	
	
	void SimulateWin()
	{
		
		levelmanager.LoadNextLevel();
	}
	
	//dealing with sprite damage transition
	void LoadSprites()
	{
		int spriteIndex = timesHits - 1;
		if (hitSprites[spriteIndex] != null)
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else 
		{
			Debug.LogError("Brick Sprite missing");
		}
		
		
	}
	
	
	
}