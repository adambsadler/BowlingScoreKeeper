using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    public class FrameRepository
    {
        private List<Frame> _frames = new List<Frame>();
        private FinalFrame finalFrame = new FinalFrame(0, 0, 0);

        public void CreateFrame(Frame frame)
        {
            _frames.Add(frame);
        }

        public List<Frame> GetFrames()
        {
            return _frames;
        }

        public void ResetScore()
        {
            foreach (Frame frame in _frames)
            {
                frame.ThrowOne = 0;
                frame.ThrowTwo = 0;
            }
            finalFrame.ThrowOne = 0;
            finalFrame.ThrowTwo = 0;
            finalFrame.ThrowThree = 0;
        }

        public Frame GetFrameByID(int frameID)
        {
            foreach(Frame frame in _frames)
            {
                if(frame.FrameID == frameID)
                {
                    return frame;
                }
            }
            return null;
        }

        public void Roll(int frameID, int firstThrow, int secondThrow)
        {
            var frame = GetFrameByID(frameID);
            frame.ThrowOne = firstThrow;
            if(firstThrow == 10)
            {
                frame.IsStrike = true;
            }
            frame.ThrowTwo = secondThrow;
            if(!frame.IsStrike)
            {
                frame.ThrowTwo = secondThrow;
                if(secondThrow + firstThrow == 10)
                {
                    frame.IsSpare = true;
                }
            }
        }

        public void FinalRoll(int firstThrow, int secondThrow, int thirdThrow)
        {
            finalFrame.ThrowOne = firstThrow;
            finalFrame.ThrowTwo = secondThrow;
            finalFrame.ThrowThree = thirdThrow;
        }

        public int GetScoreByFrame(int frameID)
        {
            var frame = GetFrameByID(frameID);
            var score = 0;
            score += frame.ThrowOne;
            if(frame.IsStrike)
            {
                score += GetStrikeBonus(frame.FrameID);
            }
            score += frame.ThrowTwo;
            if(frame.IsSpare)
            {
                score += GetSpareBonus(frame.FrameID);
            }
            return score;
        }

        public int GetFinalFrameScore()
        {
            var score = 0;
            score += finalFrame.ThrowOne;
            score += finalFrame.ThrowTwo;
            if(finalFrame.ThrowOne == 10)
            {
                score += finalFrame.ThrowTwo;
                score += finalFrame.ThrowThree;
            }
            score += finalFrame.ThrowThree;
            if(finalFrame.ThrowTwo == 10)
            {
                score += finalFrame.ThrowThree;
            }
            return score;
        }

        public int GetStrikeBonus(int frameID)
        {
            var frame = GetFrameByID(frameID);
            var bonus = 0;
            var nextFrame = _frames[(_frames.IndexOf(frame) + 1) % _frames.Count];
            var otherFrame = _frames[(_frames.IndexOf(frame) + 2) % _frames.Count];
            if (frame.IsStrike)
            {
                var firstBonus = nextFrame.ThrowOne;
                var secondBonus = nextFrame.ThrowTwo;
                bonus += firstBonus;
                if (!nextFrame.IsStrike)
                {
                    bonus += secondBonus;
                }
                else if (nextFrame.IsStrike)
                {
                    bonus += otherFrame.ThrowOne;
                }
            }
            return bonus;
        }

        public int GetSpareBonus(int frameID)
        {
            var frame = GetFrameByID(frameID);
            var bonus = 0;
            var nextFrame = _frames[(_frames.IndexOf(frame) + 1) % _frames.Count];
            if (frame.IsSpare)
            {
                var spareBonus = nextFrame.ThrowOne;
                bonus += spareBonus;
            }
            return bonus;
        }

        public int GetTotalScore()
        {
            var totalScore = 0;
            for(int i = 0; i < 8; i++)
            {
                totalScore += GetScoreByFrame(i);
            }
            totalScore += GetFinalFrameScore();
            return totalScore;
        }
    }
}
