using Microsoft.Extensions.DependencyInjection;

namespace LgtvNetworkController.DependencyInjection.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDecorator<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService, TService> decoratorFactory,
        ServiceLifetime? lifetime = null)
        where TService : class
    {
        var baseServiceDescriptor =
            services.LastOrDefault(s => s.ServiceType == typeof(TService))
            ?? throw new InvalidOperationException(
                $"No service of type {typeof(TService)} registered");
        var baseServiceFactory =
            GetFactoryForServiceDescriptor(baseServiceDescriptor);

        var decoratorDescriptor = ServiceDescriptor.Describe(
            typeof(TService),
            CreateDecoratedService,
            lifetime ?? baseServiceDescriptor.Lifetime);
        services.Add(decoratorDescriptor);

        return services;

        Func<IServiceProvider, object> GetFactoryForServiceDescriptor(
            ServiceDescriptor serviceDescriptor)
        {
            var serviceFactory = baseServiceDescriptor.ImplementationFactory;
            if (baseServiceDescriptor.ImplementationInstance is not null)
            {
                serviceFactory ??=
                    _ => baseServiceDescriptor.ImplementationInstance;
            }
            if (baseServiceDescriptor.ImplementationType is not null)
            {
                serviceFactory ??= sp => ActivatorUtilities.CreateInstance(
                    sp,
                    baseServiceDescriptor.ImplementationType,
                    Array.Empty<object>());
            }
            return serviceFactory
                ?? throw new InvalidOperationException(
                    $"No implementation found for service {typeof(TService)}");
        }

        TService CreateDecoratedService(IServiceProvider serviceProvider)
        {
            var baseService = (TService)baseServiceFactory(serviceProvider);
            return decoratorFactory(serviceProvider, baseService);
        }
    }

    public static IServiceCollection AddDecorator<TService, TDecorator>(
        this IServiceCollection services,
        ServiceLifetime? lifetime = null)
        where TService : class
        where TDecorator : class, TService =>
        services.AddDecorator<TService>(
            (sp, baseService) =>
                ActivatorUtilities.CreateInstance<TDecorator>(sp, baseService),
            lifetime);
}
