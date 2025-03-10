using AutoMapper;
using System.Collections;

﻿namespace Admin.Free.Infra
{
	public static class CollectionsExtension
	{
		public static IEnumerable<T> WhereIf<T>(IEnumerable<T> list, bool tag, Func<T, bool> func)
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
	}
}
