using System;
using System.Data;
using Coevery.Data.Migration;
namespace MusicStore
{
    public class MusicStoreDataMigration : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("Album",
                table => table
                    .Column<int>("AlbumId", col => col.PrimaryKey().Identity())
                    .Column<int>("GenreId", col => col.NotNull())
                    .Column<int>("ArtistId", col => col.NotNull())
                    .Column<string>("Title", col => col.WithLength(160))
                    .Column<double>("Price", col => col.WithPrecision(10).WithScale(2))
                    .Column<string>("AlbumArtUrl",
                        col => col.WithLength(1024).WithDefault("/Content/Images/placeholder.gif"))
                    .Column<string>("PasswordSalt")
                    .Column<string>("RegistrationStatus", c => c.WithDefault("Approved"))
                    .Column<string>("EmailStatus", c => c.WithDefault("Approved"))
                    .Column<string>("EmailChallengeToken")
                );

            SchemaBuilder.CreateTable("Artist",
                table => table
                    .Column<int>("ArtistId", col => col.PrimaryKey().Identity())
                    .Column<string>("Name", col => col.WithLength(120))
                );
 SchemaBuilder.CreateTable("OrderRecord",
                table => table
                    .Column<int>("OrderId", c => c.PrimaryKey().Identity())
                    .Column<DateTime>("OrderDate", c => c.NotNull())
                    .Column<string>("Username", c => c.WithType(DbType.String).WithLength(256))
                    .Column<string>("FirstName", c => c.WithType(DbType.String).WithLength(160))
                    .Column<string>("LastName", c => c.WithType(DbType.String).WithLength(160))
                    .Column<string>("Address", c => c.WithType(DbType.String).WithLength(70))
                    .Column<string>("City", c => c.WithType(DbType.String).WithLength(40))
                    .Column<string>("State", c => c.WithType(DbType.String).WithLength(40))
                    .Column<string>("PostalCode", c => c.WithType(DbType.String).WithLength(10))
                    .Column<string>("Country", c => c.WithType(DbType.String).WithLength(40))
                    .Column<string>("Phone", c => c.WithType(DbType.String).WithLength(24))
                    .Column<string>("Email", c => c.WithType(DbType.String).WithLength(160))
                    .Column<decimal>("Total", c => c.WithType(DbType.VarNumeric).WithScale(10).WithPrecision(2).NotNull())
                );

                

            return 1;
        }
    }
}