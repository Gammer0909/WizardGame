using System;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame.Casting.Spells;

namespace Gammer0909.WizardGame.Casting;

public class SpellShop {

    #region Properties
    /// <summary>
    /// The spells that the shop has.
    /// </summary>
    /// <value></value>
    public Dictionary<ISpell, int> Spells { get; set; }
    #endregion

    #region Constructors
    public SpellShop() {
        this.Spells = new Dictionary<ISpell, int>();
    }

    public SpellShop(ISpell[] spells) {

        this.Spells = new Dictionary<ISpell, int>();

        foreach (var spell in spells) {
            this.Spells.Add(spell, spell.Cost);
        }

    }
    #endregion

    #region Methods

    // Override tostring
    public override string ToString() {
        string spells = "";

        foreach (var spell in this.Spells) {
            spells += $"{spell.Key.Name} - {spell.Value} gold\n";
        }

        return spells;
    }

    public void AddSpell(ISpell spell, int cost) {
        this.Spells.Add(spell, cost);
    }

    private void RemoveSpell(ISpell spell) {
        this.Spells.Remove(spell);
    }

    /// <summary>
    /// Buys a spell from the shop.
    /// </summary>
    /// <param name="wanting">The spell that the person is wanting</param>
    /// <param name="gold">The amount of gold the Entity buying the spell has</param>
    /// <returns>The <see cref="ISpell"/> that the Spell that the person is wanting, if it exists.</returns>
    /// <exception cref="NullReferenceException">Thrown when the spell that the person is wanting doesn't exist.</exception>
    /// <exception cref="Exception">Thrown when the Entity doesn't have enough gold to buy the spell.</exception>
    public KeyValuePair<ISpell, int> BuySpell(ISpell wanting, int gold) {

        // Find the spell they want
        KeyValuePair<ISpell, int> spellToBuy = new KeyValuePair<ISpell, int>(new NullSpell(), 0);

        foreach (var spell in this.Spells) {
            if (spell.Key == wanting) {
                spellToBuy = spell;
                break;
            }
        }

        if (spellToBuy.Key == new NullSpell() && spellToBuy.Value == 0) {
            throw new NullReferenceException("That spell doesn't exist!");
        }

        // Check if we have enough to buy it
        if (spellToBuy.Value > gold) {
            throw new NotEnoughException("You don't have enough gold to buy that spell!");
        }

        // Remove the spell from the shop
        this.RemoveSpell(spellToBuy.Key);

        // Return the spell
        return spellToBuy;

    }
    #endregion

}