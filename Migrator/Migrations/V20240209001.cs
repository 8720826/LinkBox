using FluentMigrator;
using LinkBox.Extentions;

namespace LinkBox.Migrator.Migrations
{
    [Migration(20240209001)]
    public class V20240209001 : Migration
    {
        public override void Up()
        {
            Alter.Table("Config").AddColumn("Password").AsString(64).NotNullable().WithDefaultValue("admin".ToMd5());
        }

        public override void Down()
        {
            Delete.Column("Password").FromTable("Config");
        }
    }
}
