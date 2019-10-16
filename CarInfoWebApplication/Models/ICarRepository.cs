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
        IList<Car> LoadCarInfoFromFile(string fileName);
        void WriteCarInfoIntoFile(string fileName);
        IList<Car> ListCarInfo();
        bool DeleteCar(int carId);
        Car FindCarByDes(int descriptionId);
        bool AddDescriptionToCar(int carId, string content);
    }
}
