namespace Gammer0909.WizardGame;

using Gammer0909.WizardGame.Console.Window;

public abstract class Game {

    #region Properties
    public ConsoleWindow Window { get; set; }
    #endregion

    #region Constructors
    public Game(ConsoleWindow window) {
        this.Window = window;
    }
    #endregion

    #region Methods
    public abstract void Start();
    public abstract void Update();
    #endregion

}