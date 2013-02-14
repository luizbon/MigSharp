﻿using System.Collections.Generic;
using System.Linq;

using MigSharp.Providers;

namespace MigSharp.Core.Commands
{
    internal class AddForeignKeyToCommand : Command, ITranslatableCommand
    {
        private readonly string _referencedTableName;
        private readonly string _constraintName;
        private readonly string _referencedSchemaName;
        private readonly IList<KeyValuePair<string, string>> _columnNames = new List<KeyValuePair<string, string>>();

        public new AlterTableCommand Parent { get { return (AlterTableCommand)base.Parent; } }
        public IList<KeyValuePair<string, string>> ColumnNames { get { return _columnNames; } }

        public AddForeignKeyToCommand(AlterTableCommand parent, string referencedTableName, string constraintName, string referencedSchemaName) : base(parent)
        {
            _referencedTableName = referencedTableName;
            _constraintName = constraintName;
            _referencedSchemaName = referencedSchemaName;
        }

        public IEnumerable<string> ToSql(IProvider provider, IRuntimeContext context)
        {
            if (_columnNames.Count == 0)
            {
                throw new InvalidCommandException("At least one column must be added to the AddForeignKeyTo command.");
            }
            string effectiveConstraintName = GetEffectiveConstraintName();
            return provider.AddForeignKey(Parent.TableName, _referencedTableName, _columnNames.Select(p => new ColumnReference(p.Key, p.Value)), effectiveConstraintName, SchemaName, _referencedSchemaName);
        }

        private string GetEffectiveConstraintName()
        {
            return DefaultObjectNameProvider.GetForeignKeyConstraintName(Parent.TableName, _referencedTableName, _constraintName);
        }
    }
}