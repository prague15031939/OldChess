using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessServer
{
    public enum GameStatus
    {
        wait, AcceptOne, inProgress, destroyed
    }

    class GameSession
    {
        public User PlayerWhite { get; set; }
        public User PlayerBlack { get; set; }
        public GameStatus status { get; set; }
        public string CurrentFen { get; set; }

        public GameSession(User player, string side)
        {
            this.status = GameStatus.wait;
            if (side == "white") { PlayerWhite = player; PlayerBlack = null; }
            else { PlayerBlack = player; PlayerWhite = null; }
        }

        public User GetActivePlayer()
        {
            if (status == GameStatus.wait)
            {
                if (PlayerWhite == null) 
                    return PlayerBlack; 
                else
                    return PlayerWhite;
            }
            return null;
        }

        public void SetSecondPlayer(User joiner)
        {
            if (status == GameStatus.wait)
            {
                if (PlayerWhite == null)
                    PlayerWhite = joiner;
                else
                    PlayerBlack = joiner;
            }
        }

        public string GetInactiveSide()
        {
            if (status == GameStatus.wait)
            {
                if (PlayerWhite == null)
                    return "white";
                else
                    return "black";
            }
            return null;
        }

        public User GetOpponent(User user)
        {
            if (user.side == "white") return PlayerBlack;
            else return PlayerWhite;
        }
    }
}
