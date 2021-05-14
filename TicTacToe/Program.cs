using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lets Play!");
            int n = int.Parse(Console.ReadLine());
            string player1Name = Console.ReadLine();
            string player2Name = Console.ReadLine();

            TicTacToe Game = new TicTacToe(n, player1Name, player2Name);

            while(Game.GameStatus == GameStatus.Active)
            {
                bool turn = bool.Parse(Console.ReadLine());
                int row = int.Parse(Console.ReadLine());
                int col = int.Parse(Console.ReadLine());
                Game.addMove(turn, row, col);
            }
            Console.WriteLine(Game.GameStatus);
            return;
        }
    }

    class TicTacToe
    {
        int[,] Board;
        Player Player1;
        Player Player2;
        bool currPlayer = true;
        List<Tuple<int, int>> Moves = new List<Tuple<int, int>>();
        int n;
        public GameStatus GameStatus { get; set; } = GameStatus.Active;
        public TicTacToe(int n, string Player1Name, string Player2Name)
        {
            this.n = n;
            this.Board = new int[n, n];
            this.Player1 = new Player(Player1Name);
            this.Player2 = new Player(Player2Name);
        }

        public int addMove(bool CurrentPlayer, int row, int col)
        {
            row--;col--;
            if (this.currPlayer != CurrentPlayer)
                throw new Exception("other player's turn");
            else if(GameStatus != GameStatus.Active)
            {
                throw new Exception("Game has been finished");
            }
            else if (row >= n | col >= n | row < 0 | col < 0) {
                throw new Exception("index out of range");
            } 

            else if(Board[row, col] != 0)
            {
                throw new Exception("square is already taken");
            } 

            else
            {
                try
                {
                    this.currPlayer = !this.currPlayer;
                    Moves.Add(new Tuple<int, int>(row, col));
                    int Player = CurrentPlayer ? 1 : -1;
                    Board[row, col] = Player;
                    int vert, horz, diag, reverDiag;
                    vert = horz = diag = reverDiag = 0;

                    for (int i = 0; i < n; i++)
                        vert += Board[(row + i) % n, col];

                    for (int i = 0; i < n; i++)
                        horz += Board[row, (col + i) % n];

                    if (row == col)
                    {
                        for (int i = 0; i < n; i++)
                            diag += Board[(row + i) % n, (col + i) % n];
                    }

                    if (row == n - 1 - col)
                    {
                        for (int i = 0; i < n; i++)
                            reverDiag += Board[(row + i) % n, (col - i + n) % n];
                    }
                    int temp = Player * n;
                    if (vert == temp | horz == temp| diag == temp | reverDiag == temp)
                    {
                        if (CurrentPlayer)
                            GameStatus = GameStatus.Player1Win;
                        else
                            GameStatus = GameStatus.Player2Win;
                        showBoard();
                        return Player;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            showBoard();
            return 0;
        }

        void showBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(Board[i, j].ToString() + " " );
                }
                Console.Write('\n');
            }
        }
    }

    class Player
    {
        string name;
        public Player (string name)
        {
            this.name = name;
        }
    }

    public enum GameStatus
    {
        Player1Win, Player2Win, Active, Forefeit
    }
}
