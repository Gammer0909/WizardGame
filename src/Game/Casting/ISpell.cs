using System;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;


namespace Gammer0909.WizardGame.Casting;

/// <summary>
/// The interface for a spell.
/// </summary>
public interface ISpell {

    #region Properties
    /// <summary>
    /// The Dice that should be rolled when casting the spell.
    /// </summary>
    public DieType damageDie { get; set; }

    /// <summary>
    /// The amount of damage that the spell does.
    /// </summary>
    public int Damage { get; set; }

    /// <summary>
    /// The mana cost of the spell.
    /// </summary>
    public int ManaCost { get; set; }

    /// <summary>
    /// The name of the spell
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The cost of the spell.
    /// </summary>
    public int Cost { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Casts the spell on the specified <see cref="IEntity"/>.
    /// </summary>
    /// <param name="target">The <see cref="IEntity"/> to target</param>
    public void Cast(IEntity target);
    #endregion
}