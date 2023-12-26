using System;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;
using Gammer0909.WizardGame.Entities.Monsters;

namespace Gammer0909.WizardGame.Entities.Monsters;

public class Skeleton : IEntity {
    public string Name { get; set; }
    public int Health { get; set; }
    public bool isArmorActive { get; set; }

    public DieType goldDice { get; set; }

    public Skeleton(string name, int health) {
        this.Name = name;
        this.Health = health;
        this.isArmorActive = false;
        this.goldDice = DieType.D6;
    }

    public void Attack(IEntity entity) {
        entity.Health -= DiceRoll.Roll(DieType.D6);
    }
}