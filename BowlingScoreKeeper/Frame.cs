using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    public interface IFrame
    {
        public int FrameID { get; set; }
        public string Name { get; set; }
        public int ThrowOne { get; set; }
        public int ThrowTwo { get; set; }
        public int Score { get; }
        public void Roll(int firstThrow, int secondThrow);
    }
    public class Frame : IFrame
    {
        public int FrameID { get; set; }
        public string Name { get; set; }
        public int ThrowOne { get; set; }
        public int ThrowTwo { get; set; }
        public bool IsStrike { get; set; }
        public bool IsSpare { get; set; }
        public int Score { 
            get
            {
                return ThrowOne + ThrowTwo;
            }
                }

        public Frame() { }

        public Frame(int id, string name, int firstThrow, int secondThrow)
        {
            FrameID = id;
            Name = name;
            ThrowOne = firstThrow;
            ThrowTwo = secondThrow;
        }

        public void Roll(int firstThrow, int secondThrow)
        {
            ThrowOne = firstThrow;
            ThrowTwo = secondThrow;
            if (firstThrow == 10)
            {
                IsStrike = true;
            }
            if (!IsStrike)
            {
                if ((secondThrow + firstThrow) == 10)
                {
                    IsSpare = true;
                }
            }
        }
    }

    public class FinalFrame : IFrame
    {
        public int FrameID { get; set; }
        public string Name { get; set; }
        public int ThrowOne { get; set; }
        public int ThrowTwo { get; set; }
        public int ThrowThree { get; set; }
        public int Score
        {
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

        public void Roll(int firstThrow, int secondThrow)
        {
            ThrowOne = firstThrow;
            ThrowTwo = secondThrow;
        }

        public void FinalRoll(int thirdThrow)
        {
            ThrowThree = thirdThrow;
        }
    }
}
