using System;
using Gammer0909.WizardGame.Entities;
using Gammer0909.WizardGame.Dice;

public class NullEntity : IEntity
{
    public string Name { get; set; }
    public int Health { get; set; }
    public bool isArmorActive { get; set; }
    public DieType goldDice { get; set; }

    public NullEntity()
    {
        this.Name = "NullEntity";
        this.Health = 0;
        this.isArmorActive = false;
        this.goldDice = DieType.NONE;
    }

    public void Attack(IEntity entity) {

        throw new NotImplementedException("NullEntity cannot attack.");

    }
}