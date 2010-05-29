﻿using System.Collections.Generic;
using System.Linq;

using MigSharp.Providers;

namespace MigSharp.Core.Commands
{
    internal class CreateTableCommand : Command, IScriptableCommand
    {
        private readonly string _tableName;

        public CreateTableCommand(string tableName)
        {
            _tableName = tableName;
        }

        public IEnumerable<string> Script(IProvider provider, ICommand parentCommand)
        {
            IEnumerable<CreateColumnCommand> createColumnCommands = Children.OfType<CreateColumnCommand>();
            if (createColumnCommands.Count() > 0)
            {
                foreach (string commandText in provider.CreateTable(_tableName,
                    createColumnCommands.Select(c => new CreatedColumn(c.ColumnName, c.Type, c.IsNullable, c.IsPrimaryKey))))
                {
                    yield return commandText;
                }
            }
        }
    }
}