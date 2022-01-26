using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    public class FrameRepository
    {
        private List<IFrame> _frames = new List<IFrame>();

        public void CreateFrame(IFrame frame)
        {
            _frames.Add(frame);
        }

        // Gets all frames
        public List<IFrame> GetFrames()
        {
            return _frames;
        }

        // Get a list of the first nine frames, ignoring the tenth
        public List<Frame> GetNineFrames()
        {
            var frames = new List<Frame>();
            foreach(IFrame frame in _frames)
            {
                if(frame is Frame)
                {
                    frames.Add((Frame)frame);
                }
            }
            return frames;
        }

        // Gets the tenth frame
        public FinalFrame GetFinalFrame()
        {
            return (FinalFrame)_frames[9];
        }

        // Resets the score for all frames
        public void ResetScore()
        {
            var finalFrame = GetFinalFrame();
            foreach (IFrame frame in _frames)
            {
                frame.ThrowOne = 0;
                frame.ThrowTwo = 0;
            }
            finalFrame.ThrowThree = 0;
        }

        // Gets a frame by ID
        public IFrame GetFrameByID(int frameID)
        {
            foreach(IFrame frame in _frames)
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
            var frame = (Frame)GetFrameByID(frameID);
            var frames = GetNineFrames();
            var bonus = 0;
            var nextFrame = frames[(frames.IndexOf(frame) + 1) % frames.Count];
            var otherFrame = frames[(frames.IndexOf(frame) + 2) % frames.Count];
            var finalFrame = GetFinalFrame();
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
            var frame = (Frame)GetFrameByID(frameID);
            var frames = GetNineFrames();
            var bonus = 0;
            var nextFrame = frames[(frames.IndexOf(frame) + 1) % frames.Count];
            if (frame.IsSpare)
            {
                var spareBonus = nextFrame.ThrowOne;
                if (frame.FrameID == 8)
                {
                    bonus += _frames[9].ThrowOne;
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
            totalScore += GetFinalFrame().Score;
            for (int i = 0; i < 9; i++)
            {
                totalScore += GetStrikeBonus(i);
                totalScore += GetSpareBonus(i);
            }
            return totalScore;
        }
    }
}
