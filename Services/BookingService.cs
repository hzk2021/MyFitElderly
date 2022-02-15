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
        
        // Medical specialist CRUD
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

        // Medical Department CURD
        public string CreateDepartment(SpecialistDepartment specialistDepartment)
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
                return "An error occured while adding department Field to DB";
            }

        }

        public string UpdateDepartment(SpecialistDepartment specialistDepartment)
        {
            try
            {
                Console.WriteLine(specialistDepartment.Id);
                if (_context.SpecialistDepartment.Any(x => x.Id == specialistDepartment.Id))
                {
                    _context.SpecialistDepartment.Update(specialistDepartment);
                    _context.SaveChanges();
                    return "True";
                }

                return "Unknown department Id. Please update with a valid department Id";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred while registering entry update. Try again later.";
            }

        }

        public SpecialistDepartment GetDepartmentById(int id)
        {
            return _context.SpecialistDepartment.Where(x => x.Id == id).ToList()[0];
        }

        public List<SpecialistDepartment> getDepartmentRecords()
        {
            return _context.SpecialistDepartment.ToList();
        }


        public void DeleteDepartment(int id)
        {
            SpecialistDepartment departmentProfile = _context.SpecialistDepartment.Where(x => x.Id == id).ToList()[0];
            _context.SpecialistDepartment.Remove(departmentProfile);
            _context.SaveChanges();
        }

        // Specialist slot CURD
        public List<SpecialistSlot> GetSlotBySpecId(int id)
        {
            return _context.SpecialistSlot.Where(x => x.SpecId == id).ToList();
        }

        public SpecialistSlot GetSlotByid(int id)
        {
            return _context.SpecialistSlot.Where(x => x.Id == id).ToList()[0];
        }

        public void UpdateSlot(int id, int slot)
        {
            try
            {
                var slots = _context.SpecialistSlot.FirstOrDefault(x => x.Id == id);
                
                if (slot == 1)
                {
                    slots.Slot1 = true;
                }
                else if (slot == 2)
                {
                    slots.Slot2 = true;
                }
                else if (slot == 3)
                {
                    slots.Slot3 = true;
                }
                else if (slot == 4)
                {
                    slots.Slot4 = true;
                }
                else if (slot == 5)
                {
                    slots.Slot5 = true;
                }
                else if (slot == 6)
                {
                    slots.Slot6 = true;
                }
                else if (slot == 7)
                {
                    slots.Slot7 = true;
                }
                else if (slot == 8)
                {
                    slots.Slot8 = true;
                }
                _context.SpecialistSlot.Update(slots);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
