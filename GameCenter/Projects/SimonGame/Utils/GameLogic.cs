using GameCenter.Projects.SimonGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter.Projects.SimonGame.Utils
{
    public class GameLogic
    {
        private List<ButtonColor> sequence;
        private int currentInputIndex;

        public GameLogic()
        {
            sequence = new List<ButtonColor>();
            NewRound();
        }

        public void NewRound()
        {
            sequence.Add((ButtonColor)new Random().Next(0, 4));
            currentInputIndex = 0;
        }

        public List<ButtonColor> GetSequence()
        {
            return sequence;
        }

        public CheckResult CheckInput(ButtonColor input)
        {
            if (input == sequence[currentInputIndex])
            {
                currentInputIndex++;
                if (currentInputIndex == sequence.Count)
                {
                    return CheckResult.CompletedSequence; // Completed the sequence correctly
                }
                return CheckResult.Correct; // Correct button, but still more to go
            }
            else
            {
                return CheckResult.Incorrect; // Wrong button pressed
            }
        }

        public bool IsEndOfSequence()
        {
            return currentInputIndex == sequence.Count;
        }

        public void Reset()
        {
            sequence.Clear();
            NewRound();
        }
        public void ResetCurrentInputIndex()
        {
            currentInputIndex = 0;
        }
        public int GetScore()
        {
            return sequence.Count - 1;
        }
    }

}