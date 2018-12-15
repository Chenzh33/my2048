using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour {

	// Use this for initialization
	void Start () {

		InitBoard ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void Destruct () {
		foreach (block b in _blocks) {
			b.Destruct();
		}
		Destroy (gameObject);
	}
	
	private void InitBoard () {
		for (int i = 0; i != 6; ++i)
			SpawnBlockRand ();

	}

	public bool CheckWithin (Vector3 p) {

		if (p.x < 0 || p.x >= _boardSize)
			return false;
		if (p.y < 0 || p.y >= _boardSize)
			return false;

		return true;

	}

	public void MoveOneBlockOneStep (block b, Direction dir) {
		Vector3 delta = GetMoveDelta (dir);
		Vector3 target = b.transform.position + delta;
		if (!CheckWithin (target))
			return;
		block tgt = GetBlock (target.x, target.y);
		if (tgt == null) {
			b.MoveOnePixel (dir);
			MoveOneBlockOneStep (b, dir);

		} else {
			if (tgt.GetSpriteNum () == b.GetSpriteNum ()) {
				tgt.LevelUp ();
				RemoveBlock (b);
			}
			return;

		}

	}
	public bool SpawnBlockRand () {
		if (_blocks.Count >= (_boardSize * _boardSize - 1))
			return false;
		int x, y;
		while (true) {
			x = Random.Range (0, _boardSize);
			y = Random.Range (0, _boardSize);
			if (GetBlock (x, y) == null)
				break;
		}
		block b = Instantiate (_blockPrefab, new Vector2 (x, y), Quaternion.identity) as block;

		b.InitSpriteNum ();
		_blocks.AddLast (b);
		return true;
	}

	public void RemoveBlock (block b) {
		_blocks.Remove (b);
		b.Destruct ();
		Debug.Log (_blocks.Count);
	}

	public block GetBlock (double x, double y) {
		foreach (block b in _blocks) {
			if (b.transform.position.x == x && b.transform.position.y == y) {
				return b;
			}
		}
		return null;
	}

	public static Vector3 GetMoveDelta (Direction dir) {
		Vector3 delta = new Vector3 ();
		switch (dir) {
			case Direction.Up:
				delta = new Vector2 (0, 1);
				break;
			case Direction.Down:
				delta = new Vector2 (0, -1);
				break;
			case Direction.Left:
				delta = new Vector2 (-1, 0);
				break;
			case Direction.Right:
				delta = new Vector2 (1, 0);
				break;
		}
		return delta;

	}

	private LinkedList<block> _blocks = new LinkedList<block> ();

	public const int _boardSize = 4;
	public block _blockPrefab;
}