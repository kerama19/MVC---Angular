using Lab.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Business.Classes
{
    public class Addresses
    {
        public static List<Address> GetAddressList()
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var addresses = context.Addresses.ToList();
                    return addresses;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Address GetAddressById(int id)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var address = context.Addresses.Where(x => x.Id == id).FirstOrDefault();

                    return address;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Address SaveAddress(Address address)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    Address currentAddress = new Address();

                    if (address.Id != 0)
                    {
                        currentAddress = context.Addresses.Where(x => x.Id == address.Id).FirstOrDefault();
                    }

                    currentAddress.Id = address.Id;
                    currentAddress.CompanyName = address.CompanyName;
                    currentAddress.AddressLine1 = address.AddressLine1;
                    currentAddress.AddressLine2 = address.AddressLine2;
                    currentAddress.State = address.State;
                    currentAddress.City = address.City;
                    currentAddress.Country = address.Country;
                    currentAddress.Zip = address.Zip;

                    if (address.Id == 0)
                    {
                        context.Addresses.Add(currentAddress);
                    }

                    context.SaveChanges();

                    context.Dispose();

                    return currentAddress;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool DeleteAddress(int id)
        {
            try
            {
                using (var context = new MVCLabContext())
                {
                    var currentAddress = context.Addresses.Where(x => x.Id == id).FirstOrDefault();

                    context.Addresses.Remove(currentAddress);

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
