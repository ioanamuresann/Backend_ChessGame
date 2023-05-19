public class Game
{
    private Colour turn = Colour.WHITE;
    private ChessBoard board;
    private int blackScore = 0;
    private int whiteScore = 0;

    public Game()
    {
        board = new ChessBoard(TEST_SETUP);
    }

    public void Move(int x1, int y1, int x2, int y2)
    {
        if (!IsInRange(x1, y1, x2, y2))
            throw new IllegalMoveException("Out of bounds!");

        Tile start = board.BoardMatrix[x1, y1];
        Tile end = board.BoardMatrix[x2, y2];

        if (start.GetPiece() == null)
            throw new IllegalMoveException("Cannot move an empty tile!");

        if (start == end)
            throw new IllegalMoveException("Can't move onto the same tile!");

        Piece startPiece = start.GetPiece();
        Piece endPiece = end.GetPiece();

        if (!ColorMatchTurn(startPiece))
            throw new IllegalMoveException("Choose correct colour piece for your turn! Current turn: " + turn);

        if (startPiece.CheckMove(board, start, end) != 0)
            throw new IllegalMoveException("Not a valid move!");

        if (board.CausesCheck(start, end, turn))
            throw new IllegalMoveException("Move will cause check!");

        if (IsCastlingMove(startPiece, endPiece))
            DoCastling(start, end);
        else
        {
            MovePiece(start, end);
            end.GetPiece().SetHasMoved(true);
        }

        ChangeTurn();
    }

    public static Piece MovePiece(Tile start, Tile end)
    {
        Piece aux = end.GetPiece();
        end.SetPiece(start.GetPiece());
        start.SetPiece(null);
        return aux;
    }

    public static void SwapPieces(Tile start, Tile end)
    {
        Piece aux = end.GetPiece();
        end.SetPiece(start.GetPiece());
        start.SetPiece(aux);
    }

    public void Reset()
    {
        turn = Colour.WHITE;
        board = new ChessBoard(TEST_SETUP);
    }

    public void DoCastling(Tile start, Tile end)
    {
        Piece startPiece = start.GetPiece();

        if (startPiece.GetColour() == Colour.WHITE)
        {
            if (end.Y == 0)
            {
                board.BoardMatrix[0, 2].SetPiece(new King(Colour.WHITE));
                board.BoardMatrix[0, 2].GetPiece().SetHasMoved(true);

                board.BoardMatrix[0, 3].SetPiece(new Rook(Colour.WHITE));
                board.BoardMatrix[0, 3].GetPiece().SetHasMoved(true);
            }
            else
            {
                board.BoardMatrix[0, 6].SetPiece(new King(Colour.WHITE));
                board.BoardMatrix[0, 6].GetPiece().SetHasMoved(true);

                board.BoardMatrix[0, 5].SetPiece(new Rook(Colour.WHITE));
                board.BoardMatrix[0, 5].GetPiece().SetHasMoved(true);
            }
        }
        else
        {
            if (end.Y == 0)
            {
                board.BoardMatrix[7, 2].SetPiece(new King(Colour.BLACK));
                board.BoardMatrix[7, 2].GetPiece().SetHasMoved(true);

                board.BoardMatrix[7, 3].SetPiece(new Rook(Colour.BLACK));
                board.BoardMatrix[7, 3].GetPiece().SetHasMoved(true);
            }
            else
            {
                board.BoardMatrix[7, 6].SetPiece(new King(Colour.BLACK));
                board.BoardMatrix[7, 6].GetPiece().SetHasMoved(true);
                 board.BoardMatrix[7, 5].SetPiece(new Rook(Colour.BLACK));
            board.BoardMatrix[7, 5].GetPiece().SetHasMoved(true);
        }
    }
    start.SetPiece(null);
    end.SetPiece(null);
}

public bool NeedsPromote()
{
    for (int i = 0; i < 8; i++)
    {
        Piece candidatePiece = board.BoardMatrix[0, i].GetPiece();

        if (candidatePiece is Pawn)
        {
            return true;
        }

        candidatePiece = board.BoardMatrix[7, i].GetPiece();

        if (candidatePiece is Pawn)
        {
            return true;
        }
    }

    return false;
}

public void PromotePawns(string piece)
{
    for (int i = 0; i < 8; i++)
    {
        Piece candidatePiece = board.BoardMatrix[0, i].GetPiece();

        if (candidatePiece is Pawn)
        {
            if (piece.Equals("queen", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[0, i].SetPiece(new Queen(Colour.BLACK));
            }
            else if (piece.Equals("rook", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[0, i].SetPiece(new Rook(Colour.BLACK));
            }
            else if (piece.Equals("bishop", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[0, i].SetPiece(new Bishop(Colour.BLACK));
            }
            else if (piece.Equals("knight", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[0, i].SetPiece(new Knight(Colour.BLACK));
            }

            board.BoardMatrix[0, i].GetPiece().SetHasMoved(true);
            break;
        }

        candidatePiece = board.BoardMatrix[7, i].GetPiece();

        if (candidatePiece is Pawn)
        {
            if (piece.Equals("queen", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[7, i].SetPiece(new Queen(Colour.WHITE));
            }
            else if (piece.Equals("rook", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[7, i].SetPiece(new Rook(Colour.WHITE));
            }
            else if (piece.Equals("bishop", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[7, i].SetPiece(new Bishop(Colour.WHITE));
            }
            else if (piece.Equals("knight", StringComparison.OrdinalIgnoreCase))
            {
                board.BoardMatrix[7, i].SetPiece(new Knight(Colour.WHITE));
            }

            board.BoardMatrix[7, i].GetPiece().SetHasMoved(true);
            break;
        }
    }
}

public bool IsCastlingMove(Piece startPiece, Piece endPiece)
{
    return startPiece is King && endPiece is Rook && startPiece.GetColour() == endPiece.GetColour();
}

public bool ColorMatchTurn(Piece startPiece)
{
    return (startPiece.GetColour() == Colour.WHITE && turn == Colour.WHITE)
        || (startPiece.GetColour() == Colour.BLACK && turn == Colour.BLACK);
}

public void ChangeTurn()
{
    if (turn == Colour.WHITE)
        turn = Colour.BLACK;
    else
        turn = Colour.WHITE;
}

public static bool IsInRange(int x1, int y1, int x2, int y2)
{
    return IsInRange(x1, y1) && IsInRange(x2, y2);
}
public static bool IsInRange(int x, int y)
{
    return x >= 0 && x < 8 && y >= 0 && y < 8;
}
}
