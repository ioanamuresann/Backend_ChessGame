public class Knight : Piece
{
    public Knight(Colour colour) : base(colour)
    {
    }

    /**
     *
     * @return 0 if valid move, -1 if bad colour, -2 if invalid move
     */
    public override int CheckMove(ChessBoard board, Tile start, Tile end)
    {
        if (end.GetPiece() != null && end.GetPiece().GetColour() == this.GetColour()) return -1; // bad colour
        int dx = Math.Abs(end.GetX() - start.GetX());
        int dy = Math.Abs(end.GetY() - start.GetY());
        if (dx * dy == 2) return 0; // valid move
        return -2; // invalid move
    }
}
