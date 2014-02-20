using System;
using System.Data;
using Coevery.Data.Migration;

namespace MusicStore {
    public class MusicStoreDataMigration : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable("Album",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<int>("GenreId", col => col.NotNull())
                    .Column<int>("ArtistId", col => col.NotNull())
                    .Column<string>("Title", col => col.WithLength(160))
                    .Column<decimal>("Price", col => col.WithPrecision(10).WithScale(2))
                    .Column<string>("AlbumArtUrl",
                    col => col.WithLength(1024).WithDefault("/Content/Images/placeholder.gif"))
                );

            SchemaBuilder.CreateTable("Artist",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<string>("Name", col => col.WithLength(120))
                );

            SchemaBuilder.CreateTable("Cart",
               table => table
                   .Column<int>("Id", col => col.PrimaryKey().Identity())
                   .Column<string>("CartId", col => col.NotNull().WithType(DbType.AnsiString).WithLength(50))
                   .Column<int>("AlbumId", col => col.NotNull())
                   .Column<int>("Count", col => col.NotNull())
                   .Column<DateTime>("DateCreated", col => col.NotNull())
               );

            SchemaBuilder.CreateTable("Genre",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<string>("Name", col => col.WithLength(120))
                    .Column<string>("Description", col => col.WithLength(4000))
                );

            SchemaBuilder.CreateTable("Order",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
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
                    .Column<decimal>("Total", col => col.WithPrecision(10).WithScale(2).NotNull())
                );

            SchemaBuilder.CreateTable("OrderDetail",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<int>("OrderId", col => col.NotNull())
                    .Column<int>("AlbumId", col => col.NotNull())
                    .Column<int>("Quantity", col => col.NotNull())
                    .Column<decimal>("UnitPrice", col => col.WithPrecision(10).WithScale(2).NotNull())
                );

            SchemaBuilder.CreateForeignKey("FK_Album_Genre",
                "Album", new string[] { "GenreId" },
                "Genre", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_Album_Artist",
                "Album", new string[] { "ArtistId" },
                "Artist", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_Cart_Album",
                "Cart", new string[] { "AlbumId" },
                "Album", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_OrderDetail_Album",
                "OrderDetail", new string[] { "AlbumId" },
                "Album", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_OrderDetail_Order",
                "OrderDetail", new string[] { "OrderId" },
                "Order", new string[] { "Id" });

            return 1;
        }
    }
}