public class Pawn : Piece
{
    public Pawn(Colour colour) : base(colour)
    {
    }

    public override int CheckMove(ChessBoard board, Tile start, Tile end)
    {
        if (end.GetPiece() != null && end.GetPiece().GetColour() == this.GetColour()) return -1;

        int dx = end.GetX() - start.GetX();
        int dy = end.GetY() - start.GetY();

        if (this.GetColour() == Colour.WHITE)
        {
            if (dx == 2 && dy == 0 && !HasMoved() && start.GetX() == 1 && end.GetPiece() == null)
            {
                return 0; // valid move
            }
            else if (dx == 1 && dy == 0 && end.GetPiece() == null)
            {
                return 0; // valid move
            }
            else if (dx == 1 && Math.Abs(dy) == 1 && end.GetPiece() != null)
            {
                return 0; // valid move (capture)
            }
        }
        else if (this.GetColour() == Colour.BLACK)
        {
            if (dx == -2 && dy == 0 && !HasMoved() && start.GetX() == 6 && end.GetPiece() == null)
            {
                return 0; // valid move
            }
            else if (dx == -1 && dy == 0 && end.GetPiece() == null)
            {
                return 0; // valid move
            }
            else if (dx == -1 && Math.Abs(dy) == 1 && end.GetPiece() != null)
            {
                return 0; // valid move (capture)
            }
        }

        return -2; // no such move for pawn
    }

}
