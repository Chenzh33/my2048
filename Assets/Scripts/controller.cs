using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		isGameOver = false;

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("up")) {
			print ("key up pressed!\n");
			for (int i = board._boardSize - 2; i >= 0; --i) {
				for (int j = 0; j != board._boardSize; ++j) {
					block b = _boardPrefab.GetBlock (j, i);
					if (b != null)
						_boardPrefab.MoveOneBlockOneStep (b, Direction.Up);

				}
			}
			isGameOver = !_boardPrefab.SpawnBlockRand();

		}
		if (Input.GetKeyDown ("down")) {
			print ("key down pressed!\n");
			for (int i = 1; i <= board._boardSize - 1; ++i) {
				for (int j = 0; j != board._boardSize; ++j) {
					block b = _boardPrefab.GetBlock (j, i);
					if (b != null)
						_boardPrefab.MoveOneBlockOneStep (b, Direction.Down);

				}
			}
			isGameOver = !_boardPrefab.SpawnBlockRand();

		}
		if (Input.GetKeyDown ("left")) {
			print ("key left pressed!\n");
			for (int i = 1; i <= board._boardSize - 1; ++i) {
				for (int j = 0; j != board._boardSize; ++j) {
					block b = _boardPrefab.GetBlock (i, j);
					if (b != null)
						_boardPrefab.MoveOneBlockOneStep (b, Direction.Left);

				}
			}
			isGameOver = !_boardPrefab.SpawnBlockRand();

		}
		if (Input.GetKeyDown ("right")) {
			print ("key right pressed!\n");
			for (int i = board._boardSize - 2; i >= 0; --i) {
				for (int j = 0; j != board._boardSize; ++j) {
					block b = _boardPrefab.GetBlock (i, j);
					if (b != null)
						_boardPrefab.MoveOneBlockOneStep (b, Direction.Right);

				}
			}
			isGameOver = !_boardPrefab.SpawnBlockRand();

		}
		CheckStatus();
		
	}

	private void CheckStatus() {
		if(isGameOver)
		{
			Debug.Log("Game Over");
			_boardPrefab.Destruct();

		}

	}

	private bool isGameOver;
	public board _boardPrefab;
}