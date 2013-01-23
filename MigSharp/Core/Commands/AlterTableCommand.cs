namespace MigSharp.Core.Commands
{
    internal class AlterTableCommand : Command
    {
        private readonly string _tableName;

        public string TableName { get { return _tableName; } }

        public AlterTableCommand(ICommand parent, string tableName, string schemaName = null)
            : base(parent)
        {
            _tableName = tableName;
            SchemaName = schemaName;
        }
    }
}