using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    public class Frame
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
    }
}
