using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Klenzer.Domain.Entities;
using Klenzer.WebApi.Controllers.Inputs;
using Klenzer.WebApi.Controllers.Inputs.Servicos;
using Klenzer.WebApi.Controllers.Inputs.Agendamentos;
using Klenzer.WebApi.Controllers.Inputs.TipoServicos;
using Klenzer.WebApi.Controllers.Inputs.Clientes;
using Klenzer.WebApi.Controllers.Outputs;
using System.Linq;

namespace Klenzer.WebApi.Extensions
{
    public static class AutoMapExtensions
    {

        public static IServiceCollection AddApplicationModelMappings(this IServiceCollection services)
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<CreateUserInput, User>();
                cfg.CreateMap<LoginInput, User>();
                cfg.CreateMap<PostServico, Servico>();
                cfg.CreateMap<PostAgendamentoServicos, AgendamentoServico>();
                cfg.CreateMap<PostAgendamento, Agendamento>()
                    .ForMember(entity => entity.AgendamentosServicos, opt => opt.MapFrom(model => model.AgendamentosServicos));
                cfg.CreateMap<PostTipoServico, TipoServico>();
                cfg.CreateMap<PostCliente, Cliente>();
                cfg.CreateMap<Cliente, ClienteOutput>();
                cfg.CreateMap<User, UserOutput>();
                cfg.CreateMap<Servico, ServicoOutput>();
                cfg.CreateMap<TipoServico, TipoServicoOutput>();
                cfg.CreateMap<Agendamento, AgendamentoOutput>().ForMember(x => x.AgendamentosServicos, src => src.MapFrom(y => y.AgendamentosServicos.Select(z => z.Servico)));
            });
            Mapper.Configuration.CompileMappings();
            return services;
        }

        /// <summary>
        /// Converts an object to another using AutoMapper library. Creates a new object of <typeparamref name="TDestination"/>.
        /// There must be a mapping between objects before calling this method.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination object</typeparam>
        /// <param name="source">Source object</param>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// There must be a mapping between objects before calling this method.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}
