using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAssignment
{
    class GameBoard
    {
        //Making a Char 2d array - 8 by 8
        public char[,] ChessBoard = new char[8, 8];

        public string PlayerOne = "One"; //Player One Indicator
        public string PlayerTwo = "Two"; //Player Two Indicator

        public bool PlayerOneTurn = true;  //Player one's turn - begining starts with one
        public bool PlayerTwoTurn = false; //Player Two's turn - starts with false

        public int TurnCounter = 1; //Turncounter set to one

        //Starting screen showing user their options
        public void startScreen()
        {
            for (int i = 0; i < 1;) //Loop for displaying CHESS start screen
            {
                Console.Clear();

                Console.WriteLine("    ___ _  _ ___ ___ ___ ");
                Console.WriteLine("   / __| || | __/ __/ __|");
                Console.WriteLine(@"  | (__| __ | _|\__ \__ \ ");
                Console.WriteLine(@"   \___|_||_|___|___/___/");


                Console.WriteLine("\nWelcome to my Chess Game! (Enter: 1 - PVP)");
                if (!int.TryParse(Console.ReadLine(), out int UserInput)) //if the user does not input an int value
                {
                    Console.WriteLine("\nEnter correct inputs"); //keeps notifying them so and reloops
                }
                else if (UserInput == 1) //if the user inputs '1' (correct input)
                {
                    break; //breaks and goes to the actual game
                }

            }

        }

        //Starting positions for Chess Pieces 
        public void chessPieces()
        {
            //Pawns
            for (int i = 0; i < ChessBoard.GetLength(1); i++)
            {
                ChessBoard[i, 1] = 'p';
                ChessBoard[i, 6] = 'P';
                ChessBoard[i, 2] = ' '; //Needed for my code to work, or else it will not detect it as an empty ' ' char
                ChessBoard[i, 3] = ' ';
                ChessBoard[i, 4] = ' ';
                ChessBoard[i, 5] = ' ';
                
            }

          //Kings
            ChessBoard[4, 0] = 'k';
            ChessBoard[4, 7] = 'K';

            //Queen
            ChessBoard[3, 0] = 'q';
            ChessBoard[3, 7] = 'Q';

            //Bishop
            ChessBoard[5, 7] = 'B';
            ChessBoard[2, 7] = 'B';
            ChessBoard[5, 0] = 'b';
            ChessBoard[2, 0] = 'b';

            //Knights
            ChessBoard[6, 7] = 'N';
            ChessBoard[6, 0] = 'n';
            ChessBoard[1, 7] = 'N';
            ChessBoard[1, 0] = 'n';

            //Rooks
            ChessBoard[0, 7] = 'R';
            ChessBoard[0, 0] = 'r';
            ChessBoard[7, 7] = 'R';
            ChessBoard[7, 0] = 'r';


        }

        //Debug mode for test cases
        
        public void debugModePieces() //Need The For loop with spaces  and both kings(k and K) inorder for the board to work
        {
            for (int i = 0; i < ChessBoard.GetLength(1); i++) //Loop is required for  pieces to work/kill
            {
                ChessBoard[i, 0] = ' ';
                ChessBoard[i, 1] = ' ';
                ChessBoard[i, 2] = ' ';
                ChessBoard[i, 3] = ' ';
                ChessBoard[i, 4] = ' ';
                ChessBoard[i, 5] = ' ';
                ChessBoard[i, 6] = ' ';
                ChessBoard[i, 7] = ' ';

            }


            Console.WriteLine("\n   DEBUG MODE RUNNING");

            //Rook/King Swap for both sides of the board 



            //  ChessBoard[0, 7] = 'R';
            // ChessBoard[7, 7] = 'R';
            // ChessBoard[7, 0] = 'r';
            // ChessBoard[0, 0] = 'r';

            //Pawn Promotion 
            ChessBoard[0, 2] = 'k';
            ChessBoard[7, 4] = 'K';

            ChessBoard[6, 1] = 'P';
            ChessBoard[1, 6] = 'p';


        }
        

        //Source - Coding Homework: Altered ChessBoard Structure: https://www.youtube.com/watch?v=QGglep-HEvc&index=4&list=PLvQSG8B7sh6k9vaW-2OvVJxckE9OTdYpC
        public void drawBoard()
        {
            // Console.Clear();

            Console.WriteLine("\n    0   1   2   3   4   5   6   7  :X");

            for (int x = 0; x < ChessBoard.GetLength(1); x++)  //First loop set by the length of 8
            {
                Console.Write("  "); //Prints a space
                for (int y = 0; y < ChessBoard.GetLength(0); y++) //Second loop
                {
                    Console.Write("+---"); //Prints horizontally
                }
                Console.Write("+\n");//'+ ' with Newline 

                for (int i = 0; i < ChessBoard.GetLength(1); i++) //Third Loop
                {
                    if (i == 0)
                    {
                        Console.Write(x + " "); //Adds the number counter vertically and tile
                    }

                    Console.Write("| " + ChessBoard[i, x] + " "); //Prints the chess pieces
                }
                Console.Write("|\n"); 
            }

            Console.WriteLine("Y:+---+---+---+---+---+---+---+---+  "); //end line


        }

        //Win Detector for player one 
        public  bool  winDetectorP1(char[,] Board) 
        {
            for (int i = 0; i < Board.GetLength(0); i++) //Keeps checking the whole board
            {
                for (int b = 0; b < Board.GetLength(1); b++)
                {
                    if (Board[i, b] == 'k') //If the enmy king 'k'exists
                    {
                          
                        return false; //return false, P1 did not win the game yet
                    }
            
                }
           
            }
            Console.WriteLine("\nPlayer One Wins the Game! Opponent's King (k) Killed!"); //else P1 won the game, king is killed
            return true; //Returns true p1 won
        }

        public bool winDetectorP2(char[,] Board)
        {
            for (int i = 0; i < Board.GetLength(0); i++) //Keeeps checking the board for P1 King 'K'
            {
                for (int b = 0; b < Board.GetLength(1); b++)
                {
                    if (Board[i, b] == 'K')  // if the P1 king exists
                    {                       
                        return false; //Returns false P2 did not win the game yet as p1 king still exists
                    }
                }
            }
            Console.WriteLine("\nPlayer Two Wins the Game! Opponent's King (K) Killed!"); //else P2 wins the game and king is killed
            return true; //return true, P2 wins

        }

     
    }
}
