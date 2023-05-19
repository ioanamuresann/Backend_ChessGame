using System;

public class MainClass
{
    public static void Main(string[] args)
    {
        // Create a new instance of the Game class
        Game game = new Game();

        try
        {
            // Perform moves or other game-related operations
            // Example:
            game.Move(1, 0, 2, 2);
            game.Move(6, 0, 5, 0);
            game.Move(0, 5, 0, 7);
        }
        catch (IllegalMoveException ex)
        {
            Console.WriteLine("Illegal move: " + ex.Message);
        }

        // Reset the game
        game.Reset();

        // Print the current state of the board
        PrintBoard(game.GetBoard());
    }

    private static void PrintBoard(ChessBoard board)
    {
        Tile[,] boardMatrix = board.BoardMatrix;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Piece piece = boardMatrix[i, j].GetPiece();
                string pieceName = (piece != null) ? piece.GetType().Name : "-";
                Console.Write(pieceName + " ");
            }
            Console.WriteLine();
        }
    }
}
