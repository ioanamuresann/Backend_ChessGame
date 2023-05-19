using System.Collections.Generic;

public abstract class Piece
{
    private Colour colour;
    private bool hasMoved = false;

    public Piece(Colour colour)
    {
        this.colour = colour;
    }

    public abstract int CheckMove(ChessBoard board, Tile start, Tile end);

    public List<Tile> GetLegalMoves(ChessBoard board, Tile pieceTile)
    {
        Piece piece = pieceTile.GetPiece();
        if (piece == null)
            return null;

        List<Tile> validMoves = new List<Tile>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board.BoardMatrix[i, j] != pieceTile)
                {
                    if (piece.CheckMove(board, pieceTile, board.BoardMatrix[i, j]) == 0)
                        validMoves.Add(board.BoardMatrix[i, j]);
                }
            }
        }
        return validMoves;
    }

    public Colour GetColour()
    {
        return colour;
    }

    public void SetColour(Colour colour)
    {
        this.colour = colour;
    }

    public bool HasMoved()
    {
        return hasMoved;
    }

    public void SetHasMoved(bool hasMoved)
    {
        this.hasMoved = hasMoved;
    }
}
