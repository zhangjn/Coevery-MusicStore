using System.Data;
using Coevery.ContentManagement.MetaData;
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

            return 1;
        }
    }
}