namespace Gammer0909.WizardGame.Console.Color;

using System.Drawing;

/// <summary>
/// An abstraction of the 24-Bit Truecolor RGB.
/// </summary>
public class ConsoleRGB {

    #region Properties
    private byte _red;
    private byte _green;
    private byte _blue;
    private string _hex;
    private string _ANSICode;
    #endregion

    #region Static Foreground Colors
    public static ConsoleRGB BlackForeground = new ConsoleRGB(0, 0, 0, true);
    public static ConsoleRGB RedForeground = new ConsoleRGB(255, 0, 0, true);
    public static ConsoleRGB GreenForeground = new ConsoleRGB(0, 255, 0, true);
    public static ConsoleRGB YellowForeground = new ConsoleRGB(255, 255, 0, true);
    public static ConsoleRGB BlueForeground = new ConsoleRGB(0, 0, 255, true);
    public static ConsoleRGB MagentaForeground = new ConsoleRGB(255, 0, 255, true);
    public static ConsoleRGB CyanForeground = new ConsoleRGB(0, 255, 255, true);    
    public static ConsoleRGB WhiteForeground = new ConsoleRGB(255, 255, 255, true);
    #endregion

    #region Static Background Colors
    public static ConsoleRGB BlackBackground = new ConsoleRGB(0, 0, 0, false);
    public static ConsoleRGB RedBackground = new ConsoleRGB(255, 0, 0, false);
    public static ConsoleRGB GreenBackground = new ConsoleRGB(0, 255, 0, false);
    public static ConsoleRGB YellowBackground = new ConsoleRGB(255, 255, 0, false);
    public static ConsoleRGB BlueBackground = new ConsoleRGB(0, 0, 255, false);
    public static ConsoleRGB MagentaBackground = new ConsoleRGB(255, 0, 255, false);
    public static ConsoleRGB CyanBackground = new ConsoleRGB(0, 255, 255, false);
    public static ConsoleRGB WhiteBackground = new ConsoleRGB(255, 255, 255, false);
    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new instance of <see cref="ConsoleRGB"/> with the specified RGB values and whether or not it is a forground color.
    /// </summary>
    /// <param name="red">The red part of the color.</param>
    /// <param name="green">The green part of the color.</param>
    /// <param name="blue">The blue part of the color.</param>
    /// <param name="isForgroundColor">Is this color a forground (text) or a background?</param>
    public ConsoleRGB(byte red, byte green, byte blue, bool isForgroundColor) {
        this._red = red;
        this._green = green;
        this._blue = blue;
        this._hex = $"#{red:X2}{green:X2}{blue:X2}";
        if (isForgroundColor)
            this._ANSICode = $"\x1b[38;2;{this._red};{this._green};{this._blue}m";
        else
            this._ANSICode = $"\x1b[48;2;{this._red};{this._green};{this._blue}m";
    }


    public ConsoleRGB(string hex, bool isForgroundColor) {

        // Parse the hex
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);
        if (hex.Length != 6)
            throw new ArgumentException("Hex code must be 6 characters long.");
        this._hex = hex;
        this._red = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        this._green = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        this._blue = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // Parse the ANSI code
        if (isForgroundColor)
            this._ANSICode = $"\x1b[38;2;{this._red};{this._green};{this._blue}m";
        else
            this._ANSICode = $"\x1b[48;2;{this._red};{this._green};{this._blue}m";

    }
    #endregion

    #region Getters
    
    /// <summary>
    /// Gets the red part of the color.
    /// </summary>
    /// <returns>A <see cref="byte"/> that represents the red part of the color.</returns>
    public byte GetRed() => this._red;

    /// <summary>
    /// Gets the green part of the color.
    /// </summary>
    /// <returns>A <see cref="byte"/> that represents the green part of the color.</returns> 
    public byte GetGreen() => this._green;

    /// <summary>
    /// Gets the blue part of the color.
    /// </summary>
    /// <returns>A <see cref="byte"/> that represents the blue part of the color.</returns> 
    public byte GetBlue() => this._blue;

    /// <summary>
    /// Gets the hex code representation of this color.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the hex code of this color.</returns>
    public string GetHex() => this._hex;

    /// <summary>
    /// Gets the ANSI code representation of this color.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the ANSI color code of this color.</returns>
    public string GetANSICode() => this._ANSICode;
    #endregion

    #region Methods
    public ConsoleRGB FromColor(Color color) => new ConsoleRGB(color.R, color.G, color.B, true);
    #endregion

    #region Operators
    public static bool operator ==(ConsoleRGB a, ConsoleRGB b) => a._hex == b._hex;
    public static bool operator !=(ConsoleRGB a, ConsoleRGB b) => a._hex != b._hex;
    public static ConsoleRGB operator +(ConsoleRGB a, ConsoleRGB b) => new ConsoleRGB((byte)(a._red + b._red), (byte)(a._green + b._green), (byte)(a._blue + b._blue), true);
    public static ConsoleRGB operator -(ConsoleRGB a, ConsoleRGB b) => new ConsoleRGB((byte)(a._red - b._red), (byte)(a._green - b._green), (byte)(a._blue - b._blue), true);
    public static ConsoleRGB operator *(ConsoleRGB a, ConsoleRGB b) => new ConsoleRGB((byte)(a._red * b._red), (byte)(a._green * b._green), (byte)(a._blue * b._blue), true);
    public static ConsoleRGB operator /(ConsoleRGB a, ConsoleRGB b) => new ConsoleRGB((byte)(a._red / b._red), (byte)(a._green / b._green), (byte)(a._blue / b._blue), true);
    #endregion

    #region Overrides
    public override bool Equals(object? obj) {

        if (obj == null)
            return false;

        if (obj is ConsoleRGB)
            return this._hex == ((ConsoleRGB)obj)._hex;
        return false;
    }

    public override int GetHashCode() {
        
        // Ngl I have no clue what a hash code is....

        // This is a very bad hash function, but it works for now.
        return this._hex.GetHashCode();

    }
    #endregion

}