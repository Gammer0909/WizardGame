using System;
using Gammer0909.WizardGame.Console.Color;
using Gammer0909.WizardGame.Console.Formatting;
using Gammer0909.WizardGame.Console.Window;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Casting.Spells;
using Gammer0909.WizardGame.Managers;

namespace Gammer0909.WizardGame;

/// <summary>
/// The abstraction of the Wizard Game, where all the logic is handled.
/// </summary>
public class WizardGame : Game {

    #region Properties
    private GameManager gm { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="WizardGame"/>.
    /// </summary>
    /// <param name="window">The <see cref="ConsoleWindow"/> to use.</param>
    public WizardGame(ConsoleWindow window) : base(window) {
        this.gm = new GameManager();
    }
    #endregion

    #region Methods
    /// <inheritdoc cref="Game.Start"/>
    public override void Start() {

        // Player Setup
        this.Window.WriteLine("Welcome to Gammer0909's Wizard Game!");
        this.Window.WriteLine("Please enter your adventurer's name: ");
        var playerName = this.GetPlayerName();


        var player = new Player(playerName, 10, 100);

        this.Window.WriteLine($"Welcome, {player.Name}!");
        this.Window.WriteLine("Press any key to view the rules.");

        // Rules
        this.PrintRules();

        // First spell
        this.Window.WriteLine("Before we get started, let's pick your first spell!");

        // Setup store
        var store = new SpellShop();

        store.AddSpell(new Fireball(), 15);
        store.AddSpell(new Heal(), 30);
        store.AddSpell(new MageArmor(), 10);

        this.gm = new GameManager(player, store, new EntityManager());

        this.gm.EntityManager.CreateEntities(10);
        
        


        // Game Loop


    }

    private void BuySpell() {

        this.Window.WriteLine("Which Spell would you like to buy?");
        this.Window.WriteLine(this.gm.SpellShop.ToString());

        var spellName = this.Window.inputHandler.ReadLine();
        spellName = spellName.ToLower();

        if (spellName == "quit" || spellName == "q") {
            
            return;

        }

        // Check if the spell exists
        ISpell spellToBuy = new NullSpell();

        foreach (var spell in this.gm.SpellShop.Spells) {
            if (spell.Key.Name.ToLower() == spellName) {
                spellToBuy = spell.Key;
                break;
            }
        }
        
        try {
            this.gm.BuySpell(spellToBuy);
        } catch (NullReferenceException) {
            this.Window.WriteLine("That spell doesn't exist!");
            this.BuySpell();
        } catch (NotEnoughException) {
            this.Window.WriteLine("You don't have enough gold to buy that spell!");
            this.BuySpell();
        }

    }

    private void PrintRules() {

        this.Window.SetFormat(ConsoleFormat.Bold);
        this.Window.ForegroundColor = ConsoleRGB.YellowForeground;
        this.Window.WriteLine("--- RULES ---");
        this.Window.WriteLine("---=======---\nYou are a wizard. Fight monsters as they come, and use the gold they drop to buy more spells.\nGet 1000 gold to win.\n---=======---");
        this.Window.WriteLine("\nWhen prompted, type \"help\" to get a list of commands, and bring up this menu.\n---=======---");
        this.Window.WriteLine("--- COMMANDS ---");
        this.Window.WriteLine("---=======---\nhelp - Brings up this menu.\n---=======---");
        this.Window.WriteLine("attack - Attacks the monster you are currently fighting.\n---=======---");
        this.Window.WriteLine("spells - Brings up the spell-buying menu\n---=======---");
        this.Window.WriteLine("quit - Quits the game.\n---=======---");
        this.Window.WriteLine("\n\nPress any key to close this menu.");
        this.Window.inputHandler.ReadKey();
        this.Window.ResetColor();
        this.Window.ClearFormat();
        this.Window.Clear();

    }

    private string GetPlayerName() {
        this.Window.WriteLine("Please enter your adventurer's name: ");
        var ret =  this.Window.inputHandler.ReadLine();

        // Null check
        if (ret == "" || ret == null) {
            this.Window.WriteLine("Please enter a valid name.");
            return this.GetPlayerName();
        }

        return ret;

    }


    /// <inheritdoc cref="Game.Update"/>
    public override void Update() {



    }
    #endregion

}