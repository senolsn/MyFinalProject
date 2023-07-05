﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{   //Generic Constraint T => Sadece ref type olabilir ve IEntity veya onun Ref. ini tutan bir class olabilir.
    //new() => Newlenebilen bir class olmalı IEntity de yazabiliyorduk ama onu new'leyemiyoruz bu yüzden IEntity'in referanslarını kullandık
    //ama kendisini devredışı bıraktık.
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
