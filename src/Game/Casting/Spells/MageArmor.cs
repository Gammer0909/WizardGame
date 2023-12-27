using System;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;

namespace Gammer0909.WizardGame.Casting.Spells;

public class MageArmor : ISpell {

    #region Properties
    public DieType damageDie { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    #endregion


    #region Constructors
    public MageArmor() {
        this.Name = "Mage Armor";
        this.ManaCost = 10;
        this.damageDie = DieType.NONE;
        this.Damage = 0;
        this.Cost = 10;
    }

    #endregion

    #region Methods
    public void Cast(IEntity target) {
        target.isArmorActive = true;
    }
    #endregion

}