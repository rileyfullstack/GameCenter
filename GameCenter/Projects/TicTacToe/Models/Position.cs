using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter.Projects.TicTacToe.Utils
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }
        
        public Position(int xValue, int yValue)
        {
            x = xValue;
            y = yValue;
        }
    }
}
