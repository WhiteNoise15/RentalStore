using RentalStore.Context;
using RentalStore.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RentalStore.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
       
        private readonly RentalStoreContext _rentalStoreContext;

        public AccountController()
        {
            _rentalStoreContext = new RentalStoreContext();
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

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(User user)
        {
            HttpResponseMessage response = null;
            string encryptedPassword = EncryptPassword(user.Password);

            if (user !=null)
            {
                try
                {
                    User currentUser = _rentalStoreContext.Users.First(u => u.Username == user.Username && u.Password == encryptedPassword);

                    if (currentUser != null)
                        response = Request.CreateResponse(HttpStatusCode.OK, currentUser);
                    else
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "Вы ввели неверные логин или пароль");
                   
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Возникла ошибка при авторизации");
                }

                return response;
            }
            else
            {
                return response;
            }
            

        }


        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(User user)
        {
            HttpResponseMessage response = null;
            user.Password = EncryptPassword(user.Password);
            user.Role = _rentalStoreContext.Roles.First(r => r.Id == 2);

            if(_rentalStoreContext.Users.Any(u => u.Username == user.Username))
            {
                response = Request.CreateResponse(HttpStatusCode.Conflict, "Такое имя пользователя уже существует");
            }

            else
            {
                try
                {
                    _rentalStoreContext.Users.Add(user);
                    _rentalStoreContext.SaveChanges();
                    response = Request.CreateResponse(HttpStatusCode.Created, user);
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Регистрация не удалась.");
                }
            }
          
            return response;

        }
    }
}
