using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    public class FinalFrame
    {
        public int ThrowOne { get; set; }
        public int ThrowTwo { get; set; }
        public int ThrowThree { get; set; }
        public int Score {
            get
            {
                return ThrowOne + ThrowTwo + ThrowThree;
            }
                }

        public FinalFrame() { }

        public FinalFrame(int firstThrow, int secondThrow, int thirdThrow)
        {
            ThrowOne = firstThrow;
            ThrowTwo = secondThrow;
            ThrowThree = thirdThrow;
        }
    }
}
