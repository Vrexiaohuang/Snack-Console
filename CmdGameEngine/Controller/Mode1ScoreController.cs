using CmdGameEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdGameEngine.Controller
{
    class Mode1ScoreController
    {
        public Text scoreText;

        int score = 0;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                if (score < 0) score = 0;
                scoreText.TextValue = score.ToString();
            }
        }
    }
}
