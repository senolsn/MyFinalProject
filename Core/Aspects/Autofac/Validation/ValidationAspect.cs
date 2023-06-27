using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    //Add Metodunu ProductValidator ile Doğrula.
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //IsAssignableFrom => Gönderilen validatorType bir IValidator'ın nesnesi olarak atanabilir mi atanamaz mı onu kontrol eder.
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil !");
            }

            _validatorType = validatorType;
            //Priority'i burada set edebilirim.
        }
        protected override void OnBefore(IInvocation invocation) //Add Methodu
        {
            //Validation İşlemleri
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //ProductValidator Instance oluşturur. productValidator
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //ProductValidator'un base'inin  çalışma tipini bulur => Product
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //Add methodundaki Product'un parametrelerini bul. Type'i product'a eşit olanları al
            foreach (var entity in entities) //Her birini tek tek gez Validation tool'u kullanarak validate et.
            {
                ValidationTool.Validate(validator, entity);
            }
        }

        
    }
}
