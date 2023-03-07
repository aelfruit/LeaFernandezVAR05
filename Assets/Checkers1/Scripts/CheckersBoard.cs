using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;


public class CheckersBoard : MonoBehaviour
{
    //Two players
    public GameObject lightPiecePrefab;
    public GameObject darkPiecePrefab;

    private bool isLightTurn;
    private bool hasCaptured;

    //public toggle for testing & announcements
    public bool lockTurn;
    public TextMeshProUGUI whoseTurn;
    public TextMeshProUGUI gameText;

    //2D array and board to hold pieces n,n = 8,8
    //public Transform PiecesOrigin; //optional instead of SetParent
    const int n = 8;
    public Piece[,] pieces = new Piece[n, n];
    private Piece selectedPiece;

    //list to contain pieces
    private List<Piece> coercedPieces;

    //add offset to nudge prefab on center of tile
    private Vector3 pieceOffset = new Vector3(.5f, 0, .5f);

    //public float tileSize = f; //check why prefabpieces don't move

    //piece movement & raycasting
    private Vector2 mouseCast;          //location reference   
    private Vector2 startMovePiece;     //piece origin
    private Vector2 endMovePiece;       //piece destination


    private void Start()
    {
        GenerateBoard(); // option use UI button
        isLightTurn = true;

        coercedPieces = new List<Piece>();
    }

    //public void DarkTurn()
    //{
    //    lockTurn = true;
    //    whoseTurn.text = "Dark moves";
    //}

    //public void LightTurn()
    //{
    //    lockTurn = false;
    //    whoseTurn.text = "Lite moves";
    //}


    public void GenerateBoard()
    {
        //light pieces
        for (int z = 0; z < 3; z++)
        {
            bool skipRow = (z % 2 == 0);   //shift every 2nd row 1 tile over //modulus % is remainder
            for (int x = 0; x < 8; x += 2)    //8 tiles skip to 2nd to get 4 pieces
            {
                //GeneratePiece(x, y);
                GeneratePiece(skipRow ? x : x + 1, z, lightPiecePrefab); //ternary is shortened "if else"
            }
        }

        //dark pieces 
        for (int z = 7; z > 4; z--)         //same code change location & prefab
        {
            bool skipRow = (z % 2 == 0);
            for (int x = 0; x < 8; x += 2)
            {
                //GeneratePiece(x, y);
                GeneratePiece(skipRow ? x : x + 1, z, darkPiecePrefab);
            }
        }
    }
    private void Update()
    {
        MouseLocate();
        //Debug.Log(mouseCast);

        //player's turn:
        {
            int x = (int)mouseCast.x;
            int y = (int)mouseCast.y;

            if (selectedPiece != null)
                CarryPiece(selectedPiece);

            //if (Mouse.current.leftButton.isPressed) //pick the piece with left pick
            if (Mouse.current.leftButton.wasPressedThisFrame)
                SelectPiece(x, y);

            //if (Mouse.current.rightButton.isPressed) //drop the piece with right pick
            if (Mouse.current.leftButton.wasReleasedThisFrame)
                TryMovePiece((int)startMovePiece.x, (int)startMovePiece.y, x, y);
        }
    }

    private void MouseLocate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (Physics.Raycast(ray, out RaycastHit hit, 30.0f, LayerMask.GetMask("GameBoard")))
        {
            //mouseCast.x = Mathf.RoundToInt(hit.point.x); 
            //mouseCast.y = Mathf.RoundToInt(hit.point.y);

            //float not as concise on pinpointing within tile use (int)
            mouseCast.x = (int)hit.point.x;
            mouseCast.y = (int)hit.point.z;

        }
    }

    private void SelectPiece(int x, int y)
    {
        //not within array bounds of (n,n=8)
        if (x < 0 || x >= n || y < 0 || y >= n)
            return;

        Piece piece = pieces[x, y];
        // there is a piece and whose turn?
        if (piece != null && piece.isTurnActive == lockTurn)
        {
            if (coercedPieces.Count == 0)
            {
                selectedPiece = piece;
                startMovePiece = mouseCast;
                Debug.Log($"Picked {selectedPiece.name} at {mouseCast}");
            }
            else
            {
                //look for piece with possible move
                if (coercedPieces.Find(cp => cp == piece) == null)
                    return;

                selectedPiece = piece;
                startMovePiece = mouseCast;
            }
        }
    }

    private void CarryPiece(Piece piece)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (Physics.Raycast(ray, out RaycastHit hit, 50.0f, LayerMask.GetMask("GameBoard")))
        {
            piece.transform.position = hit.point + Vector3.up;
        }
    }

    private void TryMovePiece(int xStart, int yStart, int xEnd, int yEnd) //starting and ending positions
    {
        coercedPieces = CheckValidMoves();

        //reset for each player
        startMovePiece = new Vector2(xStart, yStart);
        endMovePiece = new Vector2(xEnd, yEnd);
        selectedPiece = pieces[xStart, yStart]; //define from array which piece is selected 


        //extents (n,n = 8)
        if (xEnd < 0 || xEnd >= n || yEnd < 0 || yEnd >= n)
        {
            if (selectedPiece != null)
                ShiftPiece(selectedPiece, (int)startMovePiece.x, (int)startMovePiece.y);
            startMovePiece = Vector2.zero;
            selectedPiece = null;
            return;
        }

        //did not go anywhere put it back
        if (selectedPiece != null)
        {
            if (endMovePiece == startMovePiece)
            {
                ShiftPiece(selectedPiece, xStart, yStart);
                startMovePiece = Vector2.zero;
                selectedPiece = null;
                return;
            }
        }

        //check valid move
        if (selectedPiece.ValidMove(pieces, xStart, yStart, xEnd, yEnd))
        {
            //jump over and destroy opponent piece

            if (Mathf.Abs(xEnd - xStart) == 2) //value becomes absolute integer
            {
                Piece piece = pieces[(xStart + xEnd) / 2, (yStart + yEnd) / 2];
                if (piece != null)
                {
                    pieces[(xStart + xEnd) / 2, (yStart + yEnd) / 2] = null;
                    Debug.Log($"{piece.name} captured!");
                    Destroy(piece.gameObject);
                    hasCaptured = true;
                }
            }

            if (coercedPieces.Count != 0 && !hasCaptured)
            {
                ShiftPiece(selectedPiece, xStart, yStart);
                startMovePiece = Vector2.zero;
                selectedPiece = null;
                return;
            }

            pieces[xEnd, yEnd] = selectedPiece;
            pieces[xStart, yStart] = null;
            ShiftPiece(selectedPiece, xEnd, yEnd);

            EndTurn();
        }

        else
        {
            ShiftPiece(selectedPiece, xStart, yStart);
            startMovePiece = Vector2.zero;
            selectedPiece = null;
            return;
        }

    }

    private void EndTurn()
    {
        int x = (int)endMovePiece.x;
        int y = (int)endMovePiece.y;

        //Coronation
        if (selectedPiece != null)
        {
            //lockTurn reach opposite end
            if (selectedPiece.isTurnActive && !selectedPiece.isCrowned && y == 7)
            {
                selectedPiece.isCrowned = true;
                selectedPiece.transform.Rotate(Vector3.right * 180);
            }
        }
        else if (selectedPiece != null)
        {
            //darkPiece
            if (!selectedPiece.isTurnActive && !selectedPiece.isCrowned && y == 0)
            {
                selectedPiece.isCrowned = true;
                selectedPiece.transform.Rotate(Vector3.right * 180);
            }
        }

        selectedPiece = null;
        startMovePiece = Vector2.zero;

        if (CheckValidMoves(selectedPiece, x, y).Count != 0 && hasCaptured)
            return;

        isLightTurn = !isLightTurn;
        lockTurn = !lockTurn;
        hasCaptured = false;
        CheckWin();
    }

    private void CheckWin()
    {
        var pcs = FindObjectsOfType<Piece>();
        bool hasLightPiece = false, hasDarkPiece = false;
        for (int i = 0; i < pcs.Length; i++)
        {
            if (pcs[i].isTurnActive)
                hasLightPiece = true;
            else
                hasDarkPiece = true;
        }
        if (!hasLightPiece)
            Winner(false);
        if (!hasDarkPiece)
            Winner(true);
    }

    private void Winner(bool isTurnActive)
    {
        if (isTurnActive)
            gameText.text = ("And there was light! Hurrah Day Team!");
        else
            gameText.text = ("Now darkness will engulf us all.. Night Team wins!");
    }

    private List<Piece> CheckValidMoves(Piece piece, int x, int y)
    {
        coercedPieces = new List<Piece>();

        if (pieces[x, y].isCoercedJump(pieces, x, y))
            coercedPieces.Add(pieces[x, y]);

        return coercedPieces;
    }

    private List<Piece> CheckValidMoves()
    {
        coercedPieces = new List<Piece>();
        //check all the pieces [8,8]
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (pieces[i, j] != null && pieces[i, j].isTurnActive == lockTurn)
                    if (pieces[i, j].isCoercedJump(pieces, i, j))
                        coercedPieces.Add(pieces[i, j]);
        return coercedPieces;
    }
    private void GeneratePiece(int x, int y, GameObject piecePrefab)
    {
        //Instantiate(piecePrefab, PiecesOrigin); if this is used, we get component movement errors

        GameObject gamePiece = Instantiate(piecePrefab) as GameObject;
        gamePiece.transform.SetParent(transform);
        Piece piece = gamePiece.GetComponent<Piece>();
        pieces[x, y] = piece;
        ShiftPiece(piece, x, y);

    }
    private void ShiftPiece(Piece piece, int x, int y)
    {
        piece.transform.position = Vector3.right * x + Vector3.forward * y + pieceOffset;
    }
}


//rules
//player clicks on piece then click/place piece on valid destination
//two players.. take turns = 1 move diagonal
//jump if = occupied by enemy piece but blank after
//captured pieces removed
//piece reach end of board = crowned can move forward backward
//game over when one player loses all pieces or no valid move remaining

