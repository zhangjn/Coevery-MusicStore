using System;
using System.Data;
using Coevery.Data.Migration;

namespace MusicStore {
    public class MusicStoreDataMigration : DataMigrationImpl {
        public int Create() {
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
                    .Column<string>("RegistrationStatus", col => col.WithDefault("Approved"))
                    .Column<string>("EmailStatus", col => col.WithDefault("Approved"))
                    .Column<string>("EmailChallengeToken")
                );

            SchemaBuilder.CreateTable("Artist",
                table => table
                    .Column<int>("ArtistId", col => col.PrimaryKey().Identity())
                    .Column<string>("Name", col => col.WithLength(120))
                );

            SchemaBuilder.CreateTable("OrderRecord",
                table => table
                    .Column<int>("OrderId", col => col.PrimaryKey().Identity())
                    .Column<DateTime>("OrderDate", col => col.NotNull())
                    .Column<string>("Username", col => col.WithType(DbType.String).WithLength(256))
                    .Column<string>("FirstName", col => col.WithType(DbType.String).WithLength(160))
                    .Column<string>("LastName", col => col.WithType(DbType.String).WithLength(160))
                    .Column<string>("Address", col => col.WithType(DbType.String).WithLength(70))
                    .Column<string>("City", col => col.WithType(DbType.String).WithLength(40))
                    .Column<string>("State", col => col.WithType(DbType.String).WithLength(40))
                    .Column<string>("PostalCode", col => col.WithType(DbType.String).WithLength(10))
                    .Column<string>("Country", col => col.WithType(DbType.String).WithLength(40))
                    .Column<string>("Phone", col => col.WithType(DbType.String).WithLength(24))
                    .Column<string>("Email", col => col.WithType(DbType.String).WithLength(160))
                    .Column<decimal>("Total", col => col.WithType(DbType.VarNumeric).WithScale(10).WithPrecision(2).NotNull())
                );

            return 1;
        }
    }
}