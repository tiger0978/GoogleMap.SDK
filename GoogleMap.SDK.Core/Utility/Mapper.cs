using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Core.Utility
{
    public class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source, Action<IMappingExpression<TSource, TDestination>> action = null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                var mappingExpression = cfg.CreateMap<TSource, TDestination>();
                action?.Invoke(mappingExpression);
            }
            );
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        public static IEnumerable<T2> Map<T1, T2>(IEnumerable<T1> sources,
        Action<IMappingExpression<T1, T2>> action = null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                var mappingExpression = cfg.CreateMap<T1, T2>();
                action?.Invoke(mappingExpression);
            }
            );
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<T1>, IEnumerable<T2>>(sources);
        }
    }
}
