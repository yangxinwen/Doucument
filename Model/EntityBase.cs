using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Data.Entity;
using System.Transactions;

namespace DAL
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityBase<T> where T : class
    {
        protected static DbContext _dbContext = null;
        /// <summary>
        /// 获取数据库实体上下文
        /// </summary>
        /// <returns></returns>
        protected virtual DbContext GetDbContext()
        {
            if (_dbContext == null)
                _dbContext = new RakeBackDBEntities();            
            
            return _dbContext;
        }

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Add(T entity)
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            entitySet.Add(entity);
            return context.SaveChanges() > 0;

        }

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool AddList(List<T> entity)
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            entitySet.AddRange(entity);
            return context.SaveChanges() > 0;
        }
        /// <summary>
        /// 根据筛选条件删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Delete(Func<T, bool> predicate)
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            var list = entitySet.Where(predicate).ToList();
            if (list == null || list.Count() <= 0)
                return false;
            entitySet.RemoveRange(list);
            return context.SaveChanges() > 0;
        }

        public virtual bool Update(T entity, Func<T, bool> predicate)
        {
            try
            {
                var propertyList = typeof(T).GetProperties().ToList();
                var context = GetDbContext();
                var entitySet = context.Set<T>();

                //通过主键获取对象
                var old = entitySet.FirstOrDefault(predicate);
                if (old == null)
                    return false;
                //throw new Exception("没找到需要修改的对象");

                //old = entitySet.FirstOrDefault(a=>a.==old);
                //使用反射更新对象
                foreach (var item in propertyList)
                {
                    var newValue = item.GetValue(entity, null);
                    item.SetValue(old, newValue, null);
                }
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual T GetData(Func<T, bool> predicate)
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            var info = entitySet.FirstOrDefault(predicate);
            return info;
        }

        public virtual List<T> GetList(Func<T, bool> predicate)
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            var list = entitySet.Where(predicate).ToList();
            return list;
        }

        public virtual List<T> GetAllData()
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            var list = entitySet.ToList();
            return list;
        }

        public virtual int GetCount(Func<T, bool> predicate = null)
        {
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            if (predicate == null)
                return entitySet.Count();
            else
                return entitySet.Count(predicate);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pager">分页参数</param>
        /// <param name="predicate">过滤表达式</param>
        /// <param name="keySelector">排序表达式</param>
        /// <returns></returns>
        public virtual PagerResult<T> GetPagerData(PagerParam pager, Func<T, bool> predicate = null, Func<T, string> keySelector = null)
        {
            if (pager == null)
                pager = new PagerParam();

            var result = new PagerResult<T>();
            var context = GetDbContext();
            var entitySet = context.Set<T>();
            int count = 0;
            IEnumerable<T> list = null;

            if (predicate != null)
            {
                count = entitySet.Where(predicate).Count();
                list = entitySet.Where(predicate);
            }
            else
            {
                count = entitySet.Count();
                list = entitySet.AsEnumerable();
            }
            if (keySelector != null)
            {
                list = entitySet.OrderBy(keySelector);
            }

            if (list != null)
                list = list.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize);
            result.PageInfo = pager;
            result.PageInfo.TotalRecord = count;
            result.Content = list;
            return result;
        }

        /// <summary>
        /// 执行事务，只有同一个数据上下文(DbContext)才有效
        /// </summary>
        /// <param name="func">返回false取消事务，为true提交更改</param>
        public void ExecuteTransaction(Func<bool> func)
        {
            using (var scope = new TransactionScope())
            {
                var d = func.Invoke();
                if (d == true)
                    scope.Complete();
            }
        }
    }
}

