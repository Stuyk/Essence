using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.classes.minigames
{
    public class MinigameInfo
    {
        int score;
        MinigameType type;

        /// <summary>
        /// Types of Minigames.
        /// </summary>
        public enum MinigameType
        {
            Lockpick,
            none
        }

        /// <summary>
        /// Setup a new minigame, remember to assign it to the player with API.setEntityData(player, "Minigame", classInstanceHere);
        /// </summary>
        public MinigameInfo()
        {
            score = 0;
            type = MinigameType.none;
        }

        /// <summary>
        /// What type of minigame is this?
        /// </summary>
        public MinigameType Type
        {
            set
            {
                type = value;
            }
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Adjust the score in this minigame. Over 100 will end the minigame.
        /// </summary>
        public int Score
        {
            set
            {
                score = value;
            }
            get
            {
                return score;
            }
        }
    }
}
