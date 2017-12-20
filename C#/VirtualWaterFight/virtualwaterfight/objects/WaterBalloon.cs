using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class WaterBalloon
    {
        private int balloonID;
        public int BalloonID
        {
            get { return balloonID; }
            set { }
        }

        public enum PossibleSize
        {
            XSmall = 1,
            Small = 2,
            Medium = 3,
            Large = 4,
            XLarge = 5
        }
        public PossibleSize Size { get; set; }

        public enum PossibleColor
        {
            Red = 1,
            Blue = 2,
            Green = 3,
            White = 4,
            Black = 5,
            Purpule = 6,
            Orange = 7,
            Gray = 8,
            Brown = 9,
            Yellow = 10
        }
        public PossibleColor Color { get; set; }
        public Int16 PercentFilled { get; set; }
        public Int16 AmountOfWater { get; set; }

        //For Fight Manager and Water Manager
        public WaterBalloon(int balloonId)
        {
            balloonID = balloonId;
        }

        //For Player
        public WaterBalloon(PossibleSize size, PossibleColor color)
        {
            Size = size;
            Color = color;
            PercentFilled = 0;
        }

        //For Balloon Manager
        public WaterBalloon(int id, PossibleSize size, PossibleColor color)
        {
            balloonID = id;
            Size = size;
            Color = color;
            PercentFilled = 0;
        }

        public Int16 IncreasePercentFilled(Int16 percentFilled)
        {
            PercentFilled += percentFilled;
            CalcAmountOfWater();
            return PercentFilled;
        }

        public void SetID(int id)
        {
            balloonID = id;
        }

        public Int16 CalcAmountOfWater()
        {
            switch (Size)
            {
                case PossibleSize.XLarge:
                    AmountOfWater = (Int16)(120 * PercentFilled / 100);
                    break; 
                case PossibleSize.Large:
                    AmountOfWater = (Int16)(80 * PercentFilled / 100);
                    break;
                case PossibleSize.Medium:
                    AmountOfWater = (Int16)(60 * PercentFilled / 100);
                    break;
                case PossibleSize.Small:
                    AmountOfWater = (Int16)(40 * PercentFilled / 100);
                    break;
                case PossibleSize.XSmall:
                    AmountOfWater = (Int16)(20 * PercentFilled / 100);
                    break;
                default:
                    AmountOfWater = 0;
                    break;
            }
            return AmountOfWater;

        }
            
    }
}
