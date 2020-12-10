﻿using SQLite;

namespace HTools.Config
{
    [Table("Config")]
    internal class ConfigItem
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}