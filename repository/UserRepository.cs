using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.entities;
using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.repository
{
  public class UserRepository : IUserRepository
  {

  private readonly DataContext _context;
  private readonly IMapper _mapper;

  public UserRepository(DataContext context,IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

    public async Task<MemberDto> GetMemberAsync(string username)
    {
       return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
      return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    {
      return await _context.Users
      .Include(u => u.Photos)
      .FirstOrDefaultAsync(user => user.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
      return await _context.Users
      .Include(u => u.Photos)
      .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public void update(AppUser user)
    {
      _context.Entry(user).State = EntityState.Modified;
    }
  }
}