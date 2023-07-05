using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //Autofac paketinde yaptığımız IoC new'leme işleminin bir farklı alternatifidir. 
            // neden bunu kullanıyoruz WinForm gibi yapılar kullanırken bu şekilde IoC yapıyoruz.
            // Normalde Dependency injection yemez cünkü bu chainimizin dışında DA > Business >API bunlar chain şeklinde fakat Aspectler öyle
            //değil o yüzden bu şekilde newliyoruz
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
