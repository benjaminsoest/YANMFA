using YANMFA.Games.Paolo.MyGameMenu;
using YANMFA.Games.Lars.Snake;
using System;
using System.Collections.Generic;
using System.Threading;

namespace YANMFA.Core
{
    /**
     * You don't have to bother with this. (Only used in GameEngine & GameMenu!)
     */
    public class StaticEngine
    {

        public static readonly List<IGameInstance> Games = new List<IGameInstance>();

        public static IGameInstance CurrentGame { get; private set; } // Is either the MainMenu or a running game, but never null!
        public static bool IsGameRunning { get; private set; } // True if player is ingame, false otherwise

        static StaticEngine()
        {
            Games.Add(new GameMenu()); // Note: This has to be added as first element, since ChangeGame(null) runs the first element
            { // TODO: Add Games here
                Games.Add(new SnakeControl());
            }
            ChangeGame(null); // Change game to GameMenu
        }

        private StaticEngine() { }

        /**
         * You don't have to bother with this. (Only used in GameEngine & GameMenu!)
         * Stops the previous game and starts a new one in a new thread.
         */
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void ChangeGame(IGameInstance game)
        {
            IGameInstance tmpGame = CurrentGame;
            CurrentGame = game ?? Games[0];
            IsGameRunning = false;

            new Thread(new ThreadStart(() =>
            {
                tmpGame?.Stop();
                CurrentGame?.Start();
                IsGameRunning = true;
            })).Start();
        }

    }
}
