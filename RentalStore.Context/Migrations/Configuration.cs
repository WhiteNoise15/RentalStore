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

        }


        private Genre[] GenerateGenres()
        {
            Genre[] genres = new Genre[] {
                new Genre() { Name = "Комедия" },
                new Genre() { Name = "Фантастика" },
                new Genre() { Name = "Драма" },
                new Genre() { Name = "Боевик" },
                new Genre() { Name = "Триллер" },
                new Genre() { Name = "Мультфильм" },
                new Genre() { Name = "Мелодрама" },
            };

            return genres;
        }
   
    }
}
