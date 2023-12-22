using Gammer0909.WizardGame.Console.Color;
using Gammer0909.WizardGame.Console.Window;

namespace Gammer0909.WizardGame;

public class Program {
    public static void Main(string[] args) {

        var window = new ConsoleWindow("Wizard Game", ConsoleRGB.WhiteForeground, ConsoleRGB.BlackBackground);

        var game = new WizardGame(window);

        game.Start();

    }
}