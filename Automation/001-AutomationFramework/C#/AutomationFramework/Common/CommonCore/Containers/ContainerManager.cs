using Autofac;
using Autofac.Core;
using CommonCore.Configuration;
using CommonCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CommonCore.Containers
{
    public class ContainerManager
    {
        private static readonly List<Assembly> _assemblies = new();
        private static List<DependencyInfo> _dependencies = new();
        private static readonly Dictionary<string, ILifetimeScope> _scopes = new();
        private static ContainerBuilder _builder = new();
        public static IContainer Container { get; private set; }

        internal static void EnumerateTestAssemblies(IEnumerable<Assembly> assemblies)
        {
            if (assemblies != null && assemblies.Any())
            {
                _assemblies.AddRange(assemblies);
            }
        }

        private static void RegisterTestingComponents()
        {
            List<TypeInfo>? interfaces = _assemblies.Where(a => !a.IsDynamic)
                                        .Distinct()
                                        .SelectMany(a => a.DefinedTypes)
                                        .Where(t => t.IsInterface && t.ImplementedInterfaces.Any(i => i.FullName == typeof(ITestingComponent).FullName)).ToList();

            _builder.RegisterAssemblyTypes(_assemblies.ToArray())
                   .Where(t => t.IsClass &&
                              !t.IsAbstract &&
                               (interfaces.Any(i => t.GetTypeInfo().ImplementedInterfaces.Any(ii => ii.FullName == i.FullName)) ||
                               t.GetTypeInfo().ImplementedInterfaces.Any(ii => ii.FullName == typeof(ITestingComponent).FullName)))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }

        public static T GetTestingComponent<T>()
        {
            return Container.Resolve<T>() ?? throw new DependencyResolutionException(nameof(T));
        }

        public static T GetTestingComponent<T>(string componentName)
        {
            DependencyInfo? dependency = _dependencies.FirstOrDefault(x => x.Name.ToUpperInvariant() == componentName.ToUpperInvariant()) ?? throw new DependencyResolutionException(nameof(T));
            if (!_scopes.TryGetValue(componentName, out ILifetimeScope scope))
            {
                scope = Container.BeginLifetimeScope(componentName);
                _scopes[componentName] = scope;
            }

            ResolvedParameter? parameter = new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == dependency.InstanceType,
                    (pi, ctx) => scope.ResolveNamed(dependency.Name, pi.ParameterType)
                );
            T? testingComponent = scope.Resolve<T>(parameter) ?? throw new DependencyResolutionException(nameof(T));
            return testingComponent;
        }

        public static IEnumerable<T> GetTestingComponents<T>(IEnumerable<string> componentNames)
        {
            IEnumerable<T> testingComponents = Enumerable.Empty<T>();
            foreach (string name in componentNames)
            {
                try
                {
                    T? testingComponent = GetTestingComponent<T>(name);
                    testingComponents = testingComponents.Append(testingComponent);
                }
                catch (DependencyResolutionException)
                {
                    continue;
                }
            }

            if (!testingComponents.Any())
            {
                throw new DependencyResolutionException(nameof(T));
            }

            return testingComponents;
        }

        public static bool TypeExists(Type type)
        {
            return _dependencies.Any(x => x.GetType() == type);
        }

        public static void RegisterType<T>(string name, T instance) where T : class
        {
            var info = new DependencyInfo(name, instance, typeof(T));
            _dependencies.Add(info);
        }

        private static void RegisterInstances()
        {
            _dependencies.ForEach(dep => _builder.RegisterInstance(dep.Instance).AsImplementedInterfaces().Named(dep.Name, dep.InstanceType));
        }

        public static string GetNameFromInstance<T>(T instance)
        {
            return _dependencies.FirstOrDefault(x => x.Instance.Equals(instance)).Name;
        }

        public static void BuildContainer()
        {
            _builder = new ContainerBuilder();
            RegisterTestingComponents();
            RegisterInstances();
            Container = _builder.Build();
        }

        public static void DisposeContainer()
        {
            foreach (KeyValuePair<string, ILifetimeScope> scope in _scopes)
            {
                scope.Value.Dispose();
            }

            _scopes.Clear();
            _dependencies = new();
            Container.Dispose();
        }
    }
}
