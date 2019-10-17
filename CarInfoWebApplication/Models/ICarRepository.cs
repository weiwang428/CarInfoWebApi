using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfoWebApplication.Models
{
    public interface ICarRepository
    {
        void InitDb();
        bool LoadCarInfoFromFile(string fileName);
        bool WriteCarInfoIntoFile(string fileName);
        IList<Car> ListCarInfo();
        bool DeleteCar(int carId);
        Car FindCarByDes(int descriptionId);
        bool AddDescriptionToCar(int carId, string content);
        bool UpdateCar(int carId, Car car);
        bool DeleteDescriptionOfCar(int descriptionId);
    }
}
