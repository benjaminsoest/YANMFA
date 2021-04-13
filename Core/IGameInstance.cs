using System.Drawing;

namespace YANMFA.Core
{
    public interface IGameInstance
    {

        /**
         * By implementing this interface,
         * put the name and description of your game in here.
         * Never set these to a null reference!
         */
        string GameName { get; }
        string GameDescription { get; }

        /**
         * Called when the game is supposed to start.
         * Here you can register all listeners.
         */
        void Start();

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
         */
        Image GetTitleImage();

    }
}
