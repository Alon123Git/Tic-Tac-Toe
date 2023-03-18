using System;
using System.ComponentModel;

namespace TicTacTow
{
    internal class Program
    {
        #region Data Members
        static char[,] board = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        static char[,] boardForCheck = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        static char user = 'O';
        static char computer = 'X';
        static int turn = 0;
        static string winner;
        static string looser;
        static string player = "player";
        static string cp = "computer";
        static int pick = 0;
        static int NewGame = 0;
        static int PlayerScore = 0;
        static int ComputerScore = 0;
        #endregion

        /// <summary>
        /// Reset board
        /// </summary>
        /// <param name="matrix">matrix of chars</param>
        /// <returns>matrix of chars</returns>
        static char[,] ResetBoard(ref char[,] matrix)
        {
            #region ResetBoard
            int counter = 1;
            Console.CursorTop = 5;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.CursorLeft = 10;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = (char)(counter + '0');
                    counter++;
                }
                Console.WriteLine("\n\n");
            }
            return matrix;
            #endregion
        }
        /// <summary>
        /// Print game board
        /// </summary>
        /// <param name="matrix">matrix board</param>
        /// <returns>matrix board</returns>
        static char[,] PrintBoard(ref char[,] matrix)
        {
            #region Print_Board
            Console.CursorTop = 5;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.CursorLeft = 10;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0}\t\t", matrix[i, j]);
                }
                Console.WriteLine("\n\n");
            }
            return matrix;
            #endregion
        }

        /// <summary>
        /// print update game board
        /// </summary>
        /// <param name="matrix">matrix of chars</param>
        /// <returns></returns>
        static char[,] UpdateBoard(ref char[,] matrix)
        {
            #region Updated_Board
            Console.CursorTop = 5;
            for (int i = 0; i < 3; i++)
            {
                Console.CursorLeft = 10;
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}\t\t", matrix[i, j]);
                }
                Console.WriteLine("\n\n");
            }
            return matrix;
            #endregion
        }

        /// <summary>
        /// put O or X in the right position and print the update board. also check if there are winner
        /// </summary>
        /// <param name="u">user</param>
        /// <param name="c">computer</param>
        /// <param name="matrix">matrix of chars</param>
        static void Position(ref char u, ref char c, ref char[,] matrix, ref int p, ref int ng)
        {
            #region Position
            Console.WriteLine("Who start play first?\nFor you to start to play first enter 1.\n" +
                "For the computer start to play first enter 2: ");
            turn = int.Parse(Console.ReadLine());
            while (turn < 1 || turn > 2)
            {
                Console.WriteLine("ERROR");
                turn = int.Parse(Console.ReadLine());
            }
            Console.Clear();
            ResetBoard(ref board);
            PrintBoard(ref board);
            // error handling

            while (true)
            {
                if (turn == 1)
                {
                    turn = 2;
                }
                else if (turn == 2)
                {
                    turn = 1;
                }

                if (turn == 2)
                {
                    Console.WriteLine("Enter position between 1 to 9 please,\nand after your turn the turn move to the computer: ");
                    p = int.Parse(Console.ReadLine());

                    // error handling
                    while (p < 1 || p > 9)
                    {
                        Console.WriteLine("ERROR");
                        p = int.Parse(Console.ReadLine());
                    }

                    // check if the position is already occupied
                    int row = (p - 1) / 3;
                    int col = (p - 1) % 3;
                    if (matrix[row, col] == 'X' || matrix[row, col] == 'O')
                    {
                        Console.WriteLine("Position already occupied. Please choose another position.");
                        continue;
                    }

                    p = (p == 1) ? matrix[0, 0] = u : (p == 2) ? matrix[0, 1] = u : (p == 3) ? matrix[0, 2] = u : (p == 4) ? matrix[1, 0] = u :
                        (p == 5) ? matrix[1, 1] = u : (p == 6) ? matrix[1, 2] = u : (p == 7) ? matrix[2, 0] = u : (p == 8) ? matrix[2, 1] = u :
                        matrix[2, 2] = u;
                }
                else if (turn == 1)
                {
                    Random r = new Random();
                    p = r.Next(1, 9);

                    while (matrix[(p - 1) / 3, (p - 1) % 3] == 'X' || matrix[(p - 1) / 3, (p - 1) % 3] == 'O')
                    {
                        p = r.Next(1, 9);
                    }

                    p = (p == 1) ? matrix[0, 0] = c : (p == 2) ? matrix[0, 1] = c : (p == 3) ? matrix[0, 2] = c : (p == 4) ? matrix[1, 0] = c :
                       (p == 5) ? matrix[1, 1] = c : (p == 6) ? matrix[1, 2] = c : (p == 7) ? matrix[2, 0] = c : (p == 8) ? matrix[2, 1] = c :
                       matrix[2, 2] = c;
                }
                UpdateBoard(ref board);
                CheckWinner(ref user, ref computer, ref board, ref winner, ref looser, ref player, ref cp, ref PlayerScore, ref ComputerScore, ref boardForCheck);
                if (ng == 2)
                {
                    break;
                }
                //IsTie(ref board, ref boardForCheck);
                //if (IsTie(ref board, ref boardForCheck) == false)
                //{
                //    IsWin();
                //}
            }
            #endregion
        }

        /// <summary>
        /// Check if there are a tie
        /// </summary>
        /// <param name="matrix">matrix of chars</param>
        /// <param name="u">user char</param>
        /// <param name="c">computer char</param>
        /// <param name="p">player name (string)</param>
        /// <param name="comp">computer name (string)</param>
        /// <param name="t">tie string</param>
        static void IsTie()
        {
            #region Is Tie
            Console.CursorTop = 10;
            Console.WriteLine("The game is tie.");
            #endregion
        }

        /// <summary>
        /// Check if player or computer win
        /// </summary>
        /// <param name="u">user char</param>
        /// <param name="c">computer char</param>
        /// <param name="matrix">matrix of char</param>
        /// <param name="win">represent who is the winner</param>
        /// <param name="p">player</param>
        /// <param name="comp">computer</param>
        static bool CheckWinner(ref char u, ref char c, ref char[,] matrix, ref string win, ref string lose, ref string p, ref string comp, ref int pScore, ref int cScore, ref char[,] matrixForCheck)
        {
            #region Check_If_There_Are_Winner
            if (matrix[0, 0] == u && matrix[0, 1] == u && matrix[0, 2] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[1, 0] == u && matrix[1, 1] == u && matrix[1, 2] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[2, 0] == u && matrix[2, 1] == u && matrix[2, 2] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 0] == u && matrix[1, 0] == u && matrix[2, 0] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 1] == u && matrix[1, 1] == u && matrix[2, 1] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[2, 0] == u && matrix[2, 1] == u && matrix[2, 2] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 0] == u && matrix[1, 1] == u && matrix[2, 2] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 2] == u && matrix[1, 1] == u && matrix[2, 0] == u)
            {
                win = p;
                lose = comp;
                pScore += 1;
                IsWin(true);
                return true;
            }

            else if (matrix[0, 0] == c && matrix[0, 1] == c && matrix[0, 2] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[1, 0] == c && matrix[1, 1] == c && matrix[1, 2] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[2, 0] == c && matrix[2, 1] == c && matrix[2, 2] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 0] == c && matrix[1, 0] == c && matrix[2, 0] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 1] == c && matrix[1, 1] == c && matrix[2, 1] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[2, 0] == c && matrix[2, 1] == c && matrix[2, 2] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 0] == c && matrix[1, 1] == c && matrix[2, 2] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            else if (matrix[0, 2] == c && matrix[1, 1] == c && matrix[2, 0] == c)
            {
                win = comp;
                lose = p;
                cScore += 1;
                IsWin(true);
                return true;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == matrixForCheck[i, j])
                    {
                        return true;
                    }
                }
            }
            IsWin(false);
            return false;
            #endregion;
        }

        /// <summary>
        /// The score of the game
        /// </summary>
        /// <param name="pScore">player score</param>
        /// <param name="cScore">computer score</param>
        /// <param name="plar">player name (string)</param>
        /// <param name="comp">computer name (string)</param>
        /// <param name="win">who win</param>
        /// <param name="lose">who lose</param>
        static void Score(ref int pScore, ref int cScore, ref string plar, ref string comp, ref string win, ref string lose, bool isFinish)
        {
            #region Score
            Console.CursorTop = 10;
            Console.WriteLine("Great game! the game winner is: *{0}*, the game looser is: *{1}*.\n\n", win, lose);
            Console.Write("The score is: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(pScore);
            Console.ResetColor();
            Console.Write(" to player, and ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(cScore);
            Console.ResetColor();
            Console.WriteLine(" to computer");
            if (pScore > cScore)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0} have the lead", plar);
                Console.ResetColor();
            }
            else if (pScore < cScore)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} have the lead", comp);
                Console.ResetColor();
            }
            else if (pScore == cScore)
            {
                Console.WriteLine("The game is tied");
            }
            #endregion
        }

        /// <summary>
        /// Ask if to play another game
        /// </summary>
        /// <param name="ng">new game int</param>
        /// <returns>new game int</returns>
        static int AnotherGame(ref int ng)
        {
            #region Another Game
            Console.WriteLine("Would you like to play another game?\nIf yes press 1, if no press 2:");
            ng = int.Parse(Console.ReadLine());

            // error handling
            while (ng < 1 && ng > 2)
            {
                Console.WriteLine("ERROR");
                ng = int.Parse(Console.ReadLine());
            }
            if (ng == 1)
            {
                Console.Clear(); // Clear the console
                Main(); // Start the game again
            }
            else if (ng == 2)
            {
                Console.Clear(); // Clear the console
                PrintBoard(ref board); // Reset the board
                Console.WriteLine("Bye Bye :)");
            }
            return ng;
            #endregion
        }

        /// <summary>
        /// show who is won the game - call to the Score function and the AnotherGame function.
        /// </summary>
        static void IsWin(bool isFinish)
        {
            #region Is Win
            Console.Clear();
            //if (isFinish == true)
            //{
            //    Console.WriteLine("The result is tie");
            //    IsTie();
            //}
            Score(ref PlayerScore, ref ComputerScore, ref player, ref cp, ref winner, ref looser, true);
            AnotherGame(ref NewGame);

            #endregion
        }

        /// <summary>
        /// Main - call the PrintBoard and Position functions
        /// </summary>
        static void Main()
        {
            #region Main
            ResetBoard(ref board);
            PrintBoard(ref board);
            Position(ref user, ref computer, ref board, ref pick, ref NewGame);
            #endregion
        }
    }
}