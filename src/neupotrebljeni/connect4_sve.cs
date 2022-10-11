using System;

class Connect4
{
    public enum MoveStatus { Invalid, Regular, Wining, Draw };
    public const int ROW_COUNT = 6;
    public const int COL_COUNT = 7;
    public const int N = 4;

    public const int EMPTY_SQUARE = -1;
    public const int UNDEFINED_COLUMN = -2;

    // position in the game
    private int[,] table = new int[ROW_COUNT, COL_COUNT];

    // helper array to optimize computation
    private int[] firstEmptySquare = new int[COL_COUNT];

    // Constructor
    public Connect4()
    {
        ClearTheBoard();
    }

    public char this[int row, int col]
    {
        get
        {
            if (table[row, col] == EMPTY_SQUARE) return ' ';
            if (table[row, col] == 0) return 'X';
            if (table[row, col] == 1) return 'O';
            throw new Exception("Nedopustena vrednost u tabeli");
        }
    }

    public void ClearTheBoard()
    {
        for (int row = 0; row < ROW_COUNT; row++)
            for (int col = 0; col < COL_COUNT; col++)
                table[row, col] = EMPTY_SQUARE;

        for (int col = 0; col < COL_COUNT; col++)
            firstEmptySquare[col] = 0;

    }

    public MoveStatus Play(int player, int col)
    {
        if (col < 0 || col >= COL_COUNT)
            return MoveStatus.Invalid;

        int row = firstEmptySquare[col];
        if (row >= ROW_COUNT)
            return MoveStatus.Invalid;

        table[row, col] = player;
        firstEmptySquare[col]++;
        return GetMoveStatus(row, col);
    }

    private MoveStatus GetMoveStatus(int row, int col)
    {
        if (IsWin(row, col))
            return MoveStatus.Wining;

        int minCol = ROW_COUNT + 1;
        for (int c = 0; c < COL_COUNT; c++)
            minCol = Math.Min(minCol, firstEmptySquare[c]);

        if (minCol < ROW_COUNT)
            return MoveStatus.Regular;
        else
            return MoveStatus.Draw;

    }
    private bool IsWin(int row, int col)
    {
        int player = table[row, col];
        int[] dirX = { -1, 0, 1, -1 };
        int[] dirY = { -1, -1, -1, 0 };
        int n, x1, y1;
        for (int dir = 0; dir < 4; dir++)
        {
            n = 0;
            x1 = col;
            y1 = row;
            while (0 <= x1 && x1 < COL_COUNT && 0 <= y1 && y1 < ROW_COUNT && table[y1, x1] == player)
            {
                x1 += dirX[dir];
                y1 += dirY[dir];
                n++;
            }
            x1 = col;
            y1 = row;
            while (0 <= x1 && x1 < COL_COUNT && 0 <= y1 && y1 < ROW_COUNT && table[y1, x1] == player)
            {
                x1 -= dirX[dir];
                y1 -= dirY[dir];
                n++;
            }
            if (n >= N + 1) // N + 1 because starting piece [row, col] is counted twice
                return true;
        }
        return false;
    }
}

class Program
{
    static void Display(Connect4 game)
    {
        Console.Clear();
        for (int row = Connect4.ROW_COUNT - 1; row >= 0; row--)
        {
            Console.Write("|");
            for (int col = 0; col < Connect4.COL_COUNT; col++)
            {
                Console.Write(game[row, col]);
                Console.Write("|");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.Write(" ");
        for (int col = 0; col < Connect4.COL_COUNT; col++)
        {
            Console.Write((char)('A' + col));
            Console.Write(" ");
        }
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        Connect4 game = new Connect4();
        Display(game);
        string player = "XO";
        int nextPlayer = 1;
        bool gameOver = false;
        Connect4.MoveStatus status = Connect4.MoveStatus.Invalid;
        while (!gameOver)
        {
            nextPlayer = 1 - nextPlayer;
            status = Connect4.MoveStatus.Invalid;
            while (status == Connect4.MoveStatus.Invalid)
            {
                Console.Write("Igrac {0} - polje? ", player[nextPlayer]);
                char c = Console.ReadLine().ToUpper()[0];
                int col = c - 'A';
                status = game.Play(nextPlayer, col);
                Display(game);
            }
            if (status == Connect4.MoveStatus.Wining ||
                status == Connect4.MoveStatus.Draw)
                gameOver = true;
        }
        if (status == Connect4.MoveStatus.Wining)
            Console.WriteLine("Igrac {0} je podedio.", player[nextPlayer]);
        else if (status == Connect4.MoveStatus.Draw)
            Console.WriteLine("Nereseno.");
    }
}
