using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MigSharp.NUnit.Integration
{
    [MigrationExport(Tag = "Create table with specific schema")]
    internal class Migration19 : IIntegrationTestMigration
    {
        private const string FirstDefaultValue = "Test";
        private const int SecondDefaultValue = 747;

        public void Up(IDatabase db)
        {
            // Schema is only supported on SQL Server
            if (db.Context.ProviderMetadata.Name != ProviderNames.SqlServer2005 &&
                db.Context.ProviderMetadata.Name != ProviderNames.SqlServer2008 &&
                db.Context.ProviderMetadata.Name != ProviderNames.SqlServer2012)
            {
                return;
            }

            const string schemaName = "Schema1";

            db.Execute(string.Format("CREATE SCHEMA {0}", schemaName));

            var table = db.OnSchema(schemaName).CreateTable(Tables[0].Name)
                .WithPrimaryKeyColumn(Tables[0].Columns[0], DbType.Int32);

            table.WithNotNullableColumn(Tables[0].Columns[1], DbType.String).OfSize(10).HavingDefault(FirstDefaultValue);

            db.Execute(string.Format(CultureInfo.InvariantCulture, "INSERT INTO [{3}].[{0}] (\"{1}\") VALUES ('{2}')", Tables[0].Name, Tables[0].Columns[0], Tables[0].Value(0, 0), schemaName));
        }

        public ExpectedTables Tables { get
        {
            return new ExpectedTables
                {
                    new ExpectedTable("Mig19", "Id", "First")
                    {
                        { 1, SecondDefaultValue },
                        { 2, SecondDefaultValue },
                    }
                };
        } }
    }
}
