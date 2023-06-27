using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //Çalıştırmaya çalıştığımız metotlar Add,Update,Delete vb.
        //Bütün Metotların Çatısı burasıdır !
        //Invocation => Çağırma yani benim metotlarım (Add,Update,Delete,GetAll...)
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation); //OnBefore da invocation'un argumentslerini kullanıyoruz. Burası kısaca Aspect'in çalıştığı yer. [ValidationAspect]
            try
            {
                invocation.Proceed(); //Add methodu  _productDal.Add(product);
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
