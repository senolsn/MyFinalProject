using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed= "Ürünler listelendi!";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 adet ürün bulunabilir!";
        public static string ProductNameAlreadyExists = "Bu ürün daha önce eklenmiş!";

        public static string CategoryLimitExceded = "15'den fazla kategori mevcut olduğu için ürün eklenemez !";
        public static string? AuthorizationDenied = "Yetkiniz Yok.";
        internal static string UserRegistered = "Kayıt olundu.";
        internal static string UserNotFound = "Kullanıcı Bulunamadı !";
        internal static string PasswordError = "Şifre hatalı !";
        internal static string SuccessfulLogin = "Giriş Başarılı";
        internal static string UserAlreadyExists = "Kullanıcı mevcut";
        internal static string AccessTokenCreated = "Access token oluşturuldu";
    }
}
