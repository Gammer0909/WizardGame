using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;

namespace Gammer0909.WizardGame.Casting.Spells;

public class NullSpell : ISpell {
    public string Name { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; set; }
    public DieType damageDie { get; set; }
    public int Cost { get; set; }

    public NullSpell() {
        this.Name = "Null Spell";
        this.Damage = 0;
        this.damageDie = DieType.NONE;
        this.ManaCost = 0;
        this.Cost = 0;
    }

    public void Cast(IEntity entity) {
        throw new NotImplementedException();
    }
}