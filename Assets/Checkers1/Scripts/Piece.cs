using UnityEngine;

public class Piece : MonoBehaviour
{

    public bool isCrowned;  
    public bool isTurnActive;
      


    public void CrownFlip(Piece piece)
    {        
        //piece reaches end of board
        //flip and can move forward/backwards
        piece.transform.Rotate(0, 180, 0);
    }

    public void Captured()
    {
        Destroy(gameObject);
    }

    public bool isCoercedJump(Piece[,] board, int x, int y)
    {
        if (isTurnActive || isCrowned)
        {
            //Up Left or forward left on rotated board
            if (x >= 2 && y <= 5)
            {
                Piece piece = board[x - 1, y + 1];
                if (piece != null && piece.isTurnActive != isTurnActive) //jump over opponent's piece
                {
                    if (board[x - 2, y + 2] == null)         //can land on tile after opponent's piece
                        return true;
                }
            }

            //Up Right or forward right on rotated board
            if (x <= 5 && y <= 5)
            {
                Piece piece = board[x + 1, y + 1];      // +1
                if (piece != null && piece.isTurnActive != isTurnActive)
                {
                    if (board[x + 2, y + 2] == null)    // +2
                        return true;
                }
            }
        }

        else if (!isTurnActive || isCrowned)
        {
            //Down Left
            if (x >= 2 && y >= 2)
            {
                Piece piece = board[x - 1, y - 1];      // -1
                if (piece != null && piece.isTurnActive != isTurnActive)
                {
                    if (board[x - 2, y - 2] == null)    // -2
                        return true;
                }
            }

            //Down Right
            if (x <= 5 && y >= 2)
            {
                Piece piece = board[x + 1, y - 1];      // +1, -1
                if (piece != null && piece.isTurnActive != isTurnActive)
                {
                    if (board[x + 2, y - 2] == null)    // +2, -2
                        return true;
                }
            }
        }
        return false;
    }

    public bool ValidMove(Piece[,] board, int xStart, int yStart, int xEnd, int yEnd)
    {
        // moving on another piece
        if (board[xEnd, yEnd] != null)
            return false;

        int deltaMoveX = Mathf.Abs(xStart - xEnd);
        int deltaMoveY = yStart - yEnd;

        //light
        if (isTurnActive || isCrowned)
        {
            if (deltaMoveX == 1) //jump
            {
                if (deltaMoveY == 1)
                    return true;
            }

            else if (deltaMoveX == 2) //kill jump
            {
                if (deltaMoveY == 2)
                {
                    Piece piece = board[(xStart + xEnd) / 2, (yStart + yEnd) / 2];
                    if (piece != null && piece.isTurnActive != isTurnActive)
                        return true;
                }
            }
        }

        //dark
        if (!isTurnActive || isCrowned)
        {
            if (deltaMoveX == 1) 
            {
                if (deltaMoveY == -1)   //reverse direction
                    return true;
            }

            else if (deltaMoveX == 2) 
            {
                if (deltaMoveY == -2)   //reverse direction
                {
                    Piece piece = board[(xStart + xEnd) / 2, (yStart + yEnd) / 2];
                    if (piece != null && piece.isTurnActive != isTurnActive)
                        return true;
                }
            }
        }
        return false;
    }

}
