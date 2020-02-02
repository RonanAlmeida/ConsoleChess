using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAssignment
{
    class Moves
    {
        //Contains a buch of posssible movesets/test cases to test if piece can move or make a move

        //Move left/right
        public  bool moveSideWay(int InX, int InY, int NewX, int NewY) //Accepts old x, old y and new x new y
        {
            if (InX > NewX && InY == NewY || InX < NewX && InY == NewY) //If the Y's are the same, and the X's are less than or more
            {
                return true; //Moves
            }

                return false; //doesnt move 
        }

        //move Up/Down
        public bool moveVertical(int InX, int InY, int NewX, int NewY) 
        {
            if (InX == NewX && InY < NewY || InX == NewX && InY > NewY) //If the x's are same and the y's are more or less than
            {
                return true; //moves up/down
            }
                return false; //doesnt move
            
        }

        //Move One Step forward - P1 Pawn
        public bool moveOneStepPlayerOne(int InX, int InY, int NewX, int NewY) 
        {
            if (InX == NewX && InY - NewY == 1) //If P1 pawn can move up the board by one
            {
                return true; //return true
            }

                return false; //if not, return false
            

        }

        //Move forward onestep P2 - Pawn
        public bool moveOneStepPlayerTwo(int InX, int InY, int NewX, int NewY)
        {
            if (InX == NewX && NewY - InY == 1) //If P2 pawn can move down by one
            {
                return true;
            }
       
                return false;
            
        }

        //Kings Movement
        public bool moveOneStepAllDirections(int InX, int InY, int NewX, int NewY)
        {
            //If the Piece can move one up/down/left/right
            if ((InX == NewX && NewY - InY == 1) | (InX == NewX && InY - NewY == 1) | (InX - NewX == 1 && NewY == InY) | (NewX - InX == 1 && NewY == InY) | (InX - NewX == 1 && InY - NewY == 1) | (InX - NewX == 1 && NewY - InY == 1) | (NewX - InX == 1 && InY - NewY == 1) | (NewX - InX == 1 && NewY - InY == 1))
            {
                return true; //Return true
            }

            return false;

        }

        //Moving Two Steps Pawn P1
        public bool moveTwoStepsP1(int InX, int InY, int NewX, int NewY)
        {
            if (InX == NewX && InY - NewY == 2 &&InY==6 ) //If P1 pawn can move two steps at Y=6
            {
                return true; //return true
            }

            return false;
        }

        //Moving Two Steps Pawn P2
        public bool moveTwoStepsP2(int InX, int InY, int NewX, int NewY)
        {
            if (InX == NewX && NewY - InY == 2 && InY == 1) //if P2 pawn can move two steps aT y=1
            {
                return true;
            }
            return false;
        }


        //Pawn P1 killing Diagonally
        public bool movePawnKillP1(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            //If pawn P1 can kill diagonally from right or left
            if (NewX ==InX + 1 | NewX == InX - 1 && NewY == InY - 1 && Board[NewX,NewY] =='p' | Board[NewX, NewY] == 'r' | Board[NewX, NewY] == 'n' | Board[NewX, NewY] == 'b' | Board[NewX, NewY] == 'q' | Board[NewX, NewY] == 'k')
            {
                return true;
            }

            return false;
        }

        //Pawn P2 Killing Diagonally
        public bool movePawnKillP2(int InX, int InY, int NewX, int NewY, char[,] Board)
        {

            //if P2 can kill diagonally from left or right
            if (NewX == InX - 1 | NewX == InX + 1 && NewY == InY + 1 && Board[NewX, NewY] == 'P' | Board[NewX, NewY] == 'R' | Board[NewX, NewY] == 'N' | Board[NewX, NewY] == 'B' | Board[NewX, NewY] == 'Q' | Board[NewX, NewY] == 'K')
            {
                return true; 
            }
            return false;     
        }

        //Makes sure that Pawn cannot kill moving forward
        public bool movePawnP1NotKilling(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            //If P1 can kill a piece infront of it
            if(InX==NewX && NewY == InY-1 && Board[NewX, NewY] == 'p'| Board[NewX, NewY] == 'r' | Board[NewX, NewY] == 'n' | Board[NewX, NewY] == 'b' | Board[NewX, NewY] == 'q' | Board[NewX, NewY] == 'k')
            {
                return false;
            }
            return true;
        }

        //Makes sure that Pawn cannot kill moving forward
        public bool movePawnP2NotKilling(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            //If pawn can kill a peice infront of it
            if (InX == NewX && NewY == InY +1 && Board[NewX, NewY] == 'P' | Board[NewX, NewY] == 'R' | Board[NewX, NewY] == 'N' | Board[NewX, NewY] == 'B' | Board[NewX, NewY] == 'Q' | Board[NewX, NewY] == 'K')
            {
                return false; //return false, it cannot kill a piece infront of it
            }
            return true;

        }



            //L Shape knight
            public bool knightLShape(int InX, int InY, int NewX, int NewY)
            {
            //looks for each possible condition that the knight can move, L shape in differnt directions
               if ((InX - NewX == 1 && InY - NewY == 2) | (NewX - InX == 1 && InY - NewY == 2) | (NewX - InX == 1 && NewY - InY == 2) | (InX - NewX == 1 && NewY - InY == 2) | (InX - NewX == 2 && NewY - InY == 1) | (InX - NewX == 2 && InY - NewY == 1) | (NewX - InX == 2 && InY - NewY == 1) | (NewX - InX == 2 && NewY - InY == 1))
               {
                return true;
               }

                return false;

        }

        //Diagonal moving
        public bool diagonalMove(int InX, int InY, int NewX, int NewY)
        {
            
            int XDifference;
            int YDifference;

            if (InY > NewY)
            {
                YDifference = InY - NewY; //Gets differnt from orgy-newy

            }
            else
            {
                YDifference = NewY - InY; //else gets differnt from Newy-OrrgY
            }

            if (InX > NewX)
            {

                XDifference = InX - NewX; 
            }

            else
            {
                XDifference = NewX - InX;
            }

            //If vertical move is not true and horizontal move is not true and the slope is 1 then it can move diagonally
            if (moveSideWay(InX, InY, NewX, NewY) != true && moveVertical(InX, InY, NewX, NewY) != true && XDifference / YDifference == 1)
            {
                return true;
            }

                return false;

        }

        //Queens Moves
        public bool queenMovement(int InX, int InY, int NewX, int NewY)
        {
            //If vertical or Horizontal or diagonal move is true then return true
            if (moveVertical(InX, InY, NewX, NewY) == true | moveSideWay(InX, InY, NewX, NewY) == true | diagonalMove(InX, InY, NewX, NewY) == true)
            {
                return true;
            }

                return false;
        }


        //Swaping King/Rook PlayerOne's Right Side
        public bool swapKingRookPlayerOneRightSide(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            //If  P1  Right side conditions for rook swap are true (spaces and both king/rook is present)
           if(Board[6,7] == ' ' && Board[5,7] == ' ' && InX==4 && InY ==7 && NewX==6 && NewY==7&& Board[4,7]=='K' && Board[7,7] == 'R')
           {
            return true; //return true
           }
  
            return false;

        }

        //Swap  King/Rook PlayerOne's left side
        public bool swapKingRookPlayerOneLeftSide(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            if ( Board[3, 7] == ' ' && Board[2, 7] == ' ' && InX == 4 && InY == 7 && NewX == 2 && NewY == 7 && Board[4, 7] == 'K' && Board[0, 7] == 'R')
            {
                return true;
            }

            return false;


        }

        //Swap king/rook playertwo right side
        public bool swapKingRookPlayerTwoRightSide(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            if (Board[5, 0] == ' ' && Board[6, 0] == ' ' && InX == 4 && InY == 0 && NewX == 6 && NewY == 0 && Board[4, 0] == 'k' && Board[7, 0] == 'r')
            {
                return true;
            }

            return false;


        }

        //Swap king/rook playerTwo's left side
        public bool swapKingRookPlayerTwoLeftSide(int InX, int InY, int NewX, int NewY, char[,] Board)
        {
            if (Board[3, 0] == ' ' && Board[2, 0] == ' ' && InX == 4 && InY == 0 && NewX == 2 && NewY == 0 && Board[4, 0] == 'k' && Board[0, 0] == 'r')
            {
                return true;
            }

            return false;


        }

    }
}
