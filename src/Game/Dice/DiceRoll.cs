using System;

namespace Gammer0909.WizardGame.Dice;

/// <summary>
/// The implementation of a die. 
/// </summary>
static public class DiceRoll {
    
    public static int Roll(DieType dieType) {
        var random = new Random();
        return random.Next(1, (int) dieType + 1);
    }

}