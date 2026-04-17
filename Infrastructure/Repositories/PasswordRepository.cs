using PassGeneratorService.Application.Interfaces;
using PassGeneratorService.Domain.Entity;

namespace PassGeneratorService.Infrastructure.Repositories
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly AppDbContext _context;

        public PasswordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePassword(Password password)
        {
            bool success = true;

            try
            {
                await _context.Passwords.AddAsync(password);
                await _context.SaveChangesAsync();
            } catch
            {
                success = false;
            }

            if (success)
                Console.WriteLine("Новый пароль: " + password.Value);
            else
                Console.WriteLine("Ошибка БД");

            return success;
        }
    }
}
