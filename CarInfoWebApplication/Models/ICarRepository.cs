using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfoWebApplication.Models
{
    public interface ICarRepository
    {
        string TestDb();
        IList<Car> LoadCarInfoFromFile(string str);
        void WriteCarInfoIntoFile(string fileName);
    }
}
