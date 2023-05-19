public class Queen : Piece
{
    public Queen(Colour colour) : base(colour)
    {
    }

    /**
     * Checks if the Queen has a valid move. We check if the move is valid for a Rook or a Bishop.
     * @return whether the Queen has a valid move
     */
    public override int CheckMove(ChessBoard board, Tile start, Tile end)
    {
        if (end.GetPiece() != null && end.GetPiece().GetColour() == this.GetColour()) return -1; // bad colour
        if (end.GetX() == start.GetX() || start.GetY() == end.GetY())
        {
            if (Rook.HasPathRook(board, start, end))
            {
                return 0;
            }
            else return -3; // piece in the way
        }
        int dx = Math.Abs(end.GetX() - start.GetX());
        int dy = Math.Abs(end.GetY() - start.GetY());
        if (dx == dy)
        {
            if (Bishop.HasPathBishop(board, start, end)) return 0;
            else return -3; // piece in the way
        }
        return -2; // not a valid move for Queen
    }
}
