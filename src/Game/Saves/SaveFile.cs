using System;
using System.IO;
using Newtonsoft.Json;
using Gammer0909.WizardGame.Dice;
using Gammer0909.WizardGame.Entities;
using Gammer0909.WizardGame.Casting;
using Gammer0909.WizardGame;
using Gammer0909.WizardGame.Managers;

/// <summary>
/// The class that represents a json save file.
/// </summary>
public class SaveFile {

    [JsonProperty("player")]
    public Player player { get; set; }

    [JsonProperty("spells")]
    public ISpell[] spells { get; set; }

    [JsonProperty("shopSpells")]
    public ISpell[] shopSpells { get; set; }

    [JsonProperty("gold")]
    public int gold { get; set; }

    [JsonProperty("current-entity")]
    public IEntity currentEntity { get; set; }


    public SaveFile(Player player, ISpell[] spells, ISpell[] shopSpells, int gold, IEntity currentEntity) {
        this.player = player;
        this.spells = spells;
        this.shopSpells = shopSpells;
        this.gold = gold;
        this.currentEntity = currentEntity;
    }

    /// <summary>
    /// Updates the save with the new data.
    /// </summary>
    /// <param name="gm"></param>
    public void Update(GameManager gm) {
        this.player = gm.Player;
        this.spells = gm.Player.Spells.ToArray();
        // make an array of the keys
        ISpell[] spellArr = new ISpell[gm.SpellShop.Spells.Keys.Count];
        gm.SpellShop.Spells.Keys.CopyTo(spellArr, 0);
        this.shopSpells = spellArr;
        this.gold = gm.Player.GetGold();
        this.currentEntity = gm.EntityManager.GetCurrentEntity();    
    }

    public void Save(int num) {

        // Ok ok ok ok
        // So we need to

        // Serialize into JSON
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);

        string dir = "/save_slots";

        string name = $"/save_slot_number_{num}.json";

        // Check if the directory exists
        if (!Directory.Exists(dir)) {
            // If not then make one
            Directory.CreateDirectory(dir);
        }

        // Boom write the file
        File.WriteAllText(dir + name, json);


        // I would encrypt it, but idk how :p
    }

    public SaveFile FromFile(int num) {

        string json = File.ReadAllText($"/save_slots/save_slot_number_{num}.json");

        var saveFile = JsonConvert.DeserializeObject<SaveFile>(json);

        return saveFile ?? throw new NullReferenceException("Save file does not exist.");

    }
}