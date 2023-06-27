using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            //Buradaki kodu refactor ederken ProductValidator'u newlemek yerine onun Iface'ine ulaşıp (F12) Iface'e Validator'un referansını tutturuyoruz ve dinamikleştiriyoruz.
            //Eğer Dinamikleştirme yapmazsak bunu core'a yazmanın bir anlamı kalmıyor ve sadece ProductValidator'a bağımlı kalmış oluyoruz. Burada amaç core katmanından alıp dinamik
            //şekilde kullanmak.

           
            var context = new ValidationContext<object>(entity);

            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
