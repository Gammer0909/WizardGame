namespace Gammer0909.WizardGame.Console.Window;

/// <summary>
/// A class that handles input from the Console.
/// </summary>
public class InputHandler {

    #region Constructors
    /// <summary>
    /// Creates a new instance of an <see cref="InputHandler"/>.
    /// </summary>
    public InputHandler() {}   
    #endregion

    #region Methods
    /// <inheritdoc cref="System.Console.ReadKey()"/>
    public System.ConsoleKeyInfo ReadKey() {
        return System.Console.ReadKey();
    }

    /// <inheritdoc cref="System.Console.ReadLine()"/>
    public string? ReadLine() {
        return System.Console.ReadLine();
    }
    #endregion

}