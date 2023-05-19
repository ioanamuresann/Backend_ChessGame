
public class Bishop : Piece
{
    public Bishop(Colour colour) : base(colour)
    {
    }

    public override int CheckMove(ChessBoard board, Tile start, Tile end)
    {
        if (end.GetPiece() != null && end.GetPiece().GetColour() == this.GetColour()) return -1; // bad colour
        int dx = Math.Abs(end.GetX() - start.GetX());
        int dy = Math.Abs(end.GetY() - start.GetY());
        if (dx == dy)
        {
            if (HasPathBishop(board, start, end)) return 0;
            else return -3; // piece in the way
        }
        else return -2; // not a valid move for bishop
    }

    /**
     * Checks if there is a piece in the way diagonally, the 4 diagonal cases of
     * values are given by dx and dy.
     * @param board the board
     * @param start the start tile
     * @param end the end tile
     * @return true if there is no piece in the way diagonally, false otherwise
     */
    public static bool HasPathBishop(ChessBoard board, Tile start, Tile end)
    {
        int dx = end.GetX() - start.GetX();
        int dy = end.GetY() - start.GetY();
        if (dx > 0 && dy > 0)
        {
            for (int i = start.GetX() + 1, j = start.GetY() + 1; i < end.GetX(); i++, j++)
            {
                if (board.GetTile(i, j).GetPiece() != null) return false;
            }
            return true;
        }

        if (dx > 0 && dy < 0)
        {
            for (int i = start.GetX() + 1, j = start.GetY() - 1; i < end.GetX(); i++, j--)
            {
                if (board.GetTile(i, j).GetPiece() != null) return false;
            }
            return true;
        }

        if (dx < 0 && dy > 0)
        {
            for (int i = start.GetX() - 1, j = start.GetY() + 1; j < end.GetY(); i--, j++)
            {
                if (board.GetTile(i, j).GetPiece() != null) return false;
            }
            return true;
        }

        if (dx < 0 && dy < 0)
        {
            for (int i = start.GetX() - 1, j = start.GetY() - 1; j > end.GetY(); i--, j--)
            {
                if (board.GetTile(i, j).GetPiece() != null) return false;
            }
            return true;
        }

        return false;
    }
}
