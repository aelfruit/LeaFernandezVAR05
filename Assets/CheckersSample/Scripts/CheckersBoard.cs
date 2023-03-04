using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckersBoard : MonoBehaviour
{
    //Two players
    public GameObject lightPiecePrefab;
    public GameObject darkPiecePrefab;
    public Piece[,] pieces = new Piece[8, 8]; //2D array

    private Piece selectedPiece;

    public Transform PiecesOrigin;

    private Vector3 boardOffset = new Vector3(-4f, 0, -4);
    private Vector3 pieceOffset = new Vector3(.5f, 0, .5f);

    private Vector3 mouseCast;
    private Vector3 startMovePiece;
    private Vector3 endMovePiece;

    //private List<> 


    private void Start()
    {
        GenerateBoard();
    }
    private void GenerateBoard()
    {
        //light or left side pieces
        for (int z = 0; z < 3; z++)
        {
            bool skipRow = ( z % 2 == 0 );   //shift every 2nd row 1 tile over
            for (int x = 0; x < 8; x+= 2)   //8 tiles skip to 2nd to get 4 pieces
            {
                //GeneratePiece(x, z);
                GeneratePiece((skipRow) ? x : x + 1, z, lightPiecePrefab); //ternary is shortened "if else"
            }
        }

        //dark or right side pieces 
        for (int z = 7; z > 4; z--)         //same code change location & prefab
        {
            bool skipRow = (z % 2 == 0);     
            for (int x = 0; x < 8; x += 2)   
            {
                //GeneratePiece(x, z);
                GeneratePiece((skipRow) ? x : x + 1, z, darkPiecePrefab);
            }
        }
    }
    private void Update()
    {
        MouseLocate();
        Debug.Log(mouseCast);

        //player's turn:
        {
            int x = (int)mouseCast.x;
            int y = (int)mouseCast.y;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                SelectPiece(x, y);
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                MovePiece((int)startMovePiece.x, (int)startMovePiece.y,x,y);
            }
        }       
    }

    private void MouseLocate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
               
            
            if (Physics.Raycast(ray, out RaycastHit hit ,50f, LayerMask.GetMask("GameBoard")))
            {
            //Destroy(hit.transform.gameObject); will only affect if gameObject have collider
            //mouseCast.x = (int)hit.point.x - boardOffset.x;
            //mouseCast.y = (int)hit.point.z - boardOffset.z;
            mouseCast.x = 1+ Mathf.RoundToInt(hit.point.x) - boardOffset.x;
            mouseCast.y = 1+ Mathf.RoundToInt(hit.point.z) - boardOffset.z;                           
            //Debug.Log($"Hit! at {mouseCast.x}, {mouseCast.y}");
            }

            else if (Mouse.current.rightButton.wasPressedThisFrame)
            {
            
            }    
            
            /*
            else
            {
                mouseCast.x = -1;
                mouseCast.y = -1;
                //Debug.Log($"Now at {mouseCast}");
            }      
*/
    }

    private void SelectPiece(int x, int y)
    {
        //not within array
        if(x < 0 || x >= pieces.Length || y <0 || y >= pieces.Length)
            return;
        Piece piece = pieces[x,y];
        if (piece != null)
        {
            selectedPiece = piece;
            startMovePiece = mouseCast;
            Debug.Log(selectedPiece.name);
        }
    }

    private void MovePiece(int xStart, int yStart, int xEnd, int yEnd ) //starting and ending points
    {
        startMovePiece = new Vector2(xStart, yStart);
        endMovePiece = new Vector2(xEnd, yEnd);
        selectedPiece = pieces[xStart, yStart];
        
        ShiftPiece(selectedPiece, xEnd, yEnd);
    }

    private void GeneratePiece(int x, int z, GameObject piecePrefab)
    {        
        Instantiate(piecePrefab, PiecesOrigin); 
        //piecePrefab.transform.SetParent(transform);
        Piece p = piecePrefab.GetComponent<Piece>();
        pieces[x, z] = p;
        ShiftPiece(p,x,z);
    }
    private void ShiftPiece(Piece p, int x, int z)
    {
    p.transform.position = (Vector3.right * x) + (Vector3.forward * z) + boardOffset + pieceOffset;
    }
}


//rules
//player clicks on piece then click/place piece on valid destination
//two players.. take turns = 1 move diagonal
//jump if = occupied by enemy piece but blank after
//captured pieces removed
//piece reach end of board = crowned can move forward backward
//game over when one player loses all pieces or no valid move remaining





