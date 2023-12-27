using System;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;

namespace Gammer0909.WizardGame.Entities.Monsters;

/// <summary>
/// The Goblin entity
/// </summary>
public class Goblin : IEntity {

    public string Name { get; set; }
    public int Health { get; set; }
    public bool isArmorActive { get; set; }
    public DieType goldDice { get; set; }

    public Goblin(string name, int health) {
        this.Name = name;
        this.Health = health;
        this.isArmorActive = false;
        this.goldDice = DieType.D10;
    }

    public void Attack(IEntity entity) {
        entity.Health -= DiceRoll.Roll(DieType.D4);
    }

    public void TakeDamage(int amount) {
        this.Health -= amount;
    }

    public bool IsDead() {
        return this.Health <= 0;
    }

    public override string ToString() {

        return $"Name: {this.Name}\nHealth: {this.Health}";

    }

}