using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.Expressions;

namespace DossierManagement.Dal.Migrations
{
    [Migration(0,"Initial Migration")]
    public class InitialMigration:Migration
    {
        public override void Up()
        {
            Create.Table("Dossier")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("LastName").AsString(200).NotNullable()
                .WithColumn("FiscalCode").AsString(16).NotNullable()
                .WithColumn("BirthDate").AsDateTime2().NotNullable()
                .WithColumn("Status").AsByte().NotNullable()
                .WithColumn("Result").AsByte().NotNullable();

            Create.Table("DossierChangeStatus")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("PracticeId").AsInt32().NotNullable()
                .WithColumn("Status").AsByte().NotNullable()
                .WithColumn("Result").AsByte().NotNullable()
                .WithColumn("Date").AsDateTime2().NotNullable();

            Create.ForeignKey() 
                .FromTable("DossierChangeStatus").ForeignColumn("PracticeId")
                .ToTable("Dossier").PrimaryColumn("Id");

        }
        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
