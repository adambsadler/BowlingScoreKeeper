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

        // Gets all frames
        public List<Frame> GetFrames()
        {
            return _frames;
        }

        public FinalFrame GetFinalFrame()
        {
            return finalFrame;
        }

        // Resets the score for all frames
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

        // Gets a frame by ID
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

        // Calculates bonus for a strike
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
                switch (frame.FrameID)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        bonus += firstBonus;
                        if (!nextFrame.IsStrike)
                        {
                            bonus += secondBonus;
                        }
                        else if (nextFrame.IsStrike)
                        {
                            bonus += otherFrame.ThrowOne;
                        }
                        break;
                    case 7:
                        if (nextFrame.IsStrike)
                        {
                            bonus += firstBonus;
                            bonus += finalFrame.ThrowOne;
                        }
                        else
                        {
                            bonus += firstBonus;
                            bonus += secondBonus;
                        }
                        break;
                    case 8:
                        bonus += finalFrame.ThrowOne;
                        bonus += finalFrame.ThrowTwo;
                        break;
                    default:
                        break;

                }
            }
            return bonus;
        }

        // Calculates bonus for a spare
        public int GetSpareBonus(int frameID)
        {
            var frame = GetFrameByID(frameID);
            var bonus = 0;
            var nextFrame = _frames[(_frames.IndexOf(frame) + 1) % _frames.Count];
            if (frame.IsSpare)
            {
                var spareBonus = nextFrame.ThrowOne;
                if (frame.FrameID == 8)
                {
                    bonus += finalFrame.ThrowOne;
                }
                else
                {
                    bonus += spareBonus;
                }
            }
            return bonus;
        }

        // Calculates total score for all frames
        public int GetTotalScore()
        {
            var totalScore = 0;
            for(int i = 0; i < 9; i++)
            {
                totalScore += GetFrameByID(i).Score;
            }
            totalScore += finalFrame.Score;
            for (int i = 0; i < 9; i++)
            {
                totalScore += GetStrikeBonus(i);
                totalScore += GetSpareBonus(i);
            }
            return totalScore;
        }
    }
}
