using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Supers.Infrastructure.Migrations.Versoes
{
    public abstract class VersaoBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity();

        }
    }
}
