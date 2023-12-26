using System;
using Gammer0909.WizardGame.Dice;

namespace Gammer0909.WizardGame.Entities;

/// <summary>
/// The abstraction of an entity.
/// </summary>
public interface IEntity {
    
    #region Properties
    /// <summary>
    /// The name of the entity.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The health of the entity.
    /// </summary>
    public int Health { get; set; }

    public DieType goldDice { get; set; }

    /// <summary>
    /// Is the armor of the entity active?
    /// </summary>
    /// <value></value>
    public bool isArmorActive { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Attacks the specified <see cref="IEntity"/>.
    /// </summary>
    /// <param name="entity">The Entity to attack</param>
    public void Attack(IEntity entity);
    #endregion

}