using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAssignment
{
    //Ronan Almeida
    //Date: October 19th, 2018
    //Program: Assignment 2: BattleChess
    //Purpose To simulate chess in a console form. PVP, Player One is uppercase letters while Player Two is Lowercase Letters. A player wins by killing a king 
 

    //Enhancement 
    //-Rook/King Swap 

    class Program
    {
        static void Main(string[] args)
        {

            for (int c = 0; c < 1;) //Outer infinitly Looping, only broken
            {

                GameBoard NewGame = new GameBoard(); //Making a new class var 'NewGame' 

                 NewGame.startScreen(); //Intro Start Screen, disable it  if you want instant PVP


                 NewGame.chessPieces(); //Starting Game Chess Pieces

                // NewGame.debugModePieces(); //Debuging method for  custom preset pieces, disable for Acutal normal chess

                //Rook Movement Counters (Used for King/Rook Swap)
                int RookP1CounterRight = 0;
                int RookP1CounterLeft = 0;
                int RookP2CounterRight = 0;
                int RookP2CounterLeft = 0;


                Console.WriteLine("\nPlayer One has Upper Case Pieces Eg:'K' 'Q' 'P' | PlayerTwo(or AI) Has Lower Case Pieces Eg:'k' 'q' 'p' ");

                string ReadName = "One"; //Indicator stating whos turn 

                for (int b = 0; b < 1;) //Inner Loop for moves/games to be run
                {

                    NewGame.drawBoard(); //Draws board


                    if (NewGame.winDetectorP1(NewGame.ChessBoard) == true || NewGame.winDetectorP2(NewGame.ChessBoard) == true) //If P1 OR P2 win detector is true, call and excute the method
                    {

                        Console.WriteLine("\n Would You Like To Play Again? (Y - Yes/ N - Anything else)"); //Asks if user would like to go again

                        string UserInput = Console.ReadLine().ToUpper();

                        if (UserInput == "Y" | UserInput == "YES" | UserInput == "YE")
                        {
                            break; //Breaks if user enters yes and goes to the outer loop startscreen, for a new game
                        }

                        else
                        {
                            Environment.Exit(0); //if user inputs anything besides a yes then exit program
                        }

                    }


                    int CounterPlaceHolder = NewGame.TurnCounter; //Counter placeholder 

                    Console.Write("\nTurn: " + NewGame.TurnCounter + " | Player: " + ReadName + "'s turn \n"); //Turn indicator/ Player Indicator

                    //Asks  input for old x old y, new x new y
                    Console.Write("\nEnter The X Postion: ");
                    int.TryParse(Console.ReadLine(), out int InputX);

                    Console.Write("\nEnter The Y Postion: ");
                    int.TryParse(Console.ReadLine(), out int InputY);

                    Console.Write("\nEnter the desired X Postion: ");
                    int.TryParse(Console.ReadLine(), out int DesiredX);

                    Console.Write("\nEnter the desired Y Postion: ");
                    int.TryParse(Console.ReadLine(), out int DesiredY);


                    //If it detects an error
                    if (errorDetection(NewGame.ChessBoard, InputX, InputY, DesiredX, DesiredY,NewGame.PlayerOneTurn,NewGame.PlayerTwoTurn) == true)
                    {
                        Console.WriteLine("\nInput Correct Coordinates! \n");
                        continue; //Redo 
                    }

                    else //No erros are detected
                    {
                        //Each var has a method to detect if the rook has moved, for the King/Rook swap to work
                        RookP1CounterRight = rookP1MoveCounterRight(NewGame.ChessBoard, InputX, InputY, DesiredX, DesiredY, RookP1CounterRight);
                        RookP1CounterLeft = rookP1MoveCounterLeft(NewGame.ChessBoard, InputX, InputY, DesiredX, DesiredY, RookP1CounterLeft);
                        RookP2CounterRight = rookP2MoveCounterRight(NewGame.ChessBoard, InputX, InputY, DesiredX, DesiredY, RookP2CounterRight);
                        RookP2CounterLeft = rookP2MoveCounterLeft(NewGame.ChessBoard, InputX, InputY, DesiredX, DesiredY, RookP2CounterLeft);
                    
                        //Makes a move on the board by calling method "makeMovement" - returns counter+1
                        NewGame.TurnCounter = makeMovement(NewGame.ChessBoard, InputX, InputY, DesiredX, DesiredY, NewGame.TurnCounter, RookP1CounterRight,RookP1CounterLeft,RookP2CounterRight,RookP2CounterLeft);


                        if (NewGame.TurnCounter > CounterPlaceHolder) //If the turn increments, switch Player one's/Two's boolean turn vars
                        {
                            if (NewGame.PlayerOneTurn == true)
                            {
                                ReadName = NewGame.PlayerTwo;  //Switches readname to "Two"
                                NewGame.PlayerOneTurn = false;
                                NewGame.PlayerTwoTurn = true;
                            }
                            else if (NewGame.PlayerTwoTurn == true)
                            {
                                ReadName = NewGame.PlayerOne;
                                NewGame.PlayerTwoTurn = false;
                                NewGame.PlayerOneTurn = true;
                            }

                        }

                    }

                }

            }

            Console.ReadKey();
        }
        //Checking for errors and if a piece can make a legal move 
        public static bool errorDetection(char[,] Board, int CordX, int CordY, int DesX, int DesY, bool PlayerOneTurn, bool PlayerTwoTurn)
        {
            //All the peices P1 can kill
            char pawn = 'p';
            char queen = 'q';
            char king = 'k';
            char knight = 'n';
            char bishop = 'b';
            char rook = 'r';
            
            if (PlayerOneTurn==true) 
            {
               //Do  nothing becuase peices are already set to lowercase        
            }        
            else if( PlayerTwoTurn==true) //all the pieces P2 can kill
            {
                 pawn = 'P';
                 queen = 'Q';
                 king = 'K';
                 knight = 'N';
                 bishop = 'B';
                 rook = 'R';

            }

            //If user input is out of bounds for the array or user inputs nothing
            if (CordX > Board.GetLength(0) - 1 | CordX < 0 | DesX < 0 | DesX > Board.GetLength(0) - 1 | CordY > Board.GetLength(0) - 1 | CordY < 0 | DesY < 0 | DesY > Board.GetLength(0) - 1| CordX == ' ' | CordY==' '| DesX == ' ' | DesY== ' ')
            {
                return true;
            }

            if(Board[CordX,CordY] == 'n' | Board[CordX, CordY] == 'N' ) //If the selected peice is a knight then it goes through without any errors
            {                                                           //Knight can jump over other pieces
                return false;
            }



                //Vertically checking from Bottom  to top  if path is clear- 
                if (CordX == DesX && CordY > DesY)  
               {
                    for (int i = CordY - DesY; i > 0; i--)
                    {
                        if (Board[CordX, CordY - i] != ' ')//if the path doesnot contain a ' ' 
                        {
                        //if the destitation  at new x new y contains an enemy piece
                          if(DesX == CordX && DesY == CordY-i && Board[DesX, DesY]  == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                          {
                           continue; //continue to next statement
                          }

                            return true; //else return error that path isnt clear and piece is  in the way
                        }
                    }
                              
               }

                //Rest is mostly repeated code with tons of differnt conditions

                //Vertically checking from top  to bottom if path is clear- 
                 if (CordX == DesX && CordY < DesY)
                 {
                   for (int i = DesY - CordY; i >0; i--)
                   {
                     if (Board[CordX,CordY+i] != ' ')
                     {
                        if (DesX == CordX && DesY == CordY + i && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                        {
                            continue;
                        }
                        return true;
                      }
                    }
                 }



            //Horizontally checking from right to left if path is clear -
            if (CordY == DesY && CordX > DesX)
            {
              for( int i=CordX-DesX; i>0; i-- )
              {

                    if (Board[CordX-i,DesY]!= ' ')
                    {
                        if (DesX == CordX-i && DesY == DesY && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                        {
                            continue;
                        }

                        return true;
                        
                    }                           
              }
            }
        
            //Horizontally checking from left to right if path is clear - 
            if (CordY == DesY && CordX < DesX)
            {
                for (int i = DesX-CordX; i >0; i--)
                {
                    if(Board[CordX+i,DesY] != ' ')
                    {
                        if (DesX == CordX+i && DesY == DesY && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                        {
                            continue;
                        }
                        return true;
                    }


                }
            }
       
                       //Diagonally Checking from northeast- 
                        if (CordX < DesX && CordY > DesY)
                        {
                            for (int i = DesX - CordX; i > 0; i--)
                            {
                                if (Board[CordX + i, CordY - i] != ' ')
                                {
                                  if (DesX == CordX+i && DesY == CordY - i && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                                  {
                                   continue;
                                  }
                                     return true;
                                }
                            }
                        }
 


                        //Diagonally checking from Northwest -
                        if (CordX > DesX && CordY > DesY)
                        {
                            for (int i = CordX - DesX; i > 0; i--)
                            {
                                if (Board[CordX - i, CordY - i] != ' ')
                                {
                                   if (DesX == CordX-i && DesY == CordY - i && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                                   {
                                       continue;
                                   }

                                  return true;
                                }
                            }

                        }
            
                       //Diagonally checking from southeast -
                        if (CordX < DesX && CordY < DesY)
                        {
                            for (int i = DesX - CordX; i > 0; i--)
                            {
                                if (Board[CordX + i, CordY + i] != ' ')
                                {
                                     if (DesX == CordX+i && DesY == CordY + i && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                                     {
                                       continue;
                                     }
                                     return true;
                                }
                            }
                        }
               
                        //Diagonally checking from southwest- 
                        if (CordX > DesX && CordY < DesY)
                        {
                            for (int i = CordX - DesX; i > 0; i--)
                            {
                                if (Board[CordX - i, CordY + i] != ' ')
                                {

                                    if (DesX == CordX-i && DesY == CordY + i && Board[DesX, DesY] == pawn | Board[DesX, DesY] == queen | Board[DesX, DesY] == king | Board[DesX, DesY] == rook | Board[DesX, DesY] == knight | Board[DesX, DesY] == bishop)
                                    {
                                    continue;
                                     }
                                     return true;
                                }
                            }
                        }

                
            return false; //If Path is clear and User input is not out of bounds, no erros detected
           
        }



        public static int makeMovement(char[,] Board, int CordX, int CordY, int DesX, int DesY, int counter, int RP1R, int RP1L, int RP2R, int RP2L)
        {
         


            Moves MoveItem = new Moves(); //Calls move class so it can use multiple moves from the libary

            //Player Two's Turn all even numbers - LowerCase Char
            if (counter % 2 == 0)
            {
                if (MoveItem.movePawnP2NotKilling(CordX, CordY, DesX, DesY, Board) == false) //If P2 pawn can kill a piece infront of it
                {
                    return counter; //just return the counter, reloops  
                }

                if (Board[CordX, CordY] == 'p' && MoveItem.moveOneStepPlayerTwo(CordX, CordY, DesX, DesY) == true) //if the peice is a p2 p and it can move one step up
                {

                    Board[DesX, DesY] = Board[CordX, CordY]; //the new desitication is now a pawn 

                    //Pawn Promotion
                    if (DesY == 7) //if p2 pawn reaches the end of the board
                    {
                        for (int i = 0; i < 1;) //Asks for promotion
                        {
                            Console.WriteLine("\nTo Which Piece Would You Like To Promote Your Pawn? |Enter: 1-q | 2-n | 3-b | 4-r |");

                            if (!int.TryParse(Console.ReadLine(), out int UserPromotion)) //if input is not an int
                            {
                                Console.WriteLine("\nEnter correct inputs!"); //reloops 
                                continue; //continues
                            }

                            else if (UserPromotion == 1) //Changes pawn to whatever user requests
                            {
                                Board[DesX, DesY] = 'q';
                                break;
                            }
                            else if (UserPromotion == 2)
                            {
                                Board[DesX, DesY] = 'n';
                                break;
                            }
                            else if (UserPromotion == 3)
                            {
                                Board[DesX, DesY] = 'b';
                                break;

                            }
                            else if (UserPromotion == 4)
                            {
                                Board[DesX, DesY] = 'r';
                                break;

                            }
                        }
                    }


                    Board[CordX, CordY] = ' ';//changes the orginal x and y postion to  a ' '
                    return counter + 1; //returns counter+1 so the turn can increment

                }
                //King/Rook Switch RIGHT side Player two
                if (Board[CordX, CordY] == 'k' && MoveItem.swapKingRookPlayerTwoRightSide(CordX, CordY, DesX, DesY, Board) == true && RP2R == 0)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[7, 0] = ' ';
                    Board[5, 0] = 'r'; //preset condtion to switch rook
                    Board[CordX, CordY] = ' ';
                    return counter + 1; //returns counter+1 to increment

                }

                //Rook/King Switch Left side player two
                if (Board[CordX, CordY] == 'k' && MoveItem.swapKingRookPlayerTwoLeftSide(CordX, CordY, DesX, DesY, Board) == true && RP2L == 0)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[0, 0] = ' ';
                    Board[3, 0] = 'r';
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                //if king movement is true, Move the king and increment counter
                if ( Board[CordX, CordY] == 'k' && MoveItem.moveOneStepAllDirections(CordX, CordY, DesX, DesY) == true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }
                //if Queen movement is true, Move the king and increment counter
                if (Board[CordX, CordY] == 'q' && MoveItem.queenMovement(CordX, CordY, DesX, DesY) == true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                //if Bishop movement is true, Move the king and increment counter
                if (Board[CordX, CordY] == 'b' && MoveItem.diagonalMove(CordX, CordY, DesX, DesY) == true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                //if Knight movement is true, Move the king and increment counter. Knight can also jump over other pieces and kill enemy pieces
                if ( Board[CordX, CordY] == 'n' && MoveItem.knightLShape(CordX, CordY, DesX, DesY) == true && Board[DesX,DesY] == ' ' | Board[DesX, DesY] == 'P' | Board[DesX, DesY] == 'R' | Board[DesX, DesY] == 'N' | Board[DesX, DesY] == 'B' | Board[DesX, DesY] == 'Q' | Board[DesX, DesY] == 'K')
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                //if Rook movement is true, Move the king and increment counter
                if (Board[CordX, CordY] == 'r' && MoveItem.moveVertical(CordX, CordY, DesX, DesY) == true | MoveItem.moveSideWay(CordX, CordY, DesX, DesY) == true)
                {

                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                //if pawn  is selected and it can move two up true, Move the king and increment counter 
                if (Board[CordX, CordY] == 'p' && MoveItem.moveTwoStepsP2(CordX, CordY, DesX, DesY) == true |MoveItem.movePawnKillP2(CordX, CordY, DesX, DesY,Board)== true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;
                }



            }

            //PlaterOne's Turn All odd numbers -UpperCase Char
            //Same code as P2 Just copy and pasted
            else
            {
                    if (MoveItem.movePawnP1NotKilling(CordX, CordY, DesX, DesY, Board) == false)
                    {
                        return counter;
                    }


                if (Board[CordX, CordY] == 'P' && MoveItem.moveOneStepPlayerOne(CordX, CordY, DesX, DesY) == true  )
                {


                    Board[DesX, DesY] = Board[CordX, CordY];

                    //Pawn Promotion
                    if (DesY == 0)
                    {
                        for( int i=0;i<1;)
                        {
                            Console.WriteLine("\nTo Which Piece Would You Like To Promote Your Pawn? |Enter: 1-Q | 2-N | 3-B | 4-R |");
                            
                            if(!int.TryParse(Console.ReadLine(), out int UserPromotion))
                            {
                                Console.WriteLine("Enter correct inputs!");
                                continue;
                            }

                            else if (UserPromotion == 1)
                            {
                                Board[DesX, DesY] = 'Q';
                                break;
                            }
                            else if (UserPromotion == 2)
                            {
                                Board[DesX, DesY] = 'N';
                                break;
                            }
                            else if (UserPromotion == 3)
                            {
                                Board[DesX, DesY] = 'B';
                                break;

                            }
                            else if (UserPromotion == 4)
                            {
                                Board[DesX, DesY] = 'R';
                                break;

                            }
                                           
                        }                                          
                    }


                    Board[CordX, CordY] = ' ';
                    return counter + 1;
                }
                //Rook swap Playerone RightSide
                if (Board[CordX, CordY] == 'K' && MoveItem.swapKingRookPlayerOneRightSide(CordX, CordY, DesX, DesY, Board) == true&& RP1R==0)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[7, 7] = ' ';
                    Board[5, 7] = 'R';
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                //Rook swap PlayerOne LeftSide
                if(Board[CordX, CordY] == 'K'&& MoveItem.swapKingRookPlayerOneLeftSide(CordX, CordY, DesX, DesY, Board) == true && RP1L==0)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[0, 7] = ' ';
                    Board[3, 7] = 'R';
                    Board[CordX, CordY] = ' ';
                    return counter + 1;
                }



                if (Board[CordX, CordY] == 'K'  && MoveItem.moveOneStepAllDirections(CordX, CordY, DesX, DesY) == true )
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }



                if (Board[CordX, CordY] == 'Q' && MoveItem.queenMovement(CordX, CordY, DesX, DesY) == true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                if (Board[CordX, CordY] == 'B'&& MoveItem.diagonalMove(CordX, CordY, DesX, DesY) == true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                if (Board[CordX, CordY] == 'N'&& MoveItem.knightLShape(CordX, CordY, DesX, DesY) == true && Board[DesX, DesY] == ' ' | Board[DesX, DesY] == 'p' | Board[DesX, DesY] == 'r' | Board[DesX, DesY] == 'n' | Board[DesX, DesY] == 'b' | Board[DesX, DesY] == 'q' | Board[DesX, DesY] == 'k')
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

                if (Board[CordX, CordY] == 'R' && MoveItem.moveVertical(CordX, CordY, DesX, DesY) == true | MoveItem.moveSideWay(CordX, CordY, DesX, DesY) == true)
                {


                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;

                }

 

                if (Board[CordX, CordY] == 'P'&& MoveItem.moveTwoStepsP1(CordX, CordY, DesX, DesY) == true | MoveItem.movePawnKillP1(CordX, CordY, DesX, DesY,Board) ==true)
                {
                    Board[DesX, DesY] = Board[CordX, CordY];
                    Board[CordX, CordY] = ' ';
                    return counter + 1;
                }


            }
          
                Console.WriteLine("\nNot valid move Input Correct Coordinates\n"); //if moves cannot be made
                return counter; //return the counter so it may reloop
            
        }

        //Rook coounter to count the amount of moves rook makes
        public static int rookP1MoveCounterRight(char[,] Board, int CordX, int CordY, int DesX, int DesY, int CounterP1Right)
        {
            Moves MoveItem = new Moves(); //calls moves class to check

            if (Board[CordX, CordY] == 'R' && MoveItem.moveVertical(CordX, CordY, DesX, DesY) == true | MoveItem.moveSideWay(CordX, CordY, DesX, DesY) == true)
            {
                if (CordX == 7 && CordY == 7)
                {
                    return CounterP1Right + 1; //if rook moves from its orginal position add, return a counter +1
                }
               
            }
            return CounterP1Right; //if it doesnt move then  dont add to counter
        }

        //Same code  for the rest of the Rooks

        public static int rookP1MoveCounterLeft(char[,] Board, int CordX, int CordY, int DesX, int DesY, int CounterP1Left)
        {
            Moves MoveItem = new Moves();

            if (Board[CordX, CordY] == 'R' && MoveItem.moveVertical(CordX, CordY, DesX, DesY) == true | MoveItem.moveSideWay(CordX, CordY, DesX, DesY) == true)
            {
                if (CordX == 0 && CordY == 7)
                {
                    return CounterP1Left += 1;
                }

            }

            return CounterP1Left; 
        }

        public static int rookP2MoveCounterRight(char[,] Board, int CordX, int CordY, int DesX, int DesY, int CounterP2Right)
        {
            Moves MoveItem = new Moves();

            if (Board[CordX, CordY] == 'r' && MoveItem.moveVertical(CordX, CordY, DesX, DesY) == true | MoveItem.moveSideWay(CordX, CordY, DesX, DesY) == true)
            {
                if (CordX == 7 && CordY == 0)
                {
                    return CounterP2Right += 1;
                }

            }

            return CounterP2Right;
        }

        public static int rookP2MoveCounterLeft(char[,] Board, int CordX, int CordY, int DesX, int DesY, int CounterP2Left)
        {
            Moves MoveItem = new Moves();

            if (Board[CordX, CordY] == 'r' && MoveItem.moveVertical(CordX, CordY, DesX, DesY) == true | MoveItem.moveSideWay(CordX, CordY, DesX, DesY) == true)
            {
                if (CordX == 0 && CordY == 0)
                {
                    return CounterP2Left += 1;
                }

            }

            return CounterP2Left;
        }



    }
}
