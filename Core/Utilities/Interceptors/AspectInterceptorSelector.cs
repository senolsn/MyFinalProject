using Castle.DynamicProxy;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        //Type ProductManager, MethodInfo Add(); Bilgi Toplar (Reflection)
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //ProductManager'ın Attribute'larını çağırıyoruz. Bu yöntem, MethodInterceptionBaseAttribute türüne sahip özel nitelikleri alır.
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute> 
                (true).ToList();
            // Bu ifade, örneğin ProductManager sınıfının Add'e ulaşıp MIBA türüne ait özel nitelikleri alır.
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);   //Bu kod kısaca hem ProductManager class'ı için içeren attributeları ve ProductManager'in içindeki Metotları kapsayan attribute'ları
                                                          //örneğin Add methodunun attributelarını bir sıraya koymaya yarar.

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
