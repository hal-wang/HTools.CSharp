﻿using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

namespace HTools.Config
{
    public abstract class SqliteConfigBase : IDisposable
    {
        private readonly SqliteBase<ConfigItem> _sqliteConnection;

        public SqliteConfigBase(string path = null)
        {
            _sqliteConnection = new SqliteBase<ConfigItem>(path ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config.db"));
        }

        public T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
        {
            if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

            var value = _sqliteConnection.Find<ConfigItem>(key);
            if (value == null)
            {
                Set(defaultValue, key);
                return defaultValue;
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        public void Set(object value, [CallerMemberName] string key = null)
        {
            if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

            _sqliteConnection.InsertOrReplace(new ConfigItem()
            {
                Key = key,
                Value = value.ToString()
            });
        }

        public string this[string key]
        {
            get => Get<string>(null, key);
            set => Set(value, key);
        }

        public void Dispose()
        {
            _sqliteConnection.Dispose();
        }
    }
}