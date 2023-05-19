public class Tile
{
    private int x;
    private int y;
    private Colour colour;
    private Piece piece;

    public Tile(int x, int y, Colour colour, Piece piece)
    {
        this.x = x;
        this.y = y;
        this.colour = colour;
        this.piece = piece;
    }

    public Tile(int x, int y, Colour colour)
        : this(x, y, colour, null)
    {
    }

    public Tile(int x, int y)
        : this(x, y, Colour.WHITE, null)
    {
    }

    public override string ToString()
    {
        return "TILE " + x + " " + y;
    }

    // GETTERS AND SETTERS

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public Colour Colour
    {
        get { return colour; }
        set { colour = value; }
    }

    public Piece Piece
    {
        get { return piece; }
        set { piece = value; }
    }
}
