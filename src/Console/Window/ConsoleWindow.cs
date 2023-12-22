namespace Gammer0909.WizardGame.Console.Window;

using Gammer0909.WizardGame.Console.Color;
using Gammer0909.WizardGame.Console.Formatting;

/// <summary>
/// An abstraction of a Console Window.
/// </summary>
public class ConsoleWindow {

    #region Properties
    private string _title;
    private int _width;
    private int _height;
    public readonly InputHandler inputHandler = new InputHandler();
    public ConsoleRGB BackgroundColor { get; set; } = ConsoleRGB.BlackBackground;
    public ConsoleRGB ForegroundColor { get; set; } = ConsoleRGB.WhiteForeground;
    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of <see cref="ConsoleWindow"/> with the specified title, width, and height.
    /// </summary>
    /// <param name="title">The title of the Console Window</param>
    /// <param name="width">The width of the Console Window</param>
    /// <param name="height">The height of the Console Window</param>
    /// <exception cref="System.PlatformNotSupportedException">Thrown when the current platform is not supported.</exception>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public ConsoleWindow(string title, int width, int height) {
        this._title = title;
        this._width = width;
        this._height = height;

        // WAIT
        // Make sure we're on Windows
        if (System.Environment.OSVersion.Platform != System.PlatformID.Win32NT)
            throw new System.PlatformNotSupportedException("The current platform is not supported.");

        // Set window title
        System.Console.Title = this._title;

        // Set window size
        System.Console.BufferWidth = this._width;
        System.Console.BufferHeight = this._height;
        System.Console.SetWindowSize(this._width, this._height);

        // Set the foreground and background
        System.Console.Write($"{this.BackgroundColor.GetANSICode()}{this.ForegroundColor.GetANSICode()}");
    }

    /// <summary>
    /// Creates a new instance of <see cref="ConsoleWindow"/> with the specified title, width, height, background color, and foreground color.
    /// </summary>
    /// <param name="title">The title of the Console Window</param>
    /// <param name="width">The width of the Console Window</param>
    /// <param name="height">The height of the Console Window</param>
    /// <param name="backgroundColor">The background color of the Console Window</param>
    /// <param name="foregroundColor">The foreground color of the Console Window</param>
    /// <exception cref="System.PlatformNotSupportedException">Thrown when the current platform is not supported.</exception>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public ConsoleWindow(string title, int width, int height, ConsoleRGB backgroundColor, ConsoleRGB foregroundColor) {
        this._title = title;
        this._width = width;
        this._height = height;
        this.BackgroundColor = backgroundColor;
        this.ForegroundColor = foregroundColor;

        // WAIT
        // Make sure we're on Windows
        if (System.Environment.OSVersion.Platform != System.PlatformID.Win32NT)
            throw new System.PlatformNotSupportedException("The current platform is not supported.");

        // Set window title
        System.Console.Title = this._title;

        // Set window size
        System.Console.BufferWidth = this._width;
        System.Console.BufferHeight = this._height;
        System.Console.SetWindowSize(this._width, this._height);

        // Set the foreground and background
        System.Console.Write($"{this.BackgroundColor.GetANSICode()}{this.ForegroundColor.GetANSICode()}");


    }

    /// <summary>
    /// Creates a new instance of <see cref="ConsoleWindow"/> with the specified title.
    /// </summary>
    /// <param name="title">The title of the Console Window</param>
    /// <param name="foregroundColor">The background color of the Console Window</param>
    /// <param name="backgroundColor">The foreground color of the Console Window</param>
    public ConsoleWindow(string title, ConsoleRGB foregroundColor, ConsoleRGB backgroundColor) {
        this._title = title;
        this._width = System.Console.BufferWidth;
        this._height = System.Console.BufferHeight;
        this.BackgroundColor = backgroundColor;
        this.ForegroundColor = foregroundColor;

        // Set window title
        System.Console.Title = this._title;

        // Window Size isn't supported by Linux and MacOS

        // Set the foreground and background
        System.Console.Write($"{this.BackgroundColor.GetANSICode()}{this.ForegroundColor.GetANSICode()}");

    }
    #endregion

    #region Getters
    /// <summary>
    /// Gets the title of the Console Window.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the title of the Console Window.</returns>
    public string GetTitle() => this._title;

    /// <summary>
    /// Gets the width of the Console Window.
    /// </summary>
    /// <returns>An <see cref="int"/> that represents the width of the Console Window.</returns>
    public int GetWidth() => this._width;

    /// <summary>
    /// Gets the height of the Console Window.
    /// </summary>
    /// <returns>An <see cref="int"/> that represents the height of the Console Window.</returns>
    public int GetHeight() => this._height;
    #endregion

    #region Setters
    /// <summary>
    /// Sets the title of the Console Window.
    /// </summary>
    /// <param name="title">The new title of the Console Window.</param>
    public void SetTitle(string title) {
        this._title = title;
        System.Console.Title = this._title;
    }

    /// <summary>
    /// Sets the width and height of the Console Window.
    /// </summary>
    /// <param name="width">The new width of the Console Window.</param>
    /// <param name="height">The new height of the Console Window.</param>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public void SetSize(int width, int height) {
        this._width = width;
        this._height = height;
        System.Console.BufferWidth = this._width;
        System.Console.BufferHeight = this._height;
        System.Console.SetWindowSize(this._width, this._height);
    }
    #endregion

    #region Methods

        #region Wrappers
        /// <inheritdoc cref="System.Console.Write(object?)"/>
        public void Write(object? o) => System.Console.Write(o);

        /// <inheritdoc cref="System.Console.WriteLine(object?)"/>
        public void WriteLine(object? o) => System.Console.WriteLine(o);

        /// <inheritdoc cref="System.Console.Write(string)"/>
        public void Write(string s) => System.Console.Write(s);

        /// <inheritdoc cref="System.Console.WriteLine(string)"/>
        public void WriteLine(string s) => System.Console.WriteLine(s);
        #endregion

    /// <summary>
    /// Clears the Console Window.
    /// </summary>
    /// <param name="clearColors">Should the colors be cleared?</param>
    public void Clear(bool clearColors = false) {
        if (clearColors)
            System.Console.Write($"{ConsoleRGB.BlackBackground.GetANSICode()}{ConsoleRGB.WhiteForeground.GetANSICode()}");
        System.Console.Clear();
    }

    /// <summary>
    /// Resets the colors of the Console Window.
    /// </summary>
    public void ResetColor() {
        System.Console.Write($"{ConsoleRGB.BlackBackground.GetANSICode()}{ConsoleRGB.WhiteForeground.GetANSICode()}");
    }

    /// <summary>
    /// Sets the formatting of the text on the Console Window.
    /// </summary>
    /// <param name="format">The format to set the Console Window to.</param>
    /// <remarks>You may set more than one formatting option by simply calling this method multiple times.</remarks> 
    public void SetFormat(ConsoleFormat format) {
        System.Console.Write(format.GetANSICode());
    }

    /// <summary>
    /// Clears the formatting of the text on the Console Window.
    /// </summary>
    public void ClearFormat() {
        System.Console.Write(ConsoleFormat.Reset.GetANSICode());
    }
    #endregion

    #region Overrides
    public override string ToString() => $"ConsoleWindow[Title={this._title}, Width={this._width}, Height={this._height}, BackgroundColor={this.BackgroundColor}, ForegroundColor={this.ForegroundColor}]";
    #endregion

}