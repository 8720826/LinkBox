using FluentMigrator;
using LinkBox.Entities.Enums;
using LinkBox.Extentions;

namespace LinkBox.Migrator.Migrations
{
    [Migration(0)]
    public class InitTables : Migration
    {
        public override void Up()
        {
            /*
            if (!Schema.Table("User").Exists())
            {
                Create.Table("User")
                    .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                    .WithColumn("Name").AsString(64).NotNullable()
                    .WithColumn("Password").AsString(64).NotNullable();

                Insert.IntoTable("User").Row(new
                {
                    Name = "admin",
                    Password = "admin".ToMd5(),
                    Id = 1
                });
            }
            */

            if (!Schema.Table("Category").Exists())
            {
                Create.Table("Category")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("Name").AsString(64).NotNullable()
                 .WithColumn("Type").AsInt32().NotNullable()
                 .WithColumn("SortId").AsInt32().NotNullable();

                Insert.IntoTable("Category").Row(new { Name = "应用", Type = (int)CategoryTypeEnum.应用, SortId = 1, Id = 1 });
                Insert.IntoTable("Category").Row(new { Name = "常用网站", Type = (int)CategoryTypeEnum.书签, SortId = 1, Id = 2 });

            }

            if (!Schema.Table("Link").Exists())
            {
                Create.Table("Link")
                    .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                    .WithColumn("CategoryId").AsInt32().NotNullable().Indexed()
                    .WithColumn("SortId").AsInt32().NotNullable()
                    .WithColumn("Icon").AsString(2048).NotNullable()
                    .WithColumn("Title").AsString(2048).NotNullable()
                    .WithColumn("Url").AsString(2048).NotNullable()
                    .WithColumn("Description").AsString(2048).NotNullable();

                Insert.IntoTable("Link").Row(new { Title = "LinkBox", Url = "http://127.0.0.1:5005", Description = "一个简洁的个人导航网站", Icon = "https://www.zhipin.com/favicon.ico", CategoryId = 1, SortId = 1 });

                Insert.IntoTable("Link").Row(new { Title = "LinkBox", Url = "https://github.com/8720826/LinkBox", Description = "一个简洁的个人导航网站", Icon = "https://www.zhipin.com/favicon.ico", CategoryId = 2, SortId = 1 });

            }

            if (!Schema.Table("Config").Exists())
            {
                Create.Table("Config")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                 .WithColumn("Name").AsString(64).NotNullable()
                 .WithColumn("Title").AsString(512).NotNullable();

                Insert.IntoTable("Config").Row(new { Name = "LinkBox", Title = "一个简洁、可全自定义的个人导航网站",  Id = 1 });
            }

        }



        public override void Down()
        {
            /*

            if (Schema.Table("User").Exists())
            {
                Delete.Table("User");
            }
            */

            if (Schema.Table("Category").Exists())
            {
                Delete.Table("Category");
            }


            if (Schema.Table("Link").Exists())
            {
                Delete.Table("Link");
            }

            if (Schema.Table("Config").Exists())
            {
                Delete.Table("Config");
            }


        }
    }
}
