using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotsBoard : MonoBehaviour {

	public int 			boardWidth = 5;
	public int 			boardHeight	= 5;
	public float 		dotDistance = 1f;
	public GameObject	dotSprite;

	public Vector2[,]	boardPositions;
	public List<Dot>	boardDots = new List<Dot>();

	public List<Dot>	boardDotsSelected = new List<Dot>();
	public Dot			previousSelection;
	public Dot			currentSelection;

	float startX;
	float startY;

	void Start () 
	{
		if(!dotSprite) {
			Debug.LogError("There's no \"dot\" sprite attached!");
			return;
		}
		InitializeBoard();
	}

	void Update () 
	{
		if(Input.GetMouseButton(0)) {

		}
		if(Input.GetMouseButtonDown(0)) {
			
		}
		if(Input.GetMouseButtonUp(0)) {
			ClearDots();
			previousSelection = null;
			currentSelection = null;
		}
	}

	public void SetCurrentDot(Dot current) {
		if(currentSelection != null) {
			previousSelection = currentSelection;
		}
		currentSelection = current;
	}

	public Dot GetDotByBoardPosition(float x, float y)
	{
		Dot returnDot = new Dot();
		foreach(Dot dot in boardDots) {
			Vector2 pos = new Vector2(x,y);
			if(dot.boardPosition == pos) {
				returnDot = dot;
				break;
			}
		}
		return returnDot;
	}
	
	public Vector3 BoardToWorldPosition(Vector2 pos) 
	{
		return BoardToWorldPosition(pos.x, pos.y);
	}
	
	public Vector3 BoardToWorldPosition(float x, float y)
	{
		return new Vector3(x * dotDistance - startX , y * dotDistance - startY, 0f);
	}

	void ClearDots() {
		Debug.Log ("ClearDots()");
		List<Dot> removeList = new List<Dot>();
		foreach(Dot dot in boardDots) {
			if(dot.isConnected) {
				removeList.Add(dot);

			}
		}

		foreach(Dot dot in removeList) {
			boardDots.Remove(dot);
			Destroy (dot.gameObject);
		}

		foreach(Dot dot in boardDots) {
			dot.isSelected = false;
			//dot.isConnected = false;
		}
	}
	
	void InitializeBoard()
	{
		int dotsNumber = boardWidth * boardHeight;
		boardPositions = new Vector2[boardWidth, boardHeight];
		
		startX = dotDistance * (boardWidth / 2); 
		startY = dotDistance * (boardHeight / 2);
		startX -= (boardWidth % 2) == 0 ? (dotDistance/2f) : 0f;
		startY -= (boardHeight % 2) == 0 ? (dotDistance/2f): 0f;
		
		for(int y = 0; y < boardHeight; y++) {
			for(int x = 0; x < boardWidth; x++) {
				Vector2 position = new Vector2(x * dotDistance - startX, y * dotDistance - startY);
				boardPositions[x,y] = position;
				
				Dot dot = ((GameObject)GameObject.Instantiate(dotSprite, new Vector3(position.x, position.y, 0f), Quaternion.identity)).GetComponent<Dot>();
				dot.transform.parent = transform;
				dot.boardPosition = new Vector2(x,y);
				dot.name = dot.name + x*y;
				boardDots.Add (dot);
			}
		}
		
		//Debug.Log(dotSprite.GetComponent<SpriteRenderer>().sprite.bounds);
	}

	void UpdateBoard()
	{
		// TODO:
		// update board after players pull of the button
		// remove conntected dots
		// move dots to specific direction to fill empty spaces
		// create new dots on empty positions
	}
}
