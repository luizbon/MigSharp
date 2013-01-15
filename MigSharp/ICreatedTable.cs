using System.Diagnostics.CodeAnalysis;

namespace MigSharp
{
    /// <summary>
    /// Represents a created table.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Only temporary. Methods will be added to this interface eventually.")]
    public interface ICreatedTable : ICreatedTableBase
    {
        /// <summary>
        /// Gets the name of the schema.
        /// </summary>
        string Schema { get; }

        ICreatedTableBase OnSchema(string schemaName);
    }
}