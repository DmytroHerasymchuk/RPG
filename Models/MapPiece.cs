using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MapPiece
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string ImageName { get; set; }
        public bool Status { get; set; }

        public MapPiece(int xCoordinate, int yCoordinate, string imageName, bool status = false)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            ImageName = imageName;
            Status = status;
        }
    }
}
