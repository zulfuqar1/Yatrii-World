using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Domain.Entities.Base;

namespace YatriiWorld.Persistance.Data.Common
{
    internal static class GlobalQueryFilter
    {

        public static void ApplyAllQueriesFilters(this ModelBuilder builder)
        {
            builder.ApplyQueryFilter<Category>();
            builder.ApplyQueryFilter<Review>();
            builder.ApplyQueryFilter<Ticket>();
            builder.ApplyQueryFilter<Tag>();
            builder.ApplyQueryFilter<Tour>();       
            builder.ApplyQueryFilter<TourImage>(); 
            builder.ApplyQueryFilter<ContactMessage>();
        }
        private static void ApplyQueryFilter<T>(this ModelBuilder builder) where T:BaseEntity,new()
        {
            builder.Entity<T>().HasQueryFilter(x => x.IsDeleted== false);


        }



  

        private static System.Linq.Expressions.LambdaExpression GenerateFilterExpression(Type type)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(type, "e");
            var property = System.Linq.Expressions.Expression.Property(parameter, "IsDeleted");
            var falseConstant = System.Linq.Expressions.Expression.Constant(false);
            var body = System.Linq.Expressions.Expression.Equal(property, falseConstant);

            return System.Linq.Expressions.Expression.Lambda(body, parameter);
        }

    }
}
