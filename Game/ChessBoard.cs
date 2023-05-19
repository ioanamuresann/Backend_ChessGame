public class ChessBoard
{
    public Tile[][] BoardMatrix { get; } = new Tile[8][];

    public ChessBoard(bool test)
    {
        for (int i = 0; i < 8; i++)
        {
            BoardMatrix[i] = new Tile[8];
            for (int j = 0; j < 8; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                        BoardMatrix[i][j] = new Tile(i, j, Colour.BLACK);
                    else
                        BoardMatrix[i][j] = new Tile(i, j, Colour.WHITE);
                }
                else
                {
                    if (j % 2 == 0)
                        BoardMatrix[i][j] = new Tile(i, j, Colour.WHITE);
                    else
                        BoardMatrix[i][j] = new Tile(i, j, Colour.BLACK);
                }
            }
        }

        if (test)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    BoardMatrix[i][j].Piece = null;
                }
            }

            BoardMatrix[7][7].Piece = new King(Colour.BLACK);
            BoardMatrix[6][5].Piece = new King(Colour.WHITE);
            BoardMatrix[0][6].Piece = new Queen(Colour.WHITE);
            return;
        }

        AddPieces(BoardMatrix);
    }

    private void AddPieces(Tile[][] boardMatrix)
    {
        for (int j = 0; j < 8; j++)
        {
            boardMatrix[1][j].Piece = new Pawn(Colour.WHITE);
            boardMatrix[6][j].Piece = new Pawn(Colour.BLACK);
        }

        boardMatrix[0][1].Piece = new Knight(Colour.WHITE);
        boardMatrix[0][6].Piece = new Knight(Colour.WHITE);
        boardMatrix[7][1].Piece = new Knight(Colour.BLACK);
        boardMatrix[7][6].Piece = new Knight(Colour.BLACK);

        boardMatrix[0][0].Piece = new Rook(Colour.WHITE);
        boardMatrix[0][7].Piece = new Rook(Colour.WHITE);
        boardMatrix[7][0].Piece = new Rook(Colour.BLACK);
        boardMatrix[7][7].Piece = new Rook(Colour.BLACK);

        boardMatrix[0][2].Piece = new Bishop(Colour.WHITE);
        boardMatrix[0][5].Piece = new Bishop(Colour.WHITE);
        boardMatrix[7][2].Piece = new Bishop(Colour.BLACK);
        boardMatrix[7][5].Piece = new Bishop(Colour.BLACK);

        boardMatrix[0][3].Piece = new Queen(Colour.WHITE);
        boardMatrix[7][3].Piece = new Queen(Colour.BLACK);

        boardMatrix[0][4].Piece = new King(Colour.WHITE);
        boardMatrix[7][4].Piece = new King(Colour.BLACK);
    }

    public bool IsInCheck(Colour colour)
    {
        Tile king = FindKing(colour);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Tile sourceTile = BoardMatrix[i][j];
                if (sourceTile.Piece != null && sourceTile !=(king))
                {
                    if (sourceTile.Piece.CheckMove(this, sourceTile, king) == 0)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

public bool IsCheckMate(Colour colour)
{
    if (!IsInCheck(colour))
        return false;

    return NoMoreLegalMoves(colour);
}

public bool IsStalemate(Colour colour)
{
    if (IsInCheck(colour))
        return false;

    return NoMoreLegalMoves(colour);
}

private bool NoMoreLegalMoves(Colour colour)
{
    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if (BoardMatrix[i][j].Piece != null && BoardMatrix[i][j].Piece.Colour == colour)
            {
                Piece piece = BoardMatrix[i][j].Piece;
                List<Tile> legalMoves = piece.GetLegalMoves(this, BoardMatrix[i][j]);
                foreach (Tile candidateTile in legalMoves)
                {
                    if (!CausesCheck(BoardMatrix[i][j], candidateTile, colour))
                    {
                        return false;
                    }
                }
            }
        }
    }
    return true;
}

public bool CausesCheck(Tile start, Tile end, Colour colour)
{
    Piece aux = Game.MovePiece(start, end);
    bool ok = IsInCheck(colour);
    start.Piece = aux;
    Game.SwapPieces(start, end);
    return ok;
}

public Tile GetTile(int x, int y)
{
    if (x < 0 || x > 7 || y < 0 || y > 7)
        return null;

    return BoardMatrix[x][y];
}

[Obsolete("No longer using system out as a GUI has been implemented")]
public void PrintChessBoard(bool printPieces)
{
    if (printPieces)
    {
        for (int i = 7; i >= 0; i--)
        {
            Console.Write(i + ": ");
            for (int j = 0; j < 8; j++)
            {
                Console.Write(j + " " + BoardMatrix[i][j].Piece + " ");
            }
            Console.WriteLine();
        }
    }
    else
    {
        for (int i = 7; i >= 0; i--)
        {
            Console.Write(i + ": ");
            for (int j = 0; j < 8; j++)
            {
                Console.Write(j + " " + BoardMatrix[i][j].Colour + " ");
            }
            Console.WriteLine();
        }
    }
}
}
