﻿using System.Collections.Generic;
using System.Linq;

using MigSharp.Providers;

namespace MigSharp.Core.Commands
{
    internal class AddIndexCommand : Command, ITranslatableCommand
    {
        private readonly string _indexName;
        private readonly List<string> _columnNames = new List<string>();

        public new AlterTableCommand Parent { get { return (AlterTableCommand)base.Parent; } }

        public AddIndexCommand(AlterTableCommand parent, string indexName) : base(parent)
        {
            _indexName = indexName;
        }

        public void AddColumn(string columnName)
        {
            _columnNames.Add(columnName);
        }

        public IEnumerable<string> ToSql(IProvider provider, IRuntimeContext context)
        {
            if (_columnNames.Count == 0)
            {
                throw new InvalidCommandException("At least one column must be added to the AddIndex command.");
            }
            string effectiveIndexName = GetEffectiveIndexName();
            return provider.AddIndex(Parent.TableName, _columnNames, effectiveIndexName, SchemaName);
        }

        private string GetEffectiveIndexName()
        {
            return DefaultObjectNameProvider.GetIndexName(Parent.TableName, _columnNames.First(), _indexName);
        }
    }
}