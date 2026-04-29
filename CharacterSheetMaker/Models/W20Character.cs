using System;
using System.Collections.Generic;
using System.Linq;

namespace W20Builder.Models
{
    public class W20Character
    {
        // Identity
        public string Name { get; set; } = string.Empty;
        public string Player { get; set; } = string.Empty;
        public string Chronicle { get; set; } = string.Empty;
        public string Concept { get; set; } = string.Empty;
        public string Breed { get; set; } = "Homid";
        public string Auspice { get; set; } = "Ragabash";
        public string Tribe { get; set; } = "Black Furies";

        // Attributes (Base 1 + Creation Dots)
        public int Strength { get; set; } = 1;
        public int Dexterity { get; set; } = 1;
        public int Stamina { get; set; } = 1;
        public int Charisma { get; set; } = 1;
        public int Manipulation { get; set; } = 1;
        public int Appearance { get; set; } = 1;
        public int Perception { get; set; } = 1;
        public int Intelligence { get; set; } = 1;
        public int Wits { get; set; } = 1;

        public int PhysicalDotsSpent => (Strength - 1) + (Dexterity - 1) + (Stamina - 1);
        public int SocialDotsSpent => (Charisma - 1) + (Manipulation - 1) + (Appearance - 1);
        public int MentalDotsSpent => (Perception - 1) + (Intelligence - 1) + (Wits - 1);

        // Abilities
        public Dictionary<string, int> Talents { get; set; } = new() {
            {"Alertness", 0}, {"Athletics", 0}, {"Brawl", 0}, {"Empathy", 0}, {"Expression", 0},
            {"Intimidation", 0}, {"Leadership", 0}, {"Primal-Urge", 0}, {"Streetwise", 0}, {"Subterfuge", 0}
        };
        public Dictionary<string, int> Skills { get; set; } = new() {
            {"Animal Ken", 0}, {"Crafts", 0}, {"Drive", 0}, {"Etiquette", 0}, {"Firearms", 0},
            {"Larceny", 0}, {"Meditation", 0}, {"Melee", 0}, {"Performance", 0}, {"Stealth", 0}
        };
        public Dictionary<string, int> Knowledges { get; set; } = new() {
            {"Academics", 0}, {"Computer", 0}, {"Enigmas", 0}, {"Investigation", 0}, {"Law", 0},
            {"Medicine", 0}, {"Occult", 0}, {"Rituals", 0}, {"Science", 0}, {"Technology", 0}
        };

        public int TalentDotsSpent => Talents.Values.Sum();
        public int SkillDotsSpent => Skills.Values.Sum();
        public int KnowledgeDotsSpent => Knowledges.Values.Sum();

        // Advantages
        public List<string> SelectedGifts { get; set; } = new() { "", "", "" };
        public List<string> FreebieGifts { get; set; } = new();
        public Dictionary<string, int> Backgrounds { get; set; } = new() {
            {"Allies", 0}, {"Ancestors", 0}, {"Contacts", 0}, {"Fate", 0}, {"Fetish", 0},
            {"Kinfolk", 0}, {"Mentor", 0}, {"Pure Breed", 0}, {"Resources", 0}, {"Rite", 0}, {"Totem", 0}
        };
        public int BackgroundDotsSpent => Backgrounds.Values.Sum();

        // Base Pools - Capped at 10
        public int ExtraWillpower { get; set; } = 0;
        public int ExtraRage { get; set; } = 0;
        public int ExtraGnosis { get; set; } = 0;

        public int TotalWillpower => Math.Min(10, GetBaseWillpower() + ExtraWillpower);
        public int TotalRage => Math.Min(10, GetBaseRage() + ExtraRage);
        public int TotalGnosis => Math.Min(10, GetBaseGnosis() + ExtraGnosis);

        public int GetBaseWillpower() => Tribe switch { "Bone Gnawers" or "Children of Gaia" or "Stargazers" or "Wendigo" => 4, _ => 3 };
        public int GetBaseRage() => Auspice switch { "Ragabash" => 1, "Theurge" => 2, "Philodox" => 3, "Galliard" => 4, "Ahroun" => 5, _ => 0 };
        public int GetBaseGnosis() => Breed switch { "Homid" => 1, "Metis" => 3, "Lupus" => 5, _ => 0 };

        // Final Renown
        public int Glory => Auspice == "Ahroun" ? 2 : (Auspice == "Galliard" ? 2 : 0);
        public int Honor => Auspice == "Philodox" ? 3 : (Auspice == "Ahroun" ? 1 : 0);
        public int Wisdom => Auspice == "Theurge" ? 3 : (Auspice == "Galliard" ? 1 : (Auspice == "Ragabash" ? 3 : 0));
    }
}