using System.Collections.Generic;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void TInsert(T entity);
        void TUpdate(T entity);
        void TDelete(T entity);
        List<T> TGetAll();
        T GetById(int id);
    }
}
