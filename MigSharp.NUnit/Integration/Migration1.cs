using System.Data;

namespace MigSharp.NUnit.Integration
{
    [MigrationExport]
    internal class Migration1 : IIntegrationTestMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable(Tables[0].Name).OnSchema("Schema1")
                .WithPrimaryKeyColumn(Tables[0].Columns[0], DbType.Int32);
        }

        public ExpectedTables Tables
        {
            get
            {
                return new ExpectedTables
                {
                    new ExpectedTable("Mig1", "Id")
                };
            }
        }
    }
}