public class Rook : Piece
{
    public Rook(Colour colour) : base(colour)
    {
    }

    public override int CheckMove(ChessBoard board, Tile start, Tile end)
    {
        if (end.GetPiece() != null && end.GetPiece().GetColour() == this.GetColour()) return -1; // bad colour

        if (end.GetX() == start.GetX() || start.GetY() == end.GetY())
        {
            if (HasPathRook(board, start, end))
            {
                return 0; // valid move
            }
            else
            {
                return -3; // piece in the way
            }
        }
        else
        {
            return -2; // invalid move
        }
    }

    public static bool HasPathRook(ChessBoard board, Tile start, Tile end)
    {
        if (start.GetY() == end.GetY())
        {
            if (end.GetX() > start.GetX())
            {
                for (int i = start.GetX() + 1; i < end.GetX(); i++)
                {
                    if (board.GetTile(i, end.GetY()).GetPiece() != null) return false;
                }
            }
            else // end.GetX < start.GetX
            {
                for (int i = end.GetX() + 1; i < start.GetX(); i++)
                {
                    if (board.GetTile(i, end.GetY()).GetPiece() != null) return false;
                }
            }
            return true;
        }

        if (start.GetX() == end.GetX())
        {
            if (end.GetY() > start.GetY())
            {
                for (int i = start.GetY() + 1; i < end.GetY(); i++)
                {
                    if (board.GetTile(end.GetX(), i).GetPiece() != null) return false;
                }
            }
            else // end.GetY < start.GetY
            {
                for (int i = end.GetY() + 1; i < start.GetY(); i++)
                {
                    if (board.GetTile(end.GetX(), i).GetPiece() != null) return false;
                }
            }
            return true;
        }

        return false;
    }

}
