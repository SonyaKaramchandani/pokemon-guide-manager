using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Biod.Insights.Service.UnitTest
{
    public static class DbSetExtensions
    {
        public static DbSet<T> MockFromSql<T>(this DbSet<T> dbSet, Func<IEnumerable<object>, IQueryable<T>> methodAction) where T : class
        {
            var queryProviderMock = new Mock<IQueryProvider>();
            queryProviderMock.Setup(p => p.CreateQuery<T>(It.IsAny<MethodCallExpression>()))
                .Returns<MethodCallExpression>(x => methodAction.Invoke(x.Arguments.Select(GetArgumentValue)));

            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>()
                .SetupGet(q => q.Provider)
                .Returns(() => queryProviderMock.Object);

            dbSetMock.As<IQueryable<T>>()
                .Setup(q => q.Expression)
                .Returns(Expression.Constant(dbSetMock.Object));
            return dbSetMock.Object;
        }

        private static object GetArgumentValue(Expression element)
        {
            var l = Expression.Lambda(Expression.Convert(element, element.Type));
            return l.Compile().DynamicInvoke();
        }
    }
}