using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;

namespace Gammer0909.WizardGame.Casting.Spells;

public class Fireball : ISpell {
    public DieType damageDie { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; set; }
    public string Name { get; set; }


    public Fireball() {
        this.Name = "Fireball";
        this.ManaCost = 10;
        this.damageDie = DieType.D8;
        this.Damage = 0;
    }


    public void Cast(IEntity target) {

        int damage = DiceRoll.Roll(this.damageDie);
        target.Health -= damage;

    }
}