using System;
using System.Text;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;
using Moq.Language;
using Moq.Language.Flow;
using System.Data.Entity.Core.Objects;

namespace Biod.Solution.UnitTest
{
    public static class MockDbContext
    {
        // Reference https://codethug.com/2015/03/20/mocking-dbset/
        private static Mock<DbSet<T>> CreateMockSet<T>(List<T> data)
                where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                    .Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
                    .Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                    .Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                    .Returns(() => queryableData.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);
            mockSet.Setup(m => m.AsNoTracking()).Returns(mockSet.Object);
            mockSet.As<IDbSet<T>>().Setup(m => m.Add(It.IsAny<T>())).Returns<T>(i => { data.Add(i); return i; });
            mockSet.As<IDbSet<T>>().Setup(m => m.Remove(It.IsAny<T>())).Returns<T>(i => { data.Remove(i); return i; });

            return mockSet;
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
                this IReturns<TContext, DbSet<TEntity>> setup,
                TEntity[] entities)
            where TEntity : class
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(entities.ToList());
            return setup.Returns(mockSet.Object);
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
                this IReturns<TContext, DbSet<TEntity>> setup,
                IQueryable<TEntity> entities)
            where TEntity : class
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(entities.ToList());
            return setup.Returns(mockSet.Object);
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(
                this IReturns<TContext, DbSet<TEntity>> setup,
                IEnumerable<TEntity> entities)
            where TEntity : class
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(entities.ToList());
            return setup.Returns(mockSet.Object);
        }

        public class TestableObjectResult<T> : ObjectResult<T> { }
    }
}
