using System;
using System.Collections.Generic;

namespace SimpleModelViewPresenter
{
    public interface IDataManager<T>
    {
        IList<T> List();
        void Insert(T data);
        void Update(T data);
        void Delete(object key);
    }
}
