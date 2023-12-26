using System;
using Gammer0909.WizardGame.Entities;
using Gammer0909.WizardGame.Entities.Monsters;

namespace Gammer0909.WizardGame.Managers;

/// <summary>
/// The Game Manager that manages the Monsters.
/// </summary>
public class EntityManager {

    private IEntity currentEntity;

    public Queue<IEntity> entities { get; set; }

    public EntityManager() {
        this.entities = new Queue<IEntity>();
        this.currentEntity = new NullEntity();
    }

    public void AddEntity(IEntity monster) {
        this.entities.Enqueue(monster);
    }

    public void GetEntity() {
        var entity = this.entities.Dequeue();
        // Is there anything left in the queue?
        if (this.entities.Count == 0) {
            this.CreateEntities(5);
        }
        this.currentEntity = entity;
    }

    public bool HasEntities() {
        return this.entities.Count > 0;
    }

    public IEntity GetCurrentEntity() {
        return this.currentEntity;
    }

    public void CreateEntities(int count) {

        // Fill the Queue with a count number of random IEntitys
        for (int i = 0; i < count; i++) {
            var random = new Random();
            var randomInt = random.Next(0, 3);
            switch (randomInt) {
                case 0:
                    this.AddEntity(new Skeleton("Skeleton", 10));
                    break;
                case 1:
                    this.AddEntity(new Zombie("Zombie", 10));
                    break;
                case 2:
                    this.AddEntity(new Goblin("Goblin", 10));
                    break;
            }
        }

    }

}