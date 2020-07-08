using System;
using System.Collections.Generic;
using System.Text;
using App.DataAccess.Entities;
namespace App.DataAccess.Repositories
{
    public interface ILocationRepo
    {
        Location GetById(int id);
    }
}
