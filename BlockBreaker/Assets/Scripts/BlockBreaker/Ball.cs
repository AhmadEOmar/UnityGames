using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private PaddleMovement paddle;
	private bool hasStarted = false;
	private Vector3 paddleToBallVector;
	
	
	
	
	// Use this for initialization
	void Start () 
	{
		
		paddle = GameObject.FindObjectOfType<PaddleMovement>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!hasStarted) {
			// Lock the ball relative to the paddle.
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			// Wait for a mouse press to launch.
			if (Input.GetMouseButtonDown(0)) {
				print ("Mouse clicked, launch ball");
				hasStarted = true;
				this.rigidbody2D.velocity = new Vector2 (2f, 10f);
			}
		}
		
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		//Vector2 stores 2 numbers. Takes X,Y coroidantes. To do this use Random.Range between 0. 0.2
		Vector2 tweak = new Vector2 (Random.Range(0f, 0.2f) , Random.Range (0f, 0.2f));
		if (hasStarted)
		{
			audio.Play();
			rigidbody2D.velocity += tweak;
		}
	}
}
