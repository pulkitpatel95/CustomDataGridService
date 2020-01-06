﻿using Hackathon.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hackathon.Services
{
    public class StudentDetailservice : IService<StudentDetails, int>
    {
        private readonly _DbContext ctx;
        /// <summary>
        /// Injecting the DbContext class in the Service
        /// </summary>
        /// <param name="ctx"></param>
        public StudentDetailservice(_DbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<StudentDetails> CreateAsync(StudentDetails entity)
        {
            var res = await ctx.StudentDetails.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.StudentDetails.FindAsync(id);
            if (res == null) return false;

            ctx.StudentDetails.Remove(res);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StudentDetails>> GetAsync()
        {
            var res = await ctx.StudentDetails.ToListAsync();
            return res;
        }

        public async Task<StudentDetails> GetAsync(int id)
        {
            var res = await ctx.StudentDetails.FindAsync(id);
            return res;
        }

        public async Task<StudentDetails> UpdateAsync(int id, StudentDetails entity)
        {
            var res = await ctx.StudentDetails.FindAsync(id);
            if (id != entity.StudentId) throw new Exception("Id does not match");
            if (res == null) throw new Exception("record not found");

            ctx.Entry<StudentDetails>(entity).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<UserPreferences>> GetHeadersAsync()
        {
            var res = await ctx.UserPreferences.ToListAsync();
            return res;
        }

        public string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
