using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Data
{
    public class ChargeService : IChargeService
    {
        private readonly ApplicationDbContext _context;

        public ChargeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Charge>> Get()
        {
            return await _context.Charges.ToListAsync();
        }

        public async Task<Charge> Get(int id)
        {
            var charge = await _context.Charges.FindAsync(id);
            return charge;
        }

        public async Task<Charge> Add(Charge charge)
        {
            _context.Charges.Add(charge);
            await _context.SaveChangesAsync();
            return charge;
        }

        public async Task<Charge> Update(Charge charge)
        {
            _context.Entry(charge).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return charge;
        }

        public async Task Delete(int id)
        {
            var charge = await _context.Charges.FindAsync(id);
            _context.Charges.Remove(charge);
            await _context.SaveChangesAsync();
        }

    }
}
