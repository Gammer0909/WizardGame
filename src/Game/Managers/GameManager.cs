using System;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Casting.Spells;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;
using Gammer0909.WizardGame;

namespace Gammer0909.WizardGame.Managers;

/// <summary>
/// The Game Manager that manages the player, spell shop, monsters, etc.
/// </summary>
public class GameManager {

    #region Properties
    public Player Player { get; }
    public SpellShop SpellShop { get; }
    public EntityManager EntityManager { get; }
    public bool IsRunning { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="GameManager"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to use.</param>
    /// <param name="spellShop">The <see cref="SpellShop"/> to use.</param>
    /// <param name="entityManager">The <see cref="EntityManager"/> to use.</param>
    /// <param name="isRunning">The <see cref="IsRunning"/> to use.</param>
    public GameManager(Player player, SpellShop spellShop, EntityManager entityManager, bool isRunning) {
        this.Player = player;
        this.SpellShop = spellShop;
        this.EntityManager = entityManager;
        this.IsRunning = isRunning;
    }

    /// <summary>
    /// Creates a new instance of <see cref="GameManager"/>.
    /// </summary>
    /// <param name="p">The player to run the game with</param>
    /// <param name="s">The SpellShop to run the game with</param>
    /// <param name="e">the EntityManager to run the game with</param>
    public GameManager(Player p, SpellShop s, EntityManager e) {
        this.Player = p;
        this.SpellShop = s;
        this.EntityManager = e;
        this.IsRunning = true;
    }

    public GameManager() {
        this.Player = new Player("Player", 10, 100);
        this.SpellShop = new SpellShop();
        this.EntityManager = new EntityManager();
        this.IsRunning = true;
    }
    #endregion

    #region Methods
    public void BuySpell(ISpell wanting) {

        

        var spell = this.SpellShop.BuySpell(wanting, this.Player.GetGold());

        this.Player.AddSpell(spell.Key);

        this.Player.RemoveGold(spell.Value);

        return;

    }

    public void CastSpell(ISpell spell) {

        spell.Cast(this.EntityManager.GetCurrentEntity());

    }
    #endregion


}