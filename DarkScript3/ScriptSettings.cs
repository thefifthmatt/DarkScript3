using System;
using System.Collections.Generic;

namespace DarkScript3
{
    public class ScriptSettings
    {
        public static readonly List<string> ValidSettings = new() { "ds1r", "nopreprocess" };

        private readonly InstructionDocs docs;
        private readonly Dictionary<string, string> settings;

        public ScriptSettings(InstructionDocs docs = null, Dictionary<string, string> existing = null)
        {
            this.docs = docs;
            settings = new Dictionary<string, string>();
            if (existing != null)
            {
                string val;
                if (existing.TryGetValue("ds1r", out val))
                {
                    IsRemastered = val == "true";
                }
                if (existing.TryGetValue("nopreprocess", out val))
                {
                    AllowPreprocess = val == "true";
                }
            }
        }

        public Dictionary<string, string> SettingsDict => new Dictionary<string, string>(settings);

        public EventCFG.CFGOptions CFGOptions
        {
            get
            {
                EventCFG.CFGOptions opts = EventCFG.CFGOptions.GetDefault();
                if (IsRemasteredSettable)
                {
                    opts.RestrictConditionGroupCount = !IsRemastered;
                }
                return opts;
            }
        }

        public bool IsRemasteredSettable => docs == null || docs.AllowRestrictConditionGroups;
        public bool AllowPreprocessSettable => docs == null || docs.Translator != null;

        public bool IsRemastered
        {
            // Default false
            get => settings.TryGetValue("ds1r", out string val) && val == "true";
            set
            {
                if (IsRemasteredSettable)
                {
                    // C# is bad
                    settings["ds1r"] = value ? "true" : "false";
                }
            }
        }

        public bool AllowPreprocess
        {
            // Default true if it's settable
            get => settings.TryGetValue("preprocess", out string val) ? val == "true" : AllowPreprocessSettable;
            set
            {
                if (AllowPreprocessSettable)
                {
                    settings["preprocess"] = value ? "true" : "false";
                }
            }
        }
    }
}
