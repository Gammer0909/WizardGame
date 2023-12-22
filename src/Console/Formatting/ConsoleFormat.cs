namespace Gammer0909.WizardGame.Console.Formatting;

/// <summary>
/// An abstraction of a Console Format.
/// </summary>
public class ConsoleFormat {

    #region Properties
    private string _ANSICode;
    #endregion

    #region Static Formats
    public static ConsoleFormat Reset = new ConsoleFormat("\x1b[0m");
    public static ConsoleFormat Bold = new ConsoleFormat("\x1b[1m");
    public static ConsoleFormat Dim = new ConsoleFormat("\x1b[2m");
    public static ConsoleFormat Italic = new ConsoleFormat("\x1b[3m");
    public static ConsoleFormat Underline = new ConsoleFormat("\x1b[4m");
    public static ConsoleFormat Blink = new ConsoleFormat("\x1b[5m");
    public static ConsoleFormat Reverse = new ConsoleFormat("\x1b[7m");
    public static ConsoleFormat Hidden = new ConsoleFormat("\x1b[8m");
    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of <see cref="ConsoleFormat"/> with the specified ANSI code.
    /// </summary>
    /// <param name="ANSICode">The ANSI code of the Console Format.</param>
    public ConsoleFormat(string ANSICode) {
        this._ANSICode = ANSICode;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the ANSI code of the Console Format.
    /// </summary>
    /// <returns>The ANSI code of the Console Format.</returns>
    public string GetANSICode() {
        return this._ANSICode;
    }

    #endregion

}