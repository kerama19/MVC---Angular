using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab.Data.Model;

namespace Lab.Business
{
    public class Cars
    {
        public static List<Car> GetCarList()
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var cars = context.Cars.ToList();
                    return cars;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Car SaveCar(Car car)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    Car currentCar = new Car();

                    if (car.Id != 0)
                    {
                        currentCar = context.Cars.Where(x => x.Id == car.Id).FirstOrDefault();
                    }

                    currentCar.Id = car.Id;
                    currentCar.Manufacturer = car.Manufacturer;
                    currentCar.Model = car.Model;
                    currentCar.Color = car.Color;
                    currentCar.Year = car.Year;
                    currentCar.Category = car.Category;

                    if (car.Id == 0)
                    {
                        context.Cars.Add(currentCar);
                    }

                    context.SaveChanges();

                    context.Dispose();

                    return currentCar;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Car GetCarById(int id)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var car = context.Cars.Where(x => x.Id == id).FirstOrDefault();

                    return car;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool DeleteCar(int id)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var currentCar = context.Cars.Where(x => x.Id == id).FirstOrDefault();

                    context.Cars.Remove(currentCar);

                    context.SaveChanges();

                    context.Dispose();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
