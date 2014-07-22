using UnityEngine;
using System.Collections;




public class Dot : MonoBehaviour 
{

	/*public class EDotType 
	{
		public static readonly Color DT_Yellow = Color.yellow;
		public static readonly Color DT_Green = Color.green;
		public static readonly Color DT_Red = Color.red;
		public static readonly Color DT_Blue = Color.blue;

		public static int dotTypes = 4;

	}*/

	public static readonly Color[] dotColors = { Color.green, Color.red, Color.blue, Color.yellow };

	DotsBoard			dotsBoard;
	public bool			isSelected;
	public bool 		isConnected;
	public int 			type;
	public Vector2		boardPosition;

	//public Color		startColor = Color.white;
	//public Color		targetColor;

	Vector3 beginLine;

	SpriteRenderer		srenderer;
	LineRenderer		lrenderer;

	// Use this for initialization
	void Start () {
		dotsBoard = GameObject.FindObjectOfType<DotsBoard>();
		srenderer = GetComponent<SpriteRenderer>();
		lrenderer = GetComponent<LineRenderer>();

		type = Random.Range(0, dotColors.Length);
		//targetColor = Color.white; //
		srenderer.color = dotColors[type];
		particleSystem.startColor = dotColors[type];

		beginLine = new Vector3( transform.position.x,  transform.position.y, transform.position.z + 0.05f);
		lrenderer.material = new Material(Shader.Find("Particles/Additive"));
		lrenderer.SetWidth(0.2f, 0.2f);
		lrenderer.SetColors(dotColors[type], dotColors[type]);

		isSelected = false;
		isConnected = false;
	}

	void DrawLine(Vector3 endLine)
	{
		if(!lrenderer) 
			return;


		lrenderer.SetPosition(0, beginLine);
		lrenderer.SetPosition(1, endLine);
	}

	void CheckNeighbors() {
		Vector2 pos = Vector2.zero;
		// right
		if(boardPosition.x+1 < dotsBoard.boardWidth) {	
			Dot dot = dotsBoard.GetDotByBoardPosition(boardPosition.x+1, boardPosition.y);
			if(dot.isSelected && dot.type == type) {
				isConnected = true;
				dot.isConnected = true;
				DrawLine(dot.transform.position);
			}
		}
		// left
		if(boardPosition.x-1 >= 0) { 
			Dot dot = dotsBoard.GetDotByBoardPosition(boardPosition.x-1, boardPosition.y);
			if(dot.isSelected && dot.type == type) {
				isConnected = true;
				dot.isConnected = true;
				DrawLine(dot.transform.position);
			}
		}
		// bottom
		if(boardPosition.y+1 < dotsBoard.boardHeight) {
			Dot dot = dotsBoard.GetDotByBoardPosition(boardPosition.x, boardPosition.y+1);
			if(dot.isSelected && dot.type == type) {
				isConnected = true;
				dot.isConnected = true;
				DrawLine(dot.transform.position);
			}
		}
		// top
		if(boardPosition.y-1 >= 0) {
			Dot dot = dotsBoard.GetDotByBoardPosition(boardPosition.x, boardPosition.y-1);
			if(dot.isSelected && dot.type == type) {
				isConnected = true;
				dot.isConnected = true;
				DrawLine(dot.transform.position);
			}
		}

	}

	// Update is called once per frame
	void Update () {

		if(isSelected) {

		} else {

		}

		if(isConnected) {
			//lrenderer.enabled = false;

		} else {
			//lrenderer.enabled = true;
			DrawLine(beginLine);
		}

		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(Input.GetMouseButton(0))
		{
			if(collider2D.OverlapPoint(mousePos))
			{
				if(!isSelected) {
					isSelected = true;
					particleSystem.Play();
					CheckNeighbors();
					dotsBoard.SetCurrentDot(this);
				}

			}
		}
	}

}
