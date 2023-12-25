using System.Collections.Generic;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Entities;

namespace Gammer0909.WizardGame;

/// <summary>
/// The abstraction of a player.
/// </summary>
public class Player : IEntity {

    #region Properties
    /// <summary>
    /// The name of the player.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The health of the player.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// The mana of the player.
    /// </summary>
    public int Mana { get; set; }

    /// <summary>
    /// The spells that the player owns
    /// </summary>
    public List<ISpell> Spells { get; set; }
    public bool isArmorActive { get; set; }

    /// <summary>
    /// The gold of the player.
    /// </summary>
    private int _gold;

    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of <see cref="Player"/>.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="health">The health of the player.</param>
    /// <param name="mana">The mana of the player.</param>
    public Player(string name, int health, int mana) {
        this.Name = name;
        this.Health = health;
        this.Mana = mana;
        this._gold = 0;
        this.isArmorActive = false;
        this.Spells = new List<ISpell>();
    }
    #endregion

    #region Methods
    public void Attack(ISpell casting, IEntity target) {
        casting.Cast(target);
    }

    /// <summary>
    /// Im implementing this method as a workaround so the MageArmor can apply to the player.
    /// </summary>
    /// <param name="entity"></param>
    public void Attack(IEntity entity) {
        this.Attack(this.Spells[0], entity);
    }

    public void AddSpell(ISpell spell) {
        this.Spells.Add(spell);
    }

    public int GetGold() {
        return this._gold;
    }

    public void AddGold(int gold) {
        this._gold += gold;
    }

    public void RemoveGold(int gold) {
        this._gold -= gold;
    }
    #endregion


}