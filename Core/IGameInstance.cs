using System.Drawing;

namespace YANMFA.Core
{
    public interface IGameInstance
    {

        /**
         * By implementing this interface,
         * put the name, description and GameType of your game in here.
         * Never set these to a null reference!
         */
        string GameName { get; }
        string GameDescription { get; }
        GameMode GameType { get; }

        /**
         * Called when the game is supposed to start.
         * Here you can register all listeners.
         * 
         * Parameter:
         *      GameMode mode ->    Indicates in what mode the game was started
         *                          (Either SINGLEPLAYER or MULTIPLAYER)
         */
        void Start(GameMode mode);

        /**
         * Called when the game is supposed to stop.
         * All listeners registered in the Start() method,
         * need to be unregistered in here!
         */
        void Stop();

        /**
         * Update() method for the Splash-Screen.
         * Get's called while Start() hasn't finished running. 
         */
        void UpdateSplash();

        /**
         * Render() method for the Splash-Screen.
         * Get's called while Start() hasn't finished running. 
         */
        void RenderSplash(Graphics g);

        /**
         * Game frame update method.
         * In here goes your physics & position calculation.
         */
        void Update();

        /**
         * Game frame render method.
         * In here you can render all game components.
         * Note: You should not use this as Update() alternative!
         */
        void Render(Graphics g);

        /**
         * Determine when your game has to be closed or not.
         */
        bool IsStopRequested();

        /**
         * Determine an image to be drawn in the GameMenu.
         * Never set this to a null reference!
         * Keep in mind, that this method gets invoked every render tick.
         * Please just return an Image stored in a variable!
         */
        Image GetTitleImage();

    }

    /**
     * An enumerator indicating if your game supports
     * multiplayer, singleplayer or both
     */
    public enum GameMode
    {
        SINGLE_AND_MULTIPLAYER, // Only used in MyMainMenu
        SINGLEPLAYER,
        MULTIPLAYER
    }
}
