using System;

using MigSharp.Core.Commands;

namespace MigSharp.Core.Entities
{
    internal class Database : IDatabase
    {
        private readonly IMigrationContext _context;
        private readonly MigrateCommand _root = new MigrateCommand();
        private readonly TableCollection _tables;
        private string _schemaName;

        internal ICommand Root { get { return _root; } }

        public IMigrationContext Context { get { return _context; } }
        public IExistingTableCollection Tables { get { return _tables; } }

        public Database(IMigrationContext context)
        {
            _context = context;
            _tables = new TableCollection(_root);
        }

        public ICreatedTable CreateTable(string tableName, string primaryKeyConstraintName)
        {
            var createTableCommand = new CreateTableCommand(_root, tableName, primaryKeyConstraintName);
            if (!string.IsNullOrEmpty(_schemaName))
                createTableCommand.SchemaName = _schemaName;
            _root.Add(createTableCommand);
            return new CreatedTable(createTableCommand);
        }

        public IDatabase OnSchema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public void Execute(string query)
        {
            var command = new CustomQueryCommand(_root, query);
            _root.Add(command);
        }

        public void Execute(Action<IRuntimeContext> action)
        {
            var command = new CallCommand(_root, action);
            _root.Add(command);
        }
    }
}