﻿using System;
using System.Data;
using Coevery.Data.Migration;

namespace MusicStore {
    public class MusicStoreDataMigration : DataMigrationImpl {
        public int Create() {
            SchemaBuilder.CreateTable("Album",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<int>("Genre_Id", col => col.NotNull())
                    .Column<int>("Artist_Id", col => col.NotNull())
                    .Column<string>("Title", col => col.WithLength(160).NotNull())
                    .Column<decimal>("Price", col => col.WithPrecision(10).WithScale(2).NotNull())
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
                   .Column<string>("Cart_Id", col => col.NotNull().WithType(DbType.AnsiString).WithLength(50))
                   .Column<int>("Album_Id", col => col.NotNull())
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
                    .Column<string>("Username", col => col.WithLength(256))
                    .Column<string>("FirstName", col => col.WithLength(160))
                    .Column<string>("LastName", col => col.WithLength(160))
                    .Column<string>("Address", col => col.WithLength(70))
                    .Column<string>("City", col => col.WithLength(40))
                    .Column<string>("State", col => col.WithLength(40))
                    .Column<string>("PostalCode", col => col.WithLength(10))
                    .Column<string>("Country", col => col.WithLength(40))
                    .Column<string>("Phone", col => col.WithLength(24))
                    .Column<string>("Email", col => col.WithLength(160))
                    .Column<decimal>("Total", col => col.WithPrecision(10).WithScale(2).NotNull())
                );

            SchemaBuilder.CreateTable("OrderDetail",
                table => table
                    .Column<int>("Id", col => col.PrimaryKey().Identity())
                    .Column<int>("Order_Id", col => col.NotNull())
                    .Column<int>("Album_Id", col => col.NotNull())
                    .Column<int>("Quantity", col => col.NotNull())
                    .Column<decimal>("UnitPrice", col => col.WithPrecision(10).WithScale(2).NotNull())
                );

            SchemaBuilder.CreateForeignKey("FK_Album_Genre",
                "Album", new string[] { "Genre_Id" },
                "Genre", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_Album_Artist",
                "Album", new string[] { "Artist_Id" },
                "Artist", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_Cart_Album",
                "Cart", new string[] { "Album_Id" },
                "Album", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_OrderDetail_Album",
                "OrderDetail", new string[] { "Album_Id" },
                "Album", new string[] { "Id" });

            SchemaBuilder.CreateForeignKey("FK_OrderDetail_Order",
                "OrderDetail", new string[] { "Order_Id" },
                "Order", new string[] { "Id" });

            return 1;
        }
    }
}