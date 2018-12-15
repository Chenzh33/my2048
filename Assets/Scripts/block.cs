using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction : uint {
	Up,
	Down,
	Left,
	Right
};

public class block : MonoBehaviour {

	void Start () {
	isSpawning = true;
	isLevelUping = false;
	SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
	sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 0.0f);
	currSprite = 0;
	}

	void Update () {

		if (isSpawning) {
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, sr.color.a + 1.0f / spawningTime * Time.deltaTime);
			if (sr.color.a >= 1.0f) {
				sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, 1.0f);
				isSpawning = false;
			}
		}
		if (isLevelUping) {
			float deltaSize = transform.localScale.x * levelUpMaxSizeDelta / levelUpTime * Time.deltaTime;
			transform.localScale = new Vector3 (transform.localScale.x - deltaSize, transform.localScale.y - deltaSize, transform.localScale.z);
			if (transform.localScale.x <= 2.0f) {
				transform.localScale = new Vector3 (2.0f, 2.0f, 1.0f);
				isLevelUping = false;

			}

		}
	}

	public void Destruct () {
		Destroy (gameObject);
	}
	public int GetSpriteNum () {
		return currSprite;
	}

	public void InitSpriteNum () {
		SpriteRenderer spr = GetComponent<SpriteRenderer> ();
		currSprite = 0;
		spr.sprite = spriteList[currSprite];

	}

	public void MoveOnePixel (Direction dir) {
		Vector3 delta = board.GetMoveDelta (dir);
		transform.position = transform.position + delta;

	}
	public void LevelUp () {
		SpriteRenderer spr = GetComponent<SpriteRenderer> ();
		++currSprite;
		spr.sprite = spriteList[currSprite];
		float factor = 1.0f + levelUpMaxSizeDelta;
		transform.localScale = new Vector3 (transform.localScale.x * factor, transform.localScale.y * factor, transform.localScale.z);
		isLevelUping = true;
	}
	public Sprite[] spriteList;

	private int currSprite;
	private bool isSpawning;
	private bool isLevelUping;
	private const float spawningTime = 0.4f;
	private const float levelUpTime = 0.2f;
	private const float levelUpMaxSizeDelta = 0.5f;

}