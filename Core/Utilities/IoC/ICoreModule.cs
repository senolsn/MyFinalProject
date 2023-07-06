using Microsoft.Extensions.DependencyInjection;


namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        //IServiceCollection bizim program.cs de kullandığımız arkada instance new'lerken kullandığımız bir servistir.
        void Load(IServiceCollection serviceCollection);
    }
}
