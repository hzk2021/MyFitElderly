using EDP_Project.Models.Booking;
using Pract2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDP_Project.Services
{
    public class BookingService
    {
        private readonly UserDbContext _context;

        public BookingService(UserDbContext context)
        {
            _context = context;
        }

        public string CreateSpecialistDepartment (SpecialistDepartment specialistDepartment)
        {
            try
            {
                if (_context.SpecialistDepartment.Any(x => x.Department == specialistDepartment.Department))
                    return $"{specialistDepartment.Department} already exists.";

                _context.SpecialistDepartment.Add(specialistDepartment);
                _context.SaveChanges();
                return "True";
            }
            catch (Exception)
            {
                return "An error occured while adding Specialist Field to DB";
            }

        }

        public string CreateSpecialist(Specialist specialist)
        {
            try
            {
                if (_context.Specialist.Any(x => x.Department == specialist.Name))
                {
                    if (_context.Specialist.Any(x => x.Department == specialist.Department))
                        return $"{specialist.Name} of {specialist.Department} already exists.";
                }
                    

                _context.Specialist.Add(specialist);
                _context.SaveChanges();
                return "True";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "An error occured while adding Specialist Field to DB";
            }

        }

        public string UpdateSpecialist(Specialist specialist)
        {
            try
            {
                Console.WriteLine(specialist.Id);
                if (_context.Specialist.Any(x => x.Id == specialist.Id))
                {
                    _context.Specialist.Update(specialist);
                    _context.SaveChanges();
                    return "True";
                }

                return "Unknown specialist Id. Please update with a valid specialist Id";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred while registering entry update. Try again later.";
            }

        }

        public Specialist GetSpecialistById(int id)
        {
            return _context.Specialist.Where(x => x.Id == id).ToList()[0];
        }

        public List<Specialist> getSpecialistRecords()
        {
            return _context.Specialist.ToList();
        }


        public void DeleteSpecialist(int id)
        {
            Specialist specialistProfile = _context.Specialist.Where(x => x.Id == id).ToList()[0];
            _context.Specialist.Remove(specialistProfile);
            _context.SaveChanges();
        }
    }
}
