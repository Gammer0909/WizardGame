using System;
using System.IO;
using Newtonsoft.Json;
using Gammer0909.WizardGame.Console.Color;
using Gammer0909.WizardGame.Console.Formatting;
using Gammer0909.WizardGame.Console.Window;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Casting.Spells;
using Gammer0909.WizardGame.Managers;
using Gammer0909.WizardGame.Dice;

namespace Gammer0909.WizardGame;

/// <summary>
/// The abstraction of the Wizard Game, where all the logic is handled.
/// </summary>
public class WizardGame : Game {

    #region Properties
    private GameManager gm { get; set; }

    private Dictionary<string, string> commands;
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="WizardGame"/>.
    /// </summary>
    /// <param name="window">The <see cref="ConsoleWindow"/> to use.</param>
    public WizardGame(ConsoleWindow window) : base(window) {
        this.gm = new GameManager();
        this.commands = new Dictionary<string, string>();
        this.commands.Add("help", "Brings up the help menu.");
        this.commands.Add("spells", "Brings up the spell-buying menu.");
        this.commands.Add("quit", "Quits the game.");
        this.commands.Add("cast", "Brings up the spellcasting menu.");
    }
    #endregion

    #region Methods
    /// <inheritdoc cref="Game.Start"/>
    public override void Start() {

        this.Window.Clear();

        // Player setup
        var game = this.Setup();

        this.gm = game;

        this.Window.WriteLine($"Welcome, {this.gm.Player.Name}!");
        this.Window.WriteLine("Press any key to view the rules.");
        this.Window.inputHandler.ReadKey();

        this.Window.Clear();

        // Rules
        this.PrintRules();

        // First spell
        this.Window.WriteLine("Before we get started, let's pick your first spell!");

        this.gm.EntityManager.CreateEntities(10);
        
        this.BuySpell();

        // Game Loop
        this.Window.WriteLine("Press any key to start the game!");
        this.Window.inputHandler.ReadKey();

        // Create the monsters
        this.gm.EntityManager.CreateEntities(5);
        // Dequeue the NullEntity
        this.gm.EntityManager.GetEntity();
        this.Window.Clear();
        this.Update();


    }

    /// <inheritdoc cref="Game.Update"/>
    public override void Update() {

        int amountOfPrints = 0;

        while (gm.IsRunning) {

            if (amountOfPrints >= 5) {
                this.Window.Clear();
                amountOfPrints = 0;
            }

            string command = this.Prompt();

            // Parse the command
            this.RunCommand(command);

            // TODO: Enemy turn

            // I'm doing Player -> Enemy -> Gold for this reason
            // If the player dies, the game ends, so that should go first
            // If the enemy dies, the player gets gold, so that should go second
            // If the player gets enough gold, the game ends, so that should go third, because the second step gives gold.

            // Check that the player is still alive
            if (this.gm.Player.Health <= 0) {
                // TODO: Make this more fancy
                this.HandleEnd(true);
                break;
            }

            // Check if the enemy is dead
            if (this.gm.EntityManager.GetCurrentEntity().Health <= 0) {
                
                this.Window.WriteLine($"You killed the {this.gm.EntityManager.GetCurrentEntity().Name}!");
                // Gold
                this.gm.Player.AddGold(DiceRoll.Roll(this.gm.EntityManager.GetCurrentEntity().goldDice));
                this.Window.WriteLine($"You have {this.gm.Player.GetGold()} gold.");
                this.gm.EntityManager.GetEntity();
                this.Window.WriteLine($"The next monster is a {this.gm.EntityManager.GetCurrentEntity().Name}!");
            }

            // This after death check so we don't get "The Entity has -1 health remaining!"
            this.Window.WriteLine($"The {this.gm.EntityManager.GetCurrentEntity().Name} has {this.gm.EntityManager.GetCurrentEntity().Health} health remaining!");
            this.Window.WriteLine($"You have {this.gm.Player.GetGold()} gold.");
            this.Window.WriteLine($"You have {this.gm.Player.Health} health remaining!\n\n");

            // Check if the player has won
            if (this.gm.Player.GetGold() >= 1000) {
                // TODO: Make this more fancy
                this.HandleEnd(false);
                break;
            }            

            amountOfPrints++;

        }

    }

    private GameManager Setup() {

        this.Window.WriteLine("Welcome to the Wizard Game!");

        this.Window.WriteLine("Do you have a save file? (y/n)");

        var hasSave = this.Window.inputHandler.ReadLine();

        if (hasSave == "y" || hasSave == "yes" || hasSave == "Y" || hasSave == "Yes") {

            // Let's look and see if the saves actually exist
            if (!Directory.Exists("/save_slots")) {
                this.Window.WriteLine("You don't have any save files!");
                this.Window.WriteLine("Press any key to continue.");
                this.Window.inputHandler.ReadKey();
                this.Window.Clear();
                return this.Setup();
            }

            // Get the save files
            var saveFiles = Directory.GetFiles("/save_slots");

            // Print the save files
            foreach (var file in saveFiles) {
                this.Window.WriteLine(file);
            }

            // Get the save file
            this.Window.Write("Which save file would you like to load? (1-5): ");
            var saveFile = this.Window.inputHandler.ReadLine();

            // Check if the save file exists
            if (!File.Exists($"/save_slots/save_slot_number_{saveFile}.json")) {
                this.Window.WriteLine("That save file doesn't exist!");
                this.Window.WriteLine("Press any key to continue.");
                this.Window.inputHandler.ReadKey();
                this.Window.Clear();
                return this.Setup();
            }

            // Load the save file
            var json = File.ReadAllText($"/save_slots/save_slot_number_{saveFile}.json");

            // Make sure the file actualy lived
            if (json == null || json == "") {
                this.Window.WriteLine("That save file doesn't exist!");
                this.Window.WriteLine("Press any key to continue.");
                this.Window.inputHandler.ReadKey();
                this.Window.Clear();
                return this.Setup();
            }

            // Deserialize the save file
            var save = JsonConvert.DeserializeObject<SaveFile>(json);

            // Make sure the save file is valid
            if (save == null) {
                this.Window.WriteLine("That save file doesn't exist!");
                this.Window.WriteLine("Press any key to continue.");
                this.Window.inputHandler.ReadKey();
                this.Window.Clear();
                return this.Setup();
            }


            // Return the GameManager
            return new GameManager(save);

        } else {

            this.Window.WriteLine("What's your adventure name?");
            var name = this.Window.inputHandler.ReadLine();

            if (name == null || name == "") {
                this.Window.WriteLine("Please enter a valid name.");
                return this.Setup();
            }

            var player = new Player(name, 10, 100);
            player.AddGold(10);
            var spellShop = new SpellShop();
            spellShop.AddSpell(new Fireball(), 15);
            spellShop.AddSpell(new Heal(), 20);
            spellShop.AddSpell(new MageArmor(), 10);

            return new GameManager(player, spellShop, new EntityManager());

        }

    }

    private void HandleEnd(bool died) {

        if (died) {

            this.Window.WriteLine(@" ▄▀▀▄ ▀▀▄  ▄▀▀▀▀▄   ▄▀▀▄ ▄▀▀▄      ▄▀▀█▄▄   ▄▀▀█▀▄   ▄▀▀█▄▄▄▄  ▄▀▀█▄▄  
                                    █   ▀▄ ▄▀ █      █ █   █    █     █ ▄▀   █ █   █  █ ▐  ▄▀   ▐ █ ▄▀   █ 
                                    ▐     █   █      █ ▐  █    █      ▐ █    █ ▐   █  ▐   █▄▄▄▄▄  ▐ █    █ 
                                          █   ▀▄    ▄▀   █    █         █    █     █      █    ▌    █    █ 
                                        ▄▀      ▀▀▀▀      ▀▄▄▄▄▀       ▄▀▄▄▄▄▀  ▄▀▀▀▀▀▄  ▄▀▄▄▄▄    ▄▀▄▄▄▄▀ 
                                        █                             █     ▐  █       █ █    ▐   █     ▐  
                                        ▐                             ▐        ▐       ▐ ▐        ▐        ");
            this.Window.WriteLine($"I'm sorry dear {this.gm.Player.Name}, but you have died.");
            this.Window.WriteLine("Press any key to quit.");
            this.Window.inputHandler.ReadKey();
            this.gm.IsRunning = false;

        } else {

            this.Window.WriteLine(@" ____  ____                 ____      ____  _            _  
                                    |_  _||_  _|               |_  _|    |_  _|(_)          | | 
                                      \ \  / / .--.   __   _     \ \  /\  / /  __   _ .--.  | | 
                                       \ \/ // .'`\ \[  | | |     \ \/  \/ /  [  | [ `.-. | | | 
                                       _|  |_| \__. | | \_/ |,     \  /\  /    | |  | | | | |_| 
                                      |______|'.__.'  '.__.'_/      \/  \/    [___][___||__](_) 
                                                                                                ");
            this.Window.WriteLine($"Congratulations, {this.gm.Player.Name}! You have won!");
            this.Window.WriteLine("Press any key to quit.");
            this.Window.inputHandler.ReadKey();
            this.gm.IsRunning = false;

        }

    }

    private void BuySpell() {

        this.Window.WriteLine($"You have {this.gm.Player.GetGold()} Gold.\nWhich Spell would you like to buy?");
        this.Window.WriteLine(this.gm.SpellShop.ToString());

        var spellName = this.Window.inputHandler.ReadLine();
        spellName = spellName.ToLower();

        if (spellName == "quit" || spellName == "q") {
            
            return;

        }

        if (spellName == "help") {

            this.PrintRules();
            this.BuySpell();
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
        this.Window.WriteLine($"You successfully bought the {spellToBuy.Name} spell!");

    }

    private void PrintRules() {

        this.Window.SetFormat(ConsoleFormat.Bold);
        this.Window.ForegroundColor = ConsoleRGB.YellowForeground;
        this.Window.WriteLine("--- RULES ---");
        this.Window.WriteLine("---=======---\nYou are a wizard. Fight monsters as they come, and use the gold they drop to buy more spells.\nGet 1000 gold to win.\n---=======---");
        this.Window.Write("Press any key to continue.");
        this.Window.inputHandler.ReadKey();
        this.Window.WriteLine("\r--- COMMANDS ---           ");
        this.Window.WriteLine("---=======---\nhelp - Brings up this menu.");
        this.Window.WriteLine("spells - Brings up the spell-buying menu.");
        this.Window.WriteLine("quit - Quits the game.\n---=======---");
        this.Window.Write("Press any key to continue.");
        this.Window.inputHandler.ReadKey();
        this.Window.WriteLine("\r--- FIGHTING ---            ");
        this.Window.WriteLine("---=======---\nYou will fight one monster at a time, casting a spell of your choice.");
        this.Window.WriteLine("If you win, you will get gold, and the next monster will be summoned.");
        this.Window.WriteLine("If you lose, you will be sent back to the main menu.\n---=======---");
        this.Window.Write("Press any key to continue.");
        this.Window.inputHandler.ReadKey();
        this.Window.WriteLine("\r--- FIGHTING COMMANDS ---     ");
        this.Window.WriteLine("---=======---\nhelp - Brings up this menu.");
        this.Window.WriteLine("cast - Brings up the spellcasting menu.\n---=======---");
        this.Window.WriteLine("\n\nPress any key to close this menu.");
        this.Window.inputHandler.ReadKey();
        this.Window.Clear();

    }

    private void CastSpell() {

        this.Window.WriteLine("Which spell would you like to cast?");
        foreach (var spell in this.gm.Player.Spells) {
            this.Window.WriteLine($"{spell.Name}");
        }

        var spellName = this.Window.inputHandler.ReadLine();

        if (spellName == "quit" || spellName == "q") {
            return;
        }

        if (spellName == "help") {
            this.PrintRules();
            return;
        }

        if (spellName == "" || spellName == null) {
            this.Window.WriteLine("Please enter a valid spell name.");
            this.CastSpell();
            return;
        }

        ISpell spellToCast = new NullSpell();

        foreach (var spell in this.gm.Player.Spells) {
            if (spell.Name.ToLower() == spellName.ToLower()) {
                spellToCast = spell;
                break;
            }
        }

        if (spellToCast is NullSpell) {
            this.Window.WriteLine("Please enter a valid spell name.");
            this.CastSpell();
            return;
        }

        int dmg = this.gm.CastSpell(spellToCast);

        this.Window.WriteLine($"You cast {spellToCast.Name} for {dmg} damage!");
        
    }

    private string GetPlayerName() {
        this.Window.Write("Please enter your adventurer's name: ");
        var ret =  this.Window.inputHandler.ReadLine();

        // Null check
        if (ret == "" || ret == null) {
            this.Window.WriteLine("Please enter a valid name.");
            return this.GetPlayerName();
        }

        return ret;

    }

    private string Prompt() {

        foreach (var command in this.commands) {

            this.Window.WriteLine($"{command.Key} - {command.Value}");

        }

        this.Window.Write("What would you like to do?\n>>> ");        

        var ret = this.Window.inputHandler.ReadLine();

        if (ret == null || ret == "") {
            this.Window.WriteLine("\nPlease enter a valid command.");
            return this.Prompt();
        }

        ret = ret.ToLower();

        // Make sure it's a valid command
        if (!this.commands.ContainsKey(ret)) {
            this.Window.WriteLine("\nPlease enter a valid command.");
            return this.Prompt();
        }

        return ret;

    }

    private void RunCommand(string command) {
        switch (command) {
            case "help":
                this.PrintRules();
                break;
            case "spells":
                this.BuySpell();
                break;
            case "q":
            case "quit":
                this.gm.IsRunning = false;
                break;
            case "cast":
                this.CastSpell();
                break;
            default:
                this.Window.WriteLine("Please enter a valid command.");
                break;
        }
    }
    #endregion
}