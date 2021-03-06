﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start ()
	{		
		instance = this;
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	public void Kill ()
	{
		GameManager.instance.GameOver ();
		if (PlayerPrefs.GetFloat ("highscore", 0) < this.count) {
			PlayerPrefs.SetFloat ("highscore", this.count);

		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!";
			Winner ();
		}
	}

	public void Winner ()
	{
		GameManager.instance.LevelComplete ();
	}

	void onTrigger (Collider other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			Debug.Log ("dead");
			other.gameObject.SetActive (false);
			Kill ();
		}
	}

	public void StartGame()
	{
		Time.timeScale = 1;
	}

}
