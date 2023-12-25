using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Dice;   
using Gammer0909.WizardGame.Entities;

namespace Gammer0909.WizardGame.Casting.Spells;

public class Heal : ISpell {

    #region Properties
    public DieType damageDie { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; set; }
    public string Name { get; set; }

    #endregion

    #region Constructors
    public Heal() {
        this.Name = "Heal";
        this.ManaCost = 10;
        this.damageDie = DieType.D6;
        this.Damage = 0;
    }
    #endregion

    #region Methods
    public void Cast(IEntity target) {

        int heal = DiceRoll.Roll(this.damageDie);
        target.Health += heal;

    }
    #endregion


}