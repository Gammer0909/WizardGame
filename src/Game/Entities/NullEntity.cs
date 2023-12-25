using System;
using Gammer0909.WizardGame.Entities;

public class NullEntity : IEntity
{
    public string Name { get; set; }
    public int Health { get; set; }
    public bool isArmorActive { get; set; }

    public NullEntity()
    {
        this.Name = "NullEntity";
        this.Health = 0;
        this.isArmorActive = false;
    }

    public void Attack(IEntity entity) {

        throw new NotImplementedException("NullEntity cannot attack.");

    }
}