using System;
using Gammer0909.WizardGame.Console.Color;
using Gammer0909.WizardGame.Console.Window;

namespace Gammer0909.WizardGame;

/// <summary>
/// The abstraction of the Wizard Game, where all the logic is handled.
/// </summary>
public class WizardGame : Game {

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="WizardGame"/>.
    /// </summary>
    /// <param name="window">The <see cref="ConsoleWindow"/> to use.</param>
    public WizardGame(ConsoleWindow window) : base(window) {}
    #endregion

    #region Methods
    /// <inheritdoc cref="Game.Start"/>
    public override void Start() {

        Window.WriteLine("Welcome to the Wizard Game!");

    }

    /// <inheritdoc cref="Game.Update"/>
    public override void Update() {



    }
    #endregion

}