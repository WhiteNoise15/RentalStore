namespace RentalStore.Context.Migrations
{
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<RentalStore.Context.RentalStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        private string EncryptPassword(string password)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;
        }

        protected override void Seed(RentalStore.Context.RentalStoreContext context)
        {
            context.Genres.AddOrUpdate(g => g.Name, GenerateGenres());

            context.Roles.AddOrUpdate(r => r.Name, new Role[]{
                new Role()
                {
                    Name="admin"
                },
                new Role
                {
                    Name="user"
                }
            });

            context.Users.AddOrUpdate(u => u.Email, new User[]{
                new User()
                {
                    Email="example.@example.com",
                    Username="admin",
                    Password= EncryptPassword("admin"),
                    RoleId= 1
                }
            });


            context.Genres.AddOrUpdate(g => g.Name, new Genre[]
            {
                new Genre() { Name = "Drama" },
                new Genre() { Name = "Action" },
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Romance" },
                new Genre() { Name = "Comedy" },
                new Genre() { Name = "Triller" },
            });

            context.Movies.AddOrUpdate(g => g.Title, new Movie[]
           {
                 new Movie()
                 {   Title="Drive",
                     Image ="http://allreport.ru/wp-content/uploads/2011/10/drive-550x340.jpg",
                     GenreId = 1,
                     Director ="Nicolas Winding Refn",
                     Writer="James Sallis",
                     Producer="Michel Litvak",
                     ReleaseDate = "03.11.11",
                     Count=1,
                     Description = "A mysterious Hollywood stuntman and mechanic moonlights as a getaway driver and finds himself trouble when he helps out his neighbor.",
                     TrailerURL = "https://www.youtube.com/watch?v=CWX34ShfcsE"
                },

                 new Movie()
                 {   Title="Mulholland Drive",
                     Image ="http://www.lemouv.fr/sites/default/files/2014/05/13/147640/mulholland%20drive.jpg",
                     GenreId = 1,
                     Director ="David Lynch",
                     Writer="David Lynch",
                     Producer="Neal Edelstein",
                     ReleaseDate = "14.03.02",
                     Count=1,
                     Description = "After a car wreck on the winding Mulholland Drive renders a woman amnesiac, she and a perky Hollywood-hopeful search for clues and answers across Los Angeles in a twisting venture beyond dreams and reality.",
                     TrailerURL = "https://www.youtube.com/watch?v=XQ5Q0CHQ0EU"
                },

                  new Movie()
                  {   Title="A Clockwork Orange",
                      Image ="https://resizing.flixster.com/wGgg03wtPQkhY3UWDyLX101Ofzk=/206x305/v1.bTsxMTE2ODAyNjtqOzE3Mjc0OzEyMDA7ODAwOzEyMDA",
                      GenreId = 1,
                      Director ="Stanley Kubrick",
                      Writer="Stanley Kubrick",
                      Producer="Stanley Kubrick",
                      ReleaseDate = "13.01.72",
                      Count=1,
                      Description = "In future Britain, Alex DeLarge, a charismatic and psycopath delinquent, who likes to practice crimes and ultra-violence with his gang, is jailed and volunteers for an experimental aversion therapy developed by the government in an effort to solve society's crime problem - but not all goes according to plan.",
                      TrailerURL = "https://www.youtube.com/watch?v=SPRzm8ibDQ8"
                },


           });

        }


        private Genre[] GenerateGenres()
        {
            Genre[] genres = new Genre[] {
               new Genre() { Name = "Drama" },
                new Genre() { Name = "Western" },
                new Genre() { Name = "Action" },
                new Genre() { Name = "Horror" },
                new Genre() { Name = "Romance" },
                new Genre() { Name = "Comedy" },
                new Genre() { Name = "Crime" },
            };

            return genres;
        }

    }
}
