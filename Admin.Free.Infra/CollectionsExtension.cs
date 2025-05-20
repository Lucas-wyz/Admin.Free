using AutoMapper;
using System.Collections;

namespace Admin.Free.Infra
{
	public static class CollectionsExtension
	{
		public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> list, bool tag, Func<T, bool> func)
		{
			if (tag)
			{
				return list.Where(func);
			}
			return list;

		}

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> list, bool tag, Expression<Func<T, bool>> func)
        {
            if (tag)
            {
                return list.Where(func);
            }
            return list;

        }


		public static IEnumerable<TDestination> ProjectTo<TDestination>(this IEnumerable source, IConfigurationProvider configuration)
		{
			return configuration.CreateMapper().Map<IEnumerable<TDestination>>(source);
		}

		/// <summary>
		/// 分页
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <param name="index"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int index, int size)
		{
			index = index > 1 ? index - 1 : 0;
			size = size > 1 ? size : 1;

			var _skip = index * size;

			return source.Skip(_skip).Take(size);
		}
	}
}
