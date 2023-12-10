using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter.Projects.TicTacToe.Utils
{
    public class GameLogic
    {
        public string CurrentPlayer { get; set; } = "X"; //Starts the player as X, because a tictactoe game always starts with the player being X.
        private string[,] Board = new string[3,3]; 
        public void SetNextPlayer()
        {
            if (CurrentPlayer == "X")
            {
                CurrentPlayer = "O";
            }
            else CurrentPlayer = "X";
        }

        public bool PlayerWin()
        {
            if (WinCheck('V') || WinCheck('H') || WinCheckCross())
            {
                return true;
            }
            return false;
        }

        internal void UpdateBoard(Position position, string value)
        {
            Board[position.x, position.y] = value;
        }

        public bool WinCheck(char winCondition)
        {
            int checkWin = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (winCondition == 'H')
                    {
                        if (CurrentPlayer == Board[i, j]) //Checks for horizontal win status
                        {
                            checkWin++;
                        }
                    }
                    else if (winCondition == 'V')
                    {
                        if (CurrentPlayer == Board[j, i]) //Checks for vertical win status
                        {
                            checkWin++;
                        }
                    }
                    if (checkWin == 3)
                    {
                        return true;
                    }
                }
                checkWin = 0;
            }
            return false;
        }
        public bool WinCheckCross()
        {
            if(
                (Board[0,0] == CurrentPlayer && Board[1,1] == CurrentPlayer && Board[2,2] == CurrentPlayer) ||
                (Board[2, 0] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[0, 2] == CurrentPlayer)
                )
            {
                return true;
            }
            return false;
        }
    }
}
