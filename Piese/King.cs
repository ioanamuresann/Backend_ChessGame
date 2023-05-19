public class King : Piece
{
    public King(Colour colour) : base(colour)
    {
    }

    /**
     * Check if we have a valid move for king. We can do castling if
     * the end piece is a rook, the pieces have the same color, the pieces have not
     * moved, the king is not in check, no other pieces between the two, and
     * we cannot castle into check.
     * Castling can only be done when there's a valid path (HasPathRook) and the king isn't in check.
     * @return 0 if valid move, -1 if bad colour, -2 if invalid move
     */
    public override int CheckMove(ChessBoard board, Tile start, Tile end)
    {
        if (end.GetPiece() != null && end.GetPiece() is Rook && end.GetPiece().GetColour() == this.GetColour()
                && !this.HasMoved() && !end.GetPiece().HasMoved() && !board.IsInCheck(this.GetColour()))
        {
            if (Rook.HasPathRook(board, end, start) && CanDoCastling(board, start, end))
            {
                return 0;
            }
            else return 2; // not a valid castling
        }
        // Rest of the moves
        if (end.GetPiece() != null && end.GetPiece().GetColour() != this.GetColour()) return -1; // bad colour
        int dx = Math.Abs(end.GetX() - start.GetX());
        int dy = Math.Abs(end.GetY() - start.GetY());
        if (dx + dy == 1 || (dx == 1 && dy == 1)) return 0;
        return -2; // not a valid move for king
    }

    /**
     * Checks if we are in check after castling
     * @return true if we can do castling, false otherwise
     */
    public bool CanDoCastling(ChessBoard board, Tile start, Tile end)
    {
        bool ok = false;
        Piece startPiece = start.GetPiece();
        if (startPiece.GetColour() == Colour.WHITE)
        {
            board.BoardMatrix[0][4].SetPiece(null);
            if (end.GetY() == 0)
            {
                board.BoardMatrix[0][2].SetPiece(new King(Colour.WHITE));
                if (!board.IsInCheck(Colour.WHITE)) ok = true;
                board.BoardMatrix[0][2].SetPiece(null);
            }
            else
            {
                board.BoardMatrix[0][6].SetPiece(new King(Colour.WHITE));
                if (!board.IsInCheck(Colour.WHITE)) ok = true;
                board.BoardMatrix[0][6].SetPiece(null);
            }
            board.BoardMatrix[0][4].SetPiece(new King(Colour.WHITE));
        }
        else
        {
            board.BoardMatrix[7][4].SetPiece(null);
            if (end.GetY() == 0)
            {
                board.BoardMatrix[7][2].SetPiece(new King(Colour.BLACK));
                if (!board.IsInCheck(Colour.BLACK)) ok = true;
                board.BoardMatrix[7][2].SetPiece(null);
            }
            else
            {
                board.BoardMatrix[7][6].SetPiece(new King(Colour.BLACK));
                if (!board.IsInCheck(Colour.BLACK)) ok = true;
                                board.BoardMatrix[7][6].SetPiece(null);
            }
            board.BoardMatrix[7][4].SetPiece(new King(Colour.BLACK));
        }
        return ok;
    }
}

